using QuanLyQuanAn.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanAn
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string password = txtPassword.Text;
            if (DangNhap(username, password))
            {
                QuanLyBan f = new QuanLyBan();
                this.Hide(); // ẩn form đăng nhập trước khi show form lên
                f.ShowDialog(); //ngưng các lệnh tiếp theo đến khi form này đóng lại
                this.Show(); //khi form đóng lại thì show form login ban đầu
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc tên đăng nhập!");
            }                
        }

        bool DangNhap(string username, string password)
        {
            return AccountDAO.Instance.Login(username, password);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            //Hỏi khi nhấn button thoát
            Application.Exit();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có thật sự muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }    
        }
    }
}
