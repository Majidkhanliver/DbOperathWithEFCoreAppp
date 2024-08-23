using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DbOperathWithEFCoreAppp.Data
{
    public class BookPrice
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int Amount { get; set; }
        public int CurrencyTypeId { get; set; }


        public CurrencyType CurrencyTypes { get; set; }
        public Book Book { get; set; }
    }
}
