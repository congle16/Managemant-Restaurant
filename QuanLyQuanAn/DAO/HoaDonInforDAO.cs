using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    public class HoaDonInforDAO
    {
        private static HoaDonInforDAO instance;

        public static HoaDonInforDAO Instance
        {
            get { if (instance == null) instance = new HoaDonInforDAO(); return HoaDonInforDAO.instance; }
            private set { HoaDonInforDAO.instance = value; }
        }

        private HoaDonInforDAO() { }

        public List<HoaDonInfor> GetListHoaDonInfor (int id)// id của hóa đơn
        {
            List<HoaDonInfor> listHoaDonInfor = new List<HoaDonInfor>();

            DataTable data = DataProvider.Instance.ExecuteQuery("select * from HOADONinfor where idHoaDon = " + id);

            foreach (DataRow item in data.Rows)
            {
                HoaDonInfor infor = new HoaDonInfor(item);
                listHoaDonInfor.Add(infor);
            }    

            return listHoaDonInfor;
        }

        public void InsertBillInfo(int idBill, int idFood, int count)
        {
            DataProvider.Instance.ExecuteNonQuery("USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill, idFood, count });
        }
    }
}
