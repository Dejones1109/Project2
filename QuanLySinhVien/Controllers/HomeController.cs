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
        [HttpGet]
        public IActionResult Details()
        {
            string UserName = User.Identity.Name;
            if (UserName == null)
            {
                return NotFound();
            }

            return View(_studentHelper.GetStudent(UserName));
        }
       [HttpGet]
        public async Task<IActionResult> Info(int ID)
        {
           // string UserName = User.Identity.Name;
           
            var student = await _context.Students.Include(m=>m.Parent).FirstOrDefaultAsync(m => m.ID == ID);
          

            return View(student);
        }
        public async Task<IActionResult> EditAsync(int ID)
        {
            var student = await _context.Students.Include(m => m.Parent).SingleOrDefaultAsync(m => m.ID == ID);
            return View(student);
        }
       
        [Authorize(Roles = "Admin")]
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
        [HttpGet]
        public float AverageMark(int MarkMidTerm, int MarkFinalExam, int RateMidTerm, int RateFinalExam)
        {
      
          return  ((MarkMidTerm * RateMidTerm + MarkFinalExam * RateFinalExam) / 10);
        }
     
        [Authorize(Roles = "User")]
      
        public IActionResult UpdateSV()
        {           
            return View(_studentHelper.GetStudent(User.Identity.Name));
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public  async Task<IActionResult> UpdateSV(Student student) 
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e){
                    Console.WriteLine(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
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
