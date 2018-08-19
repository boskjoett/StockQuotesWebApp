using System;
using System.ComponentModel.DataAnnotations;

namespace StockQuotesWebApp.Models
{
    public class StockApiRequest
    {
        [Display(Name = "Stock ID")]
        [Required]
        public string StockId { get; set; }

        [Display(Name = "Stock Exchange")]
        [Required]
        public string StockExchange { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
//        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime FromDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
//        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Required]
        public DateTime ToDate { get; set; }
    }
}
