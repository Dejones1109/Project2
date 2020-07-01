using QuanLySinhVien.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVien.Models
{
    public class BangDiemCaNhanViewModel
    {
        public Mark Mark { get; set; }

        public double MarkAverage { get; set; }
        public MarkWork markWork { get; set; }

         
    }
    public enum MarkWork : Int16
    {
        A =1,
        B =2,
        C =3,
        D =4,
        E=5,
        F =6


    }

}
