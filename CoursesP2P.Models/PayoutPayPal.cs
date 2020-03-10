﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CoursesP2P.Models
{
    public class PayoutPayPal
    {
        public int Id { get; set; }

        public string BatchId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
