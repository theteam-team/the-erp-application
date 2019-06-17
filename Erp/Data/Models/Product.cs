﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Erp.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public class Product
    {
        [Required]
        public string id;
        [Required]
        public string name;
        public string description;
        [Required]
        public double price;
        public double weight;
        public double length;
        public double width;
        public double height;
        [Required]
        public uint unitsInStock;
        public uint sold;
        public uint purchased;
    }
}
