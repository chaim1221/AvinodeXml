using System;
using AvinodeXmlParser;
using FluentAssertions;
using NUnit.Framework;

namespace AvinodeXmlTests
{
    [TestFixture]
    public class AvinodeXmlTests
    {
        Helper _helper;
        private string _arg1;
        private string _arg2;
        private string[] _args;

        [Test]
        public void ShouldAcceptTwoArguments()
        {
            GivenANewHelper().AndTwoRandomArguments();
            WhenValidateMethodInvoked();
            ThenFieldsArePopulatedCorrectly();
        }

        [Test]
        public void FirstArgShouldBeAValidPath()
        {
            GivenANewHelper().AndTwoRandomArguments();
            var del = new TestDelegate(WhenValidateMethodInvoked);
            Assert.Throws<ArgumentException>(del);
        }

        private void ThenFieldsArePopulatedCorrectly()
        {
            _helper.Arg1.Should().Be(_arg1);
            _helper.Arg2.Should().Be(_arg2);
        }

        private void AndTwoRandomArguments()
        {
            _arg1 = "arg1";
            _arg2 = "arg2";
            _args = new [] { _arg1, _arg2 };
        }

        private void WhenValidateMethodInvoked()
        {
            _helper.Validate(_args);
        }

        private AvinodeXmlTests GivenANewHelper()
        {
            _helper = new Helper();
            return this;
        }
    }
}
