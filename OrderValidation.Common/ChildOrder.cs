﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OrderValidation.Common
{
    public class ChildOrder
    {
        private decimal _weight;

        [Range(double.Epsilon, 1.0)]
        public decimal Weight
        {
            get => Math.Round(_weight, 2);
            set => _weight = value;
        }
    }
}
