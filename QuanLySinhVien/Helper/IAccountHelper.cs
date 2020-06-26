using QuanLySinhVien.Data;

namespace QuanLySinhVien.Helper
{
    public interface IAccountHelper// t không rõ phần này ô có thể giải thích k

    {
        bool Login(string userName, string pass);
        Account GetAccountByLogin(string userName, string pass);
        Account GetAccountByUserName(string userName);
    }
}