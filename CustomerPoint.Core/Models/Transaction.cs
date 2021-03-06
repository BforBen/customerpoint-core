﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CustomerPoint.Models
{
    public class Transaction
    {
        /// <summary>
        /// Transaction reference number
        /// </summary>
        [Display(Name = "Reference")]
        public string Id { get; set; }
        /// <summary>
        /// Transaction date
        /// </summary>
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime Date { get; set; }

        public IEnumerable<string> Account { get; set; }
        public string Description { get; set; }

        [Display(Name = "Ledger code")]
        public string LedgerCode { get; set; }
        public string Method { get; set; }
        public string Channel { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Amount { get; set; }
        [DataType(DataType.Currency)]
        [Display(Name = "VAT")]
        public decimal? Vat { get; set; }
        public string User { get; set; }
    }
}