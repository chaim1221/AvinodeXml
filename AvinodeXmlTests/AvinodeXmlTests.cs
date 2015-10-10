using AvinodeXmlParser;
using FluentAssertions;
using NUnit.Framework;

namespace AvinodeXmlTests
{
    [TestFixture]
    public class AvinodeXmlTests
    {
        Helper _helper;

        [Test]
        public void ShouldAcceptTwoArguments()
        {
            GivenANewHelper();
            WhenValidateMethodInvoked().WithTwoArguments();
            ThenFieldsArePopulatedCorrectly();
        }

        private void ThenFieldsArePopulatedCorrectly()
        {
            "this".Should().Be("this");
            _helper.arg1.Should().Be("arg1");
            _helper.arg2.Should().Be("arg2");
        }

        private void WithTwoArguments()
        {
            var args = new [] { "arg1", "arg2" };
        }

        private AvinodeXmlTests WhenValidateMethodInvoked()
        {
            return this;
        }

        private void GivenANewHelper()
        {
            _helper = new Helper();
        }
    }
}
