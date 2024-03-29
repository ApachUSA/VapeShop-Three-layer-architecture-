﻿using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VapeShop.Domain.Enum;

namespace VapeShop.Domain.Entity.Product
{
    public class Nicotine
    {
        public int NicotineID { get; set; }

        public NicotineType Nicotine_type { get; set; }

        public double Nicotine_concentration { get; set; }

        public decimal Additional_price { get; set; }

		public ICollection<Liquid_param>? Liquid_Params { get; set; }

	}
}
