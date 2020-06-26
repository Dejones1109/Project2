using QuanLySinhVien.Data;
using System.Collections.Generic;

namespace QuanLySinhVien.Helper
{
    public interface IStudentHelper
    {
        List<Student> GetStudents();
        Student GetStudent(string userName);
        Student GetStudentID(int StudentID);
    }
}