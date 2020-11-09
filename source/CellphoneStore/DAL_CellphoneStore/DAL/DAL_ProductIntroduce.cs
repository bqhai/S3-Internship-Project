﻿using DAL_CellPhoneStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CellPhoneStore.DAL
{
    public class DAL_ProductIntroduce
    {
        DbCellPhoneStoreEntities db = new DbCellPhoneStoreEntities();
        public DAL_ProductIntroduce()
        {

        }
        public ProductIntroduce GetProductIntroduceByID(string productID)
        {
            return db.ProductIntroduces.Where(prdi => prdi.ProductID == productID).FirstOrDefault();
        }
    }
}
