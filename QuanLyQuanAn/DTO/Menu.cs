using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    public class Menu
    {
        public Menu(string thucan, int soluong, float gia, float tongGia = 0)
        {
            this.Thucan = thucan;
            this.Soluong = soluong;
            this.Gia = gia;
            this.TongGia = tongGia;
        }

        public Menu(DataRow row)
        {
            this.Thucan = row["name"].ToString();
            this.Soluong = (int)row["soluong"];
            this.Gia = (float)Convert.ToDouble(row["gia"].ToString());
            this.TongGia = (float)Convert.ToDouble(row["tongGia"].ToString());
        }

        private string thucan;
        private int soluong;
        private float gia;
        private float tongGia;

        public string Thucan { get => thucan; set => thucan = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public float Gia { get => gia; set => gia = value; }
        public float TongGia { get => tongGia; set => tongGia = value; }
    }
}
