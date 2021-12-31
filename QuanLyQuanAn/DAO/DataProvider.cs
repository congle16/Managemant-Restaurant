using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    public class DataProvider
    {
        private static DataProvider instance; // Ctrl + R + E -> đóng gói // được tạo 1 lần và duy nhất
        // bất cứ thứ gì lấy ra thông qua instance là duy nhất
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; } // chỉ nội bộ class được set
        }

        private DataProvider() { }

        //chuỗi xác định kết nối
        private string conString = @"Data Source=.\sqlexpress;Initial Catalog=QuanLyQuanAn;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            //using sau khi sử dụng sẽ tự hủy con
            using (SqlConnection con = new SqlConnection(conString))//kết nối từ client xuống server
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }    
                    }    
                }    

                //trung gian thực hiện câu truy vấn lấy dữ liệu ra
                SqlDataAdapter adap = new SqlDataAdapter(cmd);

                adap.Fill(data);

                con.Close();
            }

            return data;
        }

        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            //using sau khi sử dụng sẽ tự hủy con
            using (SqlConnection con = new SqlConnection(conString))//kết nối từ client xuống server
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteNonQuery();
                con.Close();
            }

            return data; // trả về số dòng query thành công
        }

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            //using sau khi sử dụng sẽ tự hủy con
            using (SqlConnection con = new SqlConnection(conString))//kết nối từ client xuống server
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);

                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data = cmd.ExecuteScalar();// thực hiện query và trả về ô đầu tiên trong bảng
                con.Close();
            }

            return data;
        }
    }
    // lớp singleton - chỉ duy nhất tồn tại 1 thể hiện trong chương trình
}
