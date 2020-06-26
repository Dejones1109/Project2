using Microsoft.EntityFrameworkCore;
using QuanLySinhVien.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVien.Helper
{
    public class StudentHelper : IStudentHelper
    {
        private readonly QLSVContext db;
        private readonly IAccountHelper accountHelper;

        public StudentHelper(QLSVContext _db, IAccountHelper _accountHelper)
        {
            db = _db;
            accountHelper = _accountHelper;
        }

        public List<Student> GetStudents()
        {
            return db.Students.ToList();
        }
        
        public Student GetStudent(string userName)
        {
            var account = accountHelper.GetAccountByUserName(userName);
            if (account != null)
            {
                return db.Students.Include(e => e.Parent).FirstOrDefault(x => x.AccountID == account.ID);
            }
            return null;
        }
        public Student GetStudentID(int StudentID)
        {
            return db.Students.SingleOrDefault(s => s.ID == StudentID);
        }
    }
}
