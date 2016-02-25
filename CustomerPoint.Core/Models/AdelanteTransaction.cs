using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerPoint.Models
{

    [Obsolete("Temp class", false)]
    public class AdelanteTransaction
    {
        /// <summary>
        /// Transaction reference number
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Transaction date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Date transcation was recorded
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime Recorded { get; set; }

        public IEnumerable<string> Account { get; set; }
        public string Description { get; set; }

        public string Fund { get; set; }
        public string Method { get; set; }
        public string Channel { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Vat { get; set; }
        public string User { get; set; }
    }
}