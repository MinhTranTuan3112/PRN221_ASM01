﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTOs.Order
{
    public class CreateOrderDto
    {

        public int MemberId { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public decimal? Freight { get; set; }

        public string Status { get; set; } = null!;
    }
}
