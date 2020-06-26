using QuanLySinhVien.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVien.Data
{
    public class QLSVContext: DbContext
    {
        public QLSVContext(DbContextOptions<QLSVContext> options)
         : base(options)
        {
        }


        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Parent> Parents { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermisionDetail> PermisionDetails { get; set; }
    }
}
