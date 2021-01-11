using AutoFixture.Xunit2;
using System;
using System.Collections.Generic;
using System.Text;
using Order.Validation.Service.Tests.Helpers;
using Xunit;
using Xunit.Sdk;

namespace OrderValidation.Service.Validation.Tests
{
    public sealed class InlineAutoMoqDataAttribute : CompositeDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values)
            : base((DataAttribute)new InlineDataAttribute(values), (DataAttribute)new AutoMoqDataAttribute())
        {
        }
    }
}
