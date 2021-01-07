using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common
{
    public enum ValidationState
    {
        None = 0,
        Success = 1,
        UnsupportedCurrency = 2,
    }
}
