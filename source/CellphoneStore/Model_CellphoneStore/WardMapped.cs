﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class WardMapped
    {
        public WardMapped()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string LatiLongTude { get; set; }
        public int DistrictID { get; set; }
        public int SortOrder { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<bool> IsDeleted { get; set; }

        
    }
}
