using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_PromotionCode
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_PromotionCode()
        {

        }
        public PromotionCode GetPromotionCode(string code)
        {
            return db.PromotionCodes.Where(prc => prc.Code == code).FirstOrDefault();
        }
        public bool CheckUsedCode(string code, string username)
        {
            PromotionCodeUsed promotionCodeUsed = db.PromotionCodeUseds.Where(prcu => prcu.Code == code && prcu.Username == username).FirstOrDefault();
            if(promotionCodeUsed != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }           
        public void AddPromotionCodeUsed(string code, string username)
        {
            PromotionCodeUsed promotionCodeUsed = new PromotionCodeUsed()
            {
                Code = code,
                Username = username,
                UsedDate = DateTime.Now
            };
            db.PromotionCodeUseds.Add(promotionCodeUsed);
            db.SaveChanges();
        }
    }
}
