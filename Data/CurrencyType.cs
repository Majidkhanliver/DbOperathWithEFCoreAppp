﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbOperathWithEFCoreAppp.Data
{
    public class CurrencyType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public ICollection<BookPrice> BookPrices { get; set; }
    }
}
