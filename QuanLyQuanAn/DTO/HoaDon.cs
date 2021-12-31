using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanAn.DTO
{ 
    public class HoaDon
    {
        public HoaDon(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int trangthai, int discount = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Trangthai = trangthai;
            this.Discount = discount;
        }

        public HoaDon(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["ngayCheckIn"];

            var dataCheckOutTemp = row["ngayCheckOut"];
            if (dataCheckOutTemp.ToString() != "")
                this.DateCheckOut = (DateTime?)dataCheckOutTemp;

            this.Trangthai = (int)row["trangthai"];

            if (row["discount"].ToString() != "")
                this.Discount = (int)row["discount"];
        }

        private int trangthai;
        private DateTime? dateCheckIn; // thêm ? thì kiểu dữ liệu có thể null được
        private DateTime? dateCheckOut;
        private int ID;
        private int discount; // giảm giá

        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Trangthai { get => trangthai; set => trangthai = value; }
        public int ID1 { get => ID; set => ID = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}
