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
        public void ShouldAcceptTwoValidArguments()
        {
            GivenANewHelper().AndTwoValidArguments();
            WhenValidateMethodInvoked();
            ThenFieldsArePopulatedCorrectly();
        }

        private void AndTwoValidArguments()
        {
            _arg1 = "c:\\schedaeromenu.xml";
            _arg2 = "/default.aspx";
            _args = new [] { _arg1, _arg2 };
        }

        [Test]
        public void FirstArgShouldThrowExceptionIfPathNotValid()
        {
            GivenANewHelper().AndAnInvalidPath();
            var del = new TestDelegate(WhenValidateMethodInvoked);
            Assert.Throws<ArgumentException>(del, _arg1);
        }

        private void ThenFieldsArePopulatedCorrectly()
        {
            _helper.Arg1.Should().Be(_arg1);
            _helper.Arg2.Should().Be(_arg2);
        }

        private void AndAnInvalidPath()
        {
            _arg1 = "a:\\setup.exe";
            _arg2 = "/default.aspx";
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
