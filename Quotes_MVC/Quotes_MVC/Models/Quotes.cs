using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quotes_MVC.Models
{
    public class Quotes
    {
        public int Id { get; set; }
        public string QuoteAuther { get; set; }
        public string QuoteText { get; set; }

        public Quotes()
        {

        }
    }
}
