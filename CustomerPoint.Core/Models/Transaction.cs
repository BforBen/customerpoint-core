using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerPoint.Models
{
    [Obsolete("Temporary fix", false)]
    public class Transaction
    {
        /// <summary>
        /// Transaction reference number
        /// </summary>
        public string Reference { get; set; }
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
        public string Account { get; set; }
        public string Description { get; set; }

        public string Fund { get; set; }
        public string Method { get; set; }
        public string Channel { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TransactionCharge { get; set; }
        [DataType(DataType.Currency)]
        public decimal? TotalAmount { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Vat { get; set; }

        public string CardType { get; set; }
        public string CardCreditDebit { get; set; }

        public string Name { get; set; }

        public string User { get; set; }

        public string OriginalReference { get; set; }
    }
}