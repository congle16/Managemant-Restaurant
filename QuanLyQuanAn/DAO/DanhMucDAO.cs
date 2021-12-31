using QuanLyQuanAn.DAO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    public class DanhMucDAO
    {
        private static DanhMucDAO instance;

        public static DanhMucDAO Instance
        {
            get { if (instance == null) instance = new DanhMucDAO(); return DanhMucDAO.instance; }
            private set { DanhMucDAO.instance = value; }
        }

        private DanhMucDAO() { }

        public List<DanhMuc> GetListDanhMuc()
        {
            List<DanhMuc> list = new List<DanhMuc>();

            string query = "select * from LOAIMONAN";

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                DanhMuc danhmuc = new DanhMuc(item);
                list.Add(danhmuc);
            }    

            return list;
        }
    }
}
