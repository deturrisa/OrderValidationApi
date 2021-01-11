using System;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Order.Validation.Service.Tests.Helpers
{
    public sealed class AutoMoqDataAttribute : AutoDataAttribute
    {
        public AutoMoqDataAttribute()
            : this(false)
        {
        }

        public AutoMoqDataAttribute(bool configureMembers)
            : this((Func<IFixture>)(() => new Fixture().Customize((ICustomization)new AutoMoqCustomization()
            {
                ConfigureMembers = configureMembers
            })))
        {
        }

        private AutoMoqDataAttribute(Func<IFixture> fixtureFactory)
            : base(fixtureFactory)
        {
        }
    }
}
