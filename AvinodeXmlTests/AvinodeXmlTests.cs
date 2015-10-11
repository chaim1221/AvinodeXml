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

        [Test]
        public void ShouldAcceptTwoValidArguments()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked();
            ThenFieldsContainingArgumentsArePopulatedCorrectly();
        }

        private void WithTwoValidArguments()
        {
            _arg1 = ".\\schedaeromenu.xml";
            _arg2 = "/default.aspx";
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
        public void ShouldBeAbleToParseAnXmlDocumentFromAFile()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked().AndParseXmlMethodInvoked();
            ThenHelperHasInstantiatedXmlDocumentField()
                .ThenHelperHasXmlContentInAFieldThatIgnoresWhitespace()
                .ThenResultingFieldIsOfType<XmlDocument>(_helper.XmlDocument);
        }

        [Test]
        public void ShouldBeAbleToEnumerateXmlFields()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked().AndParseXmlMethodInvoked();
            ThenHelperHasInstantiatedXmlNodeListField();
        }

        private void ThenHelperHasInstantiatedXmlNodeListField()
        {
            _helper.XmlNodeList.Should().NotBeNull();
        }

        private void ThenResultingFieldIsOfType<T>(XmlDocument xmlDocument)
        {
            xmlDocument.Should().BeOfType<T>();
        }

        private AvinodeXmlTests ThenHelperHasXmlContentInAFieldThatIgnoresWhitespace()
        {
            var xmlDoc = new XmlDocument { PreserveWhitespace = false };
            xmlDoc.Load(_arg1);
            _helper.XmlDocument.InnerXml.Should().Be(xmlDoc.InnerXml);
            return this;
        }

        private AvinodeXmlTests ThenHelperHasInstantiatedXmlDocumentField()
        {
            _helper.XmlDocument.Should().NotBeNull();
            return this;
        }

        private void AndParseXmlMethodInvoked()
        {
            _helper.ParseXml();
        }

        private void WithAnInvalidUri()
        {
            _arg1 = ".\\schedaeromenu.xml";
            _arg2 = "I'm a really poorly formed URI, but I'm a great sentence.";
        }

        private void ThenFieldsContainingArgumentsArePopulatedCorrectly()
        {
            _helper.FilePath.Should().Be(_arg1);
            _helper.RelativeUri.Should().Be(_arg2);
        }

        private void WithAnInvalidPath()
        {
            _arg1 = "a:\\setup.exe";
            _arg2 = "/default.aspx";
        }

        private AvinodeXmlTests WhenValidateMethodInvoked()
        {
            var args = new[] { _arg1, _arg2 };
            _helper.Validate(args);
            return this;
        }

        private AvinodeXmlTests GivenANewHelper()
        {
            _helper = new Helper();
            return this;
        }
    }
}

