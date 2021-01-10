using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common.Type
{
    public interface ITypeValidation
    {
        ValidationState ValidateType(OrderType orderType);
    }
}
