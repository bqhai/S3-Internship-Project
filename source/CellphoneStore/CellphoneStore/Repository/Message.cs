using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CellphoneStore.Repository
{
    public static class Message
    {
        public static string ConnectFail()
        {
            return "Kết nối server thất bại";
        }
        public static string AuthenticateFail()
        {
            return "Tài khoản hoặc mật khẩu không chính xác";
        }
        public static string RegisterSuccess()
        {
            return "Đăng ký thành công!";
        }
        public static string RegisterFail()
        {
            return "Đăng ký thất bại";
        }
        public static string UserAlreadyExists()
        {
            return "Tài khoản này đã tồn tại";
        }
        public static string EmailNotExists()
        {
            return "Email không tồn tại";
        }
        public static string SendVerifyCodeFail()
        {
            return "Lỗi gửi mã xác thực.";
        }
    }
}