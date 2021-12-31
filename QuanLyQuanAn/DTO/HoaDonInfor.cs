using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    public class HoaDonInfor
    {
        public HoaDonInfor(int id, int idHoaDon, int idMonAn, int soLuong)
        {
            this.Id = id;
            this.IdHoaDon = idHoaDon;
            this.IdMonAn = idMonAn;
            this.SoLuong = soLuong;
        }

        public HoaDonInfor(DataRow row)
        {
            this.Id = (int)row["id"];
            this.IdHoaDon = (int)row["idHoaDon"];
            this.IdMonAn = (int)row["idMonAn"];
            this.SoLuong = (int)row["soluong"];
        }

        int id;
        int idHoaDon;
        int idMonAn;
        int soLuong;

        public int Id { get => id; set => id = value; }
        public int IdHoaDon { get => idHoaDon; set => idHoaDon = value; }
        public int IdMonAn { get => idMonAn; set => idMonAn = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
    }
}
