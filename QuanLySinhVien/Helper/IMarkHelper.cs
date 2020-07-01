using QuanLySinhVien.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVien.Helper
{
    public interface IMarkHelper
    {
        List<Mark> GetMarks(int studentID);
      
    }
}
