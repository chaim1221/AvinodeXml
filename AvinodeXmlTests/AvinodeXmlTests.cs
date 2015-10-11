using System;
using System.Collections.Generic;
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
        private XmlNodeList _then;
        private List<AvinodeMenuItem> _result;

        [Test]
        public void MethodValidateShouldAcceptTwoValidArguments()
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
        public void MethodValidateFirstArgShouldThrowExceptionIfPathNotValid()
        {
            GivenANewHelper().WithAnInvalidPath();
            var del = new TestDelegate(() => WhenValidateMethodInvoked());
            Assert.Throws<ArgumentException>(del, _arg1);
        }

        [Test]
        public void MethodValidateSecondArgShouldThrowExceptionIfUriNotWellFormed()
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
            ThenHelperReturnsXmlNodeList();
        }

        [Test]
        public void ShouldReturnAModelOfNodeValues()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked().AndParseXmlMethodInvoked()
                .AndUnfurlNodesMethodInvoked();
            ThenHelperPopulatesAModelWhichContainsNodeValues();
        }

        private void AndUnfurlNodesMethodInvoked()
        {
            _result = _helper.UnfurlNodes(_then);
        }

        private void ThenHelperPopulatesAModelWhichContainsNodeValues()
        {
            var expected = new List<AvinodeMenuItem>
            {
                new AvinodeMenuItem
                {
                    DisplayName = "Home",
                    Path = new Uri("/Default.aspx", UriKind.Relative),
                    SubMenuItem = null
                },
                new AvinodeMenuItem
                {
                    DisplayName = "Trips",
                    Path = new Uri("/Requests/Quotes/CreateQuote.aspx", UriKind.Relative),
                    SubMenuItem = new List<AvinodeMenuItem>
                    {
                        new AvinodeMenuItem
                        {
                            DisplayName = "Create Quote",
                            Path = new Uri("/Requests/Quotes/CreateQuote.aspx", UriKind.Relative),
                            SubMenuItem = null
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "Open Quotes",
                            Path = new Uri("/Requests/OpenQuotes.aspx", UriKind.Relative),
                            SubMenuItem = null
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "Scheduled Trips",
                            Path = new Uri("/Requests/Trips/ScheduledTrips.aspx", UriKind.Relative),
                            SubMenuItem = null
                        }
                    }
                },
                new AvinodeMenuItem
                {
                    DisplayName = "Company",
                    Path = new Uri("/mvc/company/view", UriKind.Relative),
                    SubMenuItem = new List<AvinodeMenuItem>
                    {
                        new AvinodeMenuItem
                        {
                            DisplayName = "Customers",
                            Path = new Uri("/customers/customers.aspx", UriKind.Relative),
                            SubMenuItem = null
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "Pilots",
                            Path = new Uri("/pilots/pilots.aspx", UriKind.Relative),
                            SubMenuItem = null
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "Aircraft",
                            Path = new Uri("/aircraft/Aircraft.aspx", UriKind.Relative),
                            SubMenuItem = null
                        }
                    }
                }
            };

            _result.ShouldBeEquivalentTo(expected);
        }

        private void ThenHelperReturnsXmlNodeList()
        {
            var xmlDoc = new XmlDocument { PreserveWhitespace = false };
            xmlDoc.Load(_arg1);
            var expected = xmlDoc.SelectNodes("menu/item");
            _then.ShouldBeEquivalentTo(expected);
        }

        private AvinodeXmlTests AndParseXmlMethodInvoked()
        {
            _then = _helper.ParseXml();
            return this;
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
