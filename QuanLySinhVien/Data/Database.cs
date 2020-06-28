using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace QuanLySinhVien.Data
{

    public class Account
    {
        [Key]
        public int ID { get; set; }

        public string UserName { get; set; }
    
        public string Password { get; set; }

        public DateTime RegisterDate { get; set; }

        [ForeignKey("Permission")]
        public int PermissionID { get; set; }
       
        public bool Status { get; set; }

        public Account()
        {
            Students = new HashSet<Student>();
        }

        public virtual ICollection<Student> Students { get; set; }
    }

    public class Semester
    {
        [Key]
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public Semester()
        {
            Marks = new HashSet<Mark>();
        }

        public virtual ICollection<Mark> Marks { get; set; }
    }
   
    public class Subjects
    {
        [Key]
        public int ID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Credits { get; set; }

        public int RateMidTerm { get; set; }

        public int RateFinalExam { get; set; }
        
        public Subjects()
        {
            Marks = new HashSet<Mark>();
        }

        public virtual ICollection<Mark> Marks { get; set; }
    }
   
    public class Mark
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("Account")]
        public int StudentID { get; set; }

        [ForeignKey("Semester")]
        public int SemesterID { get; set; }

        [ForeignKey("Subjects")]

        public int SubjectsID { get; set; }

        public int MarkMidTerm { get; set; }

        public int MarkFinalExam { get; set; }
        


        public virtual Semester Semester { get; set; }

        public virtual Subjects Subjects { get; set; }

        public virtual Student Student { get; set; }
    }
  
    public class Parent
    {
        [BindProperty]
        [Key]
        public int ParentID { get; set; }

        public string FatherName { get; set; }

        public string FatherJob { get; set; }

        public string FatherPhone { get; set; }

        public int? FatherBirthYear { get; set; }

        public string FatherAddress { get; set; }

        public string MotherName { get; set; }

        public string MotherJob { get; set; }

        public string MotherPhone { get; set; }

        public int? MotherBirthYear { get; set; }

        public string MotherAddress { get; set; }

        public bool Status { get; set; }

        public Parent()
        {
            Students = new HashSet<Student>();
        }

        public virtual ICollection<Student> Students { get; set; }
    }
 
    public class Permission
    {
        [Key]
        public int PerID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Permission()
        {
            Accounts = new HashSet<Account>();
        }

        public virtual ICollection<Account> Accounts { get; set; }
    }

    public class PermisionDetail
    {
        [Key]
        public int ID { get; set; }

        public string ActionName { get; set; }

        public string ActionCode { get; set; }

        [ForeignKey("Permission")]
        public int PermisstionID { get; set; }

        public virtual Permission Pesmission { get; set; }
    }

    public class Student

    {
        [BindProperty]
        [Key]
        public int ID { get; set; }

        [ForeignKey("Account")]
        public int AccountID { get; set; }

        public string StudentCode { get; set; }

        public string Name { get; set; }
       
        public int Sex
        {
            get;set;
        }

        public string Email { get; set; }

        public string Nation { get; set; }

        public string Religion { get; set; }

        public string Nationality { get; set; }

        public string Address { get; set; }

        public string Birthplace { get; set; }

        public string Phone { get; set; }

        public string CardID { get; set; }

        public DateTime DateOfCardID { get; set; }

        public string PlaceOfCardID { get; set; }

        public string HighShoolPlace { get; set; }

        public int GraduationYear { get; set; }

        public string PolicyChild { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastModify { get; set; }

        public string Contact { get; set; }

        [ForeignKey("Parent")]
        public int ParentID { get; set; }

        public bool Status { get; set; }

        public Student()
        {
            Marks = new HashSet<Mark>();
        }

        public virtual Account Account { get; set; }

        public virtual Parent Parent { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
    }
}
