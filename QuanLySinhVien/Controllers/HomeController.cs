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
using System.Collections.Immutable;
using Microsoft.AspNetCore.Components.Forms;

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
       
        [Authorize(Roles = "User")]
        public IActionResult BangDiemCaNhan(int studentID)
        {
            List<BangDiemCaNhanViewModel> marks = new List<BangDiemCaNhanViewModel>();
            if (studentID == 0)
            {
                studentID = _studentHelper.GetStudent(User.Identity.Name).ID;
                var data = _markHelper.GetMarks(studentID);
               
                foreach (Mark i in data)
                {
                    BangDiemCaNhanViewModel viewModel = new BangDiemCaNhanViewModel();
                    viewModel.Mark = i;
                    double average = (double)(i.MarkMidTerm * (i.Subjects.RateMidTerm / 10f) + i.MarkFinalExam * (i.Subjects.RateFinalExam / 10f));
                    viewModel.MarkAverage = Math.Round(average,2);
                    if (viewModel.MarkAverage >= 8.5)
                    {
                        viewModel.markWork = MarkWork.A;
                   
                    }else if(viewModel.MarkAverage >= 7.5&& viewModel.MarkAverage < 8.5)
                    {
                        viewModel.markWork = MarkWork.B;
                       
                    }
                    else if (viewModel.MarkAverage >= 6.5 && viewModel.MarkAverage < 7.5)
                    {
                        viewModel.markWork = MarkWork.C;
                     
                    }
                    else if (viewModel.MarkAverage >= 5.5 && viewModel.MarkAverage < 6.5)
                    {
                        viewModel.markWork = MarkWork.D;
                       
                    }
                    else if (viewModel.MarkAverage >= 4.0 && viewModel.MarkAverage < 5.5)
                    {
                        viewModel.markWork = MarkWork.E;
                    
                    }
                    else
                    {
                        viewModel.markWork = MarkWork.F;
                     
                    }
                    marks.Add(viewModel);
                  
                }
             
            }
            return View(marks.ToList());
        }
               

        [Authorize(Roles = "User")]
        public IActionResult BangDiemTongKet(int studentID)
        {
            BangDiemTongKetViewModel viewModel = new BangDiemTongKetViewModel();
          
            
            if (studentID == 0)
            {
             
                studentID = _studentHelper.GetStudent(User.Identity.Name).ID;
                var data = _markHelper.GetMarks(studentID);
                double sum = 0;
                int count = 0;
                foreach (Mark i in data)
                {
                    BangDiemCaNhanViewModel viewModels = new BangDiemCaNhanViewModel();
                    viewModels.Mark = i;
                   
                    sum += (i.MarkMidTerm * (i.Subjects.RateMidTerm / 10f) + i.MarkFinalExam * (i.Subjects.RateFinalExam / 10f))*i.Subjects.Credits;
                    count += i.Subjects.Credits;
                    viewModel.Semester = 0;
                    

                }
                viewModel.GPA = Math.Round(sum / count, 2);
                viewModel.PassedCredits = count;
                


            }
            return View(viewModel);
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
              
                    student.DateOfCardID = DateTime.Now;
                    _context.Update(student);
            //        Console.WriteLine(student);
                  await _context.SaveChangesAsync();
            
            }
            return RedirectToAction(nameof(Index));
          
           
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
