using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using System.Net.WebSockets;
using System.Threading.Tasks;
using QuanLySinhVien.Helper;


namespace CookieDemo.Controllers
{

    public class HomeController : Controller
    {
        public readonly QLSVContext _context;
        public readonly IStudentHelper _studentHelper;
        public readonly IMarkHelper _markHelper;
     
        public HomeController(QLSVContext context, IStudentHelper studentHelper,IMarkHelper markHelper)
        {
            _context = context;
            _studentHelper = studentHelper;
            _markHelper = markHelper;
        }
      
        

        [Authorize(Roles = "User,Admin")]

        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
              //  đưa ra list danh sách sinh  viên
                return View(_studentHelper.GetStudents());
            }
            else
            {
            
                return Redirect("Home/Details");
            }
          
        }

        //public IActionResult Details(string UserName)
        //{
        //    UserName = User.Identity.Name;
        //    if (UserName == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(_studentHelper.GetStudent(UserName));
        //}

        public IActionResult Details()
        {
            string UserName = User.Identity.Name;
            if (UserName == null)
            {
                return NotFound();
            }

            return View(_studentHelper.GetStudent(UserName));
        }


        [Authorize(Roles = "Admin,User")]
        public IActionResult BangDiemCaNhan(int studentID)
        {
            if (studentID == 0)
            {
                studentID = _studentHelper.GetStudent(User.Identity.Name).ID;
            }    
            return View(_markHelper.GetMarks(studentID));
        }
     

        [Authorize(Roles = "User")]
        public IActionResult BangDiemHocPhan(int studentID)
        {


            if (studentID == 0)
            {
                studentID = _studentHelper.GetStudent(User.Identity.Name).ID;
            }
            return View(_markHelper.GetMarks(studentID));
        }
        [Authorize(Roles = "User")]
       public IActionResult UpdateSV()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult UpdateSV(Student students,Parent parent)
        {
            if (ModelState.IsValid)
            {
               // MSSV = User.Identity.Name;
               // Console.WriteLine(MSSV);
               // var data = _context.Set<Student>().FromSql("INSERT INTO dbo.Students(mssv,ho_va_ten,gioi_tinh,dan_toc,ton_giao,dien_thoai,cmnd,ngay_cap_cmnd,noi_cap_cmnd,nam_cap3_tn,truong_thpt,noi_sinh,dia_chi,doi_tuong,dia_chi_can_thiet,ngay_tao,ngay_cap_nhat)" +
               //     "value(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)").First();
               //// var data2 =_context.Set<Students>()
               // Console.WriteLine(data);
               // var data1 = _context.Set<Parent>().FromSql("INSERT INTO dbo.parent(ho_va_ten_bo,nam_sinh_bo,sdt_bo,nghe_nghiep_bo,dia_chi_bo,ho_va_ten_me,nam_sinh_me,sdt_me,nghe_nghiep_me,dia_chi_me,ngay_tao,ngay_cap_nhap)value(?,?,?,?,?,?,?,?,?,?,?)").FirstOrDefault();
               // //    var data2 = _context.Set<parent_student>().FromSql("INSERT INTO parent_students(mssv,)");
               // Console.WriteLine(data1);
               // Console.WriteLine(students.ho_va_ten);
               // //Dữ liệu bảng student
               // data.mssv = MSSV;
               // data.ho_va_ten = students.ho_va_ten;
               // data.gioi_tinh = students.gioi_tinh;
               // data.dan_toc = students.dan_toc;
               // data.quoc_tich = students.quoc_tich;
               // data.nam_cap3_tn = Convert.ToInt32(students.nam_cap3_tn);
               // data.noi_sinh = students.noi_sinh;
               // data.truong_thpt = students.truong_thpt;
               // data.doi_tuong = students.doi_tuong;
               // data.cmnd = students.cmnd;
               // data.ngay_cap_cmnd = Convert.ToDateTime(students.ngay_cap_cmnd);
                
               // data.dia_chi_can_thiet = students.dia_chi_can_thiet;
               // //   

               // //   dữ liệu bảng parent
                
               // data1.ho_va_ten_bo = parent.ho_va_ten_bo;
               // data1.nam_sinh_bo = Convert.ToInt32(parent.nam_sinh_bo);
               // data1.nghe_nghiep_bo = parent.nghe_nghiep_bo;
               // data1.sdt_bo = parent.sdt_bo;
               // data1.dia_chi_bo = parent.dia_chi_bo;
               // data1.ho_va_ten_me = parent.ho_va_ten_me;
               // data1.nam_sinh_me = Convert.ToInt32(parent.nam_sinh_me);
               // data1.nghe_nghiep_me = parent.nghe_nghiep_me;
               // data1.sdt_me = parent.sdt_me;
               // data1.dia_chi_me = parent.dia_chi_me;
               // //thêm vào 2  bảng 
               // _context.Add(students);
               // _context.Add(parent);
               // //save vào  bảng 
               // _context.SaveChanges();
               

               // return RedirectToAction("Index", "Home");
            }
            return View();
       
        }
      /*  public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<IActionResult> Register([Bind("mssv,email,password,cpassword")] Accounts accounts)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                String.Format("{0:d/M/yyyy}", now);
                if (accounts.password == accounts.cpassword)
                {
                    accounts.ngay_dang_ki = Convert.ToDateTime(now);
                    _context.Add(accounts);
                    _context.SaveChanges();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    return RedirectToAction("Register", "Home");
                }
            }
            return View();
        }
        */


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
