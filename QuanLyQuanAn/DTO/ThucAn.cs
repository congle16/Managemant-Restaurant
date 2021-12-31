using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{
    public class ThucAn
    {
        public ThucAn(int id, string name, int danhmucID, float gia)
        {
            this.Id = id;
            this.Name = name;
            this.IdDanhMuc = danhmucID;
            this.Gia = gia;
        }

        public ThucAn(DataRow row)
        {
            this.Id = (int) row["id"];
            this.Name = row["name"].ToString();
            this.IdDanhMuc = (int) row["idLoaiMonAn"];
            this.Gia = (float) Convert.ToDouble(row["gia"].ToString());
        }

        private int id;
        private int idDanhMuc;
        private string name;
        private float gia;

        public int Id { get => id; set => id = value; }
        public int IdDanhMuc { get => idDanhMuc; set => idDanhMuc = value; }
        public string Name { get => name; set => name = value; }
        public float Gia { get => gia; set => gia = value; }
    }
}
