using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    // nhiệm vụ là lấy ra hóa đơn từ id bàn
    public class HoaDonDAO
    {
        private static HoaDonDAO instance;

        public static HoaDonDAO Instance
        {
            get { if (instance == null) instance = new HoaDonDAO(); return HoaDonDAO.instance; }
            private set { HoaDonDAO.instance = value; }
        }// cấu trúc singleton
        private HoaDonDAO() { }

        /// <summary>
        /// Thành công: id hoá đơn
        /// Thất bại: -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public int GetUnCheckHoaDonByTableID (int id)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("select * from HOADON where idBan = " + id + "and trangthai = 0");
        
            if (data.Rows.Count > 0)
            {
                HoaDon hoaDon = new HoaDon(data.Rows[0]);
                return hoaDon.ID1;
            }

            return -1;
        }

        public void CheckOut(int id, int discount, float tongGia)
        {
            //" , tongTien = " + tongGia +
            string query = "update HOADON set ngayCheckOut = getdate(), trangthai = 1 , discount = " + discount + " where id = " + id;
            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("exec USP_InsertBill @idTable", new object[]{id});
        }

        public int GetMaxIdBill()
        {
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar("select max(id) from HOADON");
            } catch
            {
                return 1;
            }
        }
    } 
}
