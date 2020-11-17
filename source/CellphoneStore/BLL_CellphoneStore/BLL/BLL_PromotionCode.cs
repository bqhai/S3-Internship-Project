using DAL_CellPhoneStore.DAL;
using DAL_CellPhoneStore.Model;
using Model_CellphoneStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_CellPhoneStore.BLL
{
    public class BLL_PromotionCode
    {
        DAL_PromotionCode dalPromotionCode = new DAL_PromotionCode();
        DAL_Account dalAccount = new DAL_Account();
        public BLL_PromotionCode()
        {

        }
        public dynamic UsingPromotionCode(string code, string username, int totalPrice)
        {
            dynamic result;
            PromotionCode promotionCode = dalPromotionCode.GetPromotionCode(code);
            Account account = dalAccount.GetAccount(username);
            if (account != null)
            {
                bool checkUsed = dalPromotionCode.CheckUsedCode(code, username);
                if (promotionCode == null)
                {
                    result = "Mã không tồn tại";
                }
                else if (DateTime.Now > promotionCode.ExpiryDate)
                {
                    result = "Mã hết hạn sử dụng";
                }
                else if (DateTime.Now < promotionCode.StartDate)
                {
                    result = "Mã bắt đầu có hiệu lực từ ngày" + promotionCode.StartDate.ToString();
                }
                else if (checkUsed)
                {
                    result = "Bạn đã sử dụng mã này rồi";
                }
                else
                {
                    if (promotionCode.Value > 1 && totalPrice >= promotionCode.Require)
                    {
                        result = (int)promotionCode.Value;
                    }
                    else if (promotionCode.Value > 0 && totalPrice >= promotionCode.Require)
                    {
                        int tempPrice = (int)(totalPrice * promotionCode.Value);
                        if (tempPrice > promotionCode.Maximum)
                        {
                            result = promotionCode.Maximum;
                        }
                        else
                        {
                            result = tempPrice;
                        }
                    }
                    else
                    {
                        result = "Đơn hàng chưa đạt đủ điều kiện sử dụng";
                    }
                }
            }
            else
            {
                result = "Tài khoản không tồn tại";
            }            
            return result;
        }
        public bool AddPromotionCodeUsed(PromotionCodeUsedMapped promotionCodeUsedMapped)
        {
            try
            {
                dalPromotionCode.AddPromotionCodeUsed(promotionCodeUsedMapped.Code, promotionCodeUsedMapped.Username);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
