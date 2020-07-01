using Microsoft.EntityFrameworkCore.Internal;
using QuanLySinhVien.Data;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace QuanLySinhVien.Models
{
    public  class Handler
    {
   
        public Mark mark;
        
        public float MarkAverage {
            get => this.mark.MarkMidTerm * (this.mark.Subjects.RateMidTerm / 10) +
                this.mark.MarkFinalExam * (this.mark.Subjects.RateFinalExam / 10);
            set{ }
         }
        
    }    
}
