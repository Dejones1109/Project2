using QuanLySinhVien.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLySinhVien.Helper
{
    public class AccountHelper : IAccountHelper
    {
        private readonly QLSVContext db;

        public AccountHelper(QLSVContext _db)
        {
            db = _db;
        }

        public bool Login(string userName, string pass)
        {
            return db.Accounts.Any(x => x.UserName.Trim() == userName.Trim() && x.Password.Trim() == pass.Trim());
        }

        public Account GetAccountByLogin(string userName, string pass)
        {
            return db.Accounts.SingleOrDefault(x => x.UserName.Trim() == userName.Trim() && x.Password.Trim() == pass.Trim());
        }
       
        public Account GetAccountByUserName(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.UserName.Trim() == userName.Trim());
        }
    }
}
