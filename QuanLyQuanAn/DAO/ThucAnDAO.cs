using QuanLyQuanAn.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DAO
{
    public class ThucAnDAO
    {
        private static ThucAnDAO instance;

        public static ThucAnDAO Instance
        {
            get { if (instance == null) instance = new ThucAnDAO(); return ThucAnDAO.instance; }
            private set { ThucAnDAO.instance = value; }
        }

        private ThucAnDAO() { }

        public List<ThucAn> GetThucAnByIdDanhMuc(int id)
        {
            List<ThucAn> list = new List<ThucAn>();

            string query = "select * from MONAN where idLoaiMonAn = " + id;

            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                ThucAn thucan = new ThucAn(item);
                list.Add(thucan);
            }    

            return list;
        }
    }
}
