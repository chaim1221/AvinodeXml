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
        private List<AvinodeMenuItem> _expectation;

        [Test]
        public void MethodValidateShouldAcceptTwoValidArguments()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked();
            ThenFieldsContainingArgumentsArePopulatedCorrectly();
        }

        private void WithTwoValidArguments()
        {
            _arg1 = ".\\wyvernmenu.xml";
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
        public void MethodParseXmlShouldBeAbleToParseXmlNodesFromAFile()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked().AndParseXmlMethodInvoked();
            ThenHelperReturnsXmlNodeList();
        }

        [Test]
        public void MethodUnfurlNodesShouldReturnAModelOfNodeValues()
        {
            GivenANewHelper().WithTwoValidArguments();
            WhenValidateMethodInvoked().AndParseXmlMethodInvoked()
                .AndUnfurlNodesMethodInvoked();
            ThenHelperPopulatesAModelWhichContainsNodeValues();
        }

        [Test]
        public void MethodUnfurlNodesAlsoMarksMatchingRecordsAsActive()
        {
            // Nothing, just added "active" to the test expectations
            throw new Exception();
        }

        [Test]
        public void AllThatStuffWorksWithTheOtherFileToo()
        {
            GivenANewHelper().WithNewValidArguments();
            WhenValidateMethodInvoked().AndParseXmlMethodInvoked()
                .AndUnfurlNodesMethodInvoked();
            ThenHelperPopulatesAModelWhichContainsNodeValues();
        }

        private void WithNewValidArguments()
        {
            _arg1 = ".\\schedaeromenu.xml";
            _arg2 = "/Requests/OpenQuotes.aspx";
        }

        private void AndUnfurlNodesMethodInvoked()
        {
            _result = _helper.UnfurlNodes(_then);
        }

        private void ThenHelperPopulatesAModelWhichContainsNodeValues()
        {
            _expectation = AvinodeMenuItems();
            _result.ShouldBeEquivalentTo(_expectation);
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
        private List<AvinodeMenuItem> AvinodeMenuItems()
        {
            switch (_arg1)
            {
                case ".\\schedaeromenu.xml":            
                    return new List<AvinodeMenuItem>
                    {
                        new AvinodeMenuItem
                        {
                            DisplayName = "Home",
                            Path = new Uri("/Default.aspx", UriKind.Relative),
                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/Default.aspx", UriKind.Relative),
                            SubMenuItem = null
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "Trips",
                            Path = new Uri("/Requests/Quotes/CreateQuote.aspx", UriKind.Relative),
                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/Requests/Quotes/CreateQuote.aspx", UriKind.Relative),
                            SubMenuItem = new List<AvinodeMenuItem>
                            {
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Create Quote",
                                    Path = new Uri("/Requests/Quotes/CreateQuote.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/Requests/Quotes/CreateQuote.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Open Quotes",
                                    Path = new Uri("/Requests/OpenQuotes.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/Requests/OpenQuotes.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Scheduled Trips",
                                    Path = new Uri("/Requests/Trips/ScheduledTrips.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/Requests/Trips/ScheduledTrips.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                }
                            }
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "Company",
                            Path = new Uri("/mvc/company/view", UriKind.Relative),
                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/mvc/company/view", UriKind.Relative),
                            SubMenuItem = new List<AvinodeMenuItem>
                            {
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Customers",
                                    Path = new Uri("/customers/customers.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/customers/customers.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Pilots",
                                    Path = new Uri("/pilots/pilots.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/pilots/pilots.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Aircraft",
                                    Path = new Uri("/aircraft/Aircraft.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/aircraft/Aircraft.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                }
                            }
                        }
                    };
                case ".\\wyvernmenu.xml":
                    return new List<AvinodeMenuItem>
                    {
                        new AvinodeMenuItem
                        {
                            DisplayName = "Home",
                            Path = new Uri("/mvc/wyvern/home", UriKind.Relative),
                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/mvc/wyvern/home", UriKind.Relative),
                            SubMenuItem = new List<AvinodeMenuItem>
                            {
                                new AvinodeMenuItem
                                {
                                    DisplayName = "News",
                                    Path = new Uri("/mvc/wyvern/home/news", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/mvc/wyvern/home/news", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Directory",
                                    Path = new Uri("/Directory/Directory.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/Directory/Directory.aspx", UriKind.Relative),
                                    SubMenuItem = new List<AvinodeMenuItem>
                                    {
                                        new AvinodeMenuItem
                                        {
                                            DisplayName = "Favorites",
                                            Path = new Uri("/TWR/Directory.aspx", UriKind.Relative),
                                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/TWR/Directory.aspx", UriKind.Relative),
                                            SubMenuItem = null
                                        },
                                        new AvinodeMenuItem
                                        {
                                            DisplayName = "Search Aircraft",
                                            Path = new Uri("/TWR/AircraftSearch.aspx", UriKind.Relative),
                                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/TWR/AircraftSearch.aspx", UriKind.Relative),
                                            SubMenuItem = null
                                        }
                                    }
                                }
                            }
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "PASS",
                            Path = new Uri("/PASS/GeneratePASS.aspx", UriKind.Relative),
                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/PASS/GeneratePASS.aspx", UriKind.Relative),
                            SubMenuItem = new List<AvinodeMenuItem>
                            {
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Create New",
                                    Path = new Uri("/PASS/GeneratePASS.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/PASS/GeneratePASS.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Sent Requests",
                                    Path = new Uri("/PASS/YourPASSReports.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/PASS/YourPASSReports.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Received Requests",
                                    Path = new Uri("/PASS/Pending/PendingRequests.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/PASS/Pending/PendingRequests.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                }
                            }
                        },
                        new AvinodeMenuItem
                        {
                            DisplayName = "Company",
                            Path = new Uri("/mvc/company/view", UriKind.Relative),
                            Active = new Uri(_arg2, UriKind.Relative) == new Uri("/mvc/company/view", UriKind.Relative),
                            SubMenuItem = new List<AvinodeMenuItem>
                            {
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Users",
                                    Path = new Uri("/mvc/account/list", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/mvc/account/list", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Aircraft",
                                    Path = new Uri("/aircraft/fleet.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/aircraft/fleet.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Insurance",
                                    Path = new Uri("/insurance/policies.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/insurance/policies.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                                new AvinodeMenuItem
                                {
                                    DisplayName = "Certificate",
                                    Path = new Uri("/Certificates/Certificates.aspx", UriKind.Relative),
                                    Active = new Uri(_arg2, UriKind.Relative) == new Uri("/Certificates/Certificates.aspx", UriKind.Relative),
                                    SubMenuItem = null
                                },
                            }
                        }
                    };
            }
            return null;
        }
    }
}
