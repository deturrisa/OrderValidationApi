using System;
using System.Collections.Generic;
using System.Text;

namespace OrderValidation.Common.Destination
{
    public interface IDestinationValidation
    {
        ValidationState ValidateDestination(string destination);
    }
}
