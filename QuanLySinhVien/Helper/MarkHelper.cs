using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace QuanLySinhVien.Helper
{
    public class MarkHelper : IMarkHelper
    {
        public readonly QLSVContext db;
        public IStudentHelper studentHelper;

        public MarkHelper(QLSVContext _db, IStudentHelper _studentHelper)
        {
            db = _db;
            studentHelper = _studentHelper;
        }

        public List<Mark> GetMarks(int studentID)
        {
            //var student = studentHelper.GetStudentID(StudentID);
            if (db.Students.Any(x => x.ID == studentID))
            {
                return db.Marks.Include(e => e.Semester).Include(e => e.Subjects).Where(x => x.StudentID == studentID).ToList();
                //return db.Students.Include(e => e.Marks).Single(x => x.ID == studentID).Marks.ToList();
            }
            return null;
        }
       
    }
}

