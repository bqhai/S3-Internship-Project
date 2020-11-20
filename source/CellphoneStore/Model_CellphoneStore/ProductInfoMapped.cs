﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_CellphoneStore
{
    public class ProductInfoMapped
    {
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string BrandID { get; set; }
        public string BrandName { get; set; }
        public string Country { get; set; }
        public string Size { get; set; }
        public string Weight { get; set; }
        public string SIM { get; set; }
        public string ScreenType { get; set; }
        public Nullable<double> ScreenSize { get; set; }
        public string ScreenResolution { get; set; }
        public string OperatingSystem { get; set; }
        public string Chipset { get; set; }
        public string CPU { get; set; }
        public string GPU { get; set; }
        public string SDCard { get; set; }
        public string BackCamera { get; set; }
        public string FrontCamera { get; set; }
        public string WLAN { get; set; }
        public string Bluetooth { get; set; }
        public string GPS { get; set; }
        public string NFC { get; set; }
        public string USB { get; set; }
        public string Sensor { get; set; }
        public string Battery { get; set; }
        public string Image { get; set; }
    }
}
