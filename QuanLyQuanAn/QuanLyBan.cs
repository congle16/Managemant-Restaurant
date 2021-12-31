using QuanLyQuanAn.DAO;
using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QuanLyQuanAn.DTO.Menu;

namespace QuanLyQuanAn
{
    public partial class QuanLyBan : Form
    {
        public QuanLyBan()
        {
            InitializeComponent();
            LoadTable();
            LoadDanhMuc();
            LoadCBTable(cbChuyenBan);
        }

        #region Method

        void LoadTable()
        {
            flpTable.Controls.Clear();

            List<Table> tableList = TableDAO.Instance.LoadTableList();

            foreach (Table item in tableList)
            {
                Button btn = new Button() { Width = TableDAO.TableWidth ,Height = TableDAO.TableHeight};
                btn.Text = item.Name + Environment.NewLine + item.Status;

                btn.Tag = item;// lưu table vào

                //set event cho button bàn
                btn.Click += btn_Click;

                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.LightPink;
                        break;
                }    
                
                flpTable.Controls.Add(btn);
            }    
        }

        void LoadDanhMuc()
        {
            List<DanhMuc> listDanhMuc = DanhMucDAO.Instance.GetListDanhMuc();
            cbLoaiMonAn.DataSource = listDanhMuc;
            cbLoaiMonAn.DisplayMember = "name";
        }

        void LoadFoodbyDanhMucID(int id)
        {
            List<ThucAn> listFood = ThucAnDAO.Instance.GetThucAnByIdDanhMuc(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "name";
        }

        void ShowHoaDon(int id)
        {
            lvHoaDon.Items.Clear();
            // lấy hóa đơn từ bàn hiện tại mà chưa được checkout -> lấy hóa đơn infor
            List<Menu> listHoaDoninfor = MenuDAO.Instance.GetListMenuByTable(id);

            float tongGia = 0;
            foreach (Menu item in listHoaDoninfor)
            {
                ListViewItem lsvItem = new ListViewItem(item.Thucan.ToString());
                lsvItem.SubItems.Add(item.Soluong.ToString());
                lsvItem.SubItems.Add(item.Gia.ToString());
                lsvItem.SubItems.Add(item.TongGia.ToString());

                tongGia += item.TongGia;

                lvHoaDon.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");

            //Thread.CurrentThread.CurrentCulture = culture;

            txbTongGia.Text = tongGia.ToString("c", culture);
        }
        private void cbLoaiMonAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;

            if (cb.SelectedItem == null)
                return;

            DanhMuc selected = cb.SelectedItem as DanhMuc;
            id = selected.ID;

            LoadFoodbyDanhMucID(id);
        }

        void LoadCBTable (ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
        }

        #endregion

        #region Events

        private void btn_Click(object sender, EventArgs e)
        {
            int tableID = ((sender as Button).Tag as Table).ID;
            lvHoaDon.Tag = (sender as Button).Tag;
            ShowHoaDon(tableID);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ThongTinCaNhan f = new ThongTinCaNhan();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Admin f = new Admin();
            f.ShowDialog();
        }

        private void btAddFoot_Click(object sender, EventArgs e)
        {
            Table table = lvHoaDon.Tag as Table;

            int idBill = HoaDonDAO.Instance.GetUnCheckHoaDonByTableID(table.ID);
            int idFood = (cbFood.SelectedItem as ThucAn).Id;
            int count = (int)nmFoodCount.Value;

            if (idBill == -1)
            {
                HoaDonDAO.Instance.InsertBill(table.ID);
                HoaDonInforDAO.Instance.InsertBillInfo(HoaDonDAO.Instance.GetMaxIdBill(), idFood, count);
            }    
            else
            {
                HoaDonInforDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }

            ShowHoaDon(table.ID);

            LoadTable();
        }

        private void btThanhToan_Click(object sender, EventArgs e)
        {
            Table table = lvHoaDon.Tag as Table;

            int idBill = HoaDonDAO.Instance.GetUnCheckHoaDonByTableID(table.ID);
            int discount = (int)nmGiamGia.Value;
            double tongTien = Convert.ToDouble(txbTongGia.Text.Split(',')[0]);
            double finalTongTien = tongTien - (tongTien / 100)*discount;

            if (idBill != -1)
            {
                if (MessageBox.Show(String.Format("Bạn có chắc thanh toán cho {0}\nSau giảm giá => {1} - ({1} / 100) x {2} = {3}", table.Name, tongTien, discount, finalTongTien), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    HoaDonDAO.Instance.CheckOut(idBill, discount, (float)finalTongTien);
                    ShowHoaDon(table.ID);

                    LoadTable();
                }    
            }
        }

        private void btChuyenBan_Click(object sender, EventArgs e)
        {


            int id1 = (lvHoaDon.Tag as Table).ID;
            int id2 = (cbChuyenBan.SelectedItem as Table).ID;
            if (MessageBox.Show(string.Format("Bạn có muốn chuyển bàn {0} qua bàn {1}", (lvHoaDon.Tag as Table).Name, (cbChuyenBan.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);

                LoadTable();
            }    
        }

        #endregion

    }
}
