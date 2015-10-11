using System;
using System.Xml;
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
        private string _validXml;

        [Test]
        public void ShouldAcceptTwoValidArguments()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked();
            ThenFieldsContainingArgumentsArePopulatedCorrectly();
        }

        private AvinodeXmlTests WithTwoValidArguments()
        {
            _arg1 = ".\\schedaeromenu.xml";
            _arg2 = "/default.aspx";
            _args = new [] { _arg1, _arg2 };
            return this;
        }

        [Test]
        public void FirstArgShouldThrowExceptionIfPathNotValid()
        {
            GivenANewHelper().WithAnInvalidPath();
            var del = new TestDelegate(() => WhenValidateMethodInvoked());
            Assert.Throws<ArgumentException>(del, _arg1);
        }

        [Test]
        public void SecondArgShouldThrowExceptionIfUriNotWellFormed()
        {
            GivenANewHelper().WithAnInvalidUri();
            var del = new TestDelegate(() => WhenValidateMethodInvoked());
            Assert.Throws<ArgumentException>(del, _arg2);
        }

        [Test]
        public void ShouldBeAbleToParseAnXmlDocument()
        {
            GivenANewHelper().WithTwoValidArguments().WithValidXml();
            WhenValidateMethodInvoked().AndParseXmlMethodInvoked();
            ThenHelperHasInstantiatedXmlField();
        }

        private void ThenHelperHasInstantiatedXmlField()
        {
            _helper.XmlStuff.Should().NotBeNull();
            _helper.XmlStuff.Should().Be(_validXml);
            _helper.XmlStuff.Should().BeOfType<XmlDocument>();
        }

        private void AndParseXmlMethodInvoked()
        {
            _helper.ParseXml(_validXml);
        }

        private void WithValidXml()
        {
            _validXml = @"<?xml version=""1.0""?> 
                               <catalog>
                                   <book id = ""bk101"" />
                               </catalog>
            ";
        }

        private void WithAnInvalidUri()
        {
            _arg1 = ".\\schedaeromenu.xml";
            _arg2 = "I'm a really poorly formed URI, but I'm a great sentence.";
            _args = new[] { _arg1, _arg2 };
        }

        private void ThenFieldsContainingArgumentsArePopulatedCorrectly()
        {
            _helper.Arg1.Should().Be(_arg1);
            _helper.Arg2.Should().Be(_arg2);
        }

        private void WithAnInvalidPath()
        {
            _arg1 = "a:\\setup.exe";
            _arg2 = "/default.aspx";
            _args = new [] { _arg1, _arg2 };
        }

        private AvinodeXmlTests WhenValidateMethodInvoked()
        {
            _helper.Validate(_args);
            return this;
        }

        private AvinodeXmlTests GivenANewHelper()
        {
            _helper = new Helper();
            return this;
        }
    }
}
