namespace Adaptive.Intelligence.Shared.Test.Data_Structures
{
    public class USStatesFactoryTests
    {
        [Fact]
        public void ConstructorTests()
        {
            Assert.Equal(53, USStatesFactory.States.Count);
        }

        [Fact]
        public void GetStateNameTest()
        {
            List<string> testnames = GenerateStateNameList();
            foreach (string name in testnames)
            {
                USStates state = USStatesFactory.GetState(name);
                string? actualName = USStatesFactory.GetStateName(state);
                Assert.Equal(name, actualName);
            }
        }

        [Fact]
        public void GetStateAbbrevTest()
        {
            List<string> testList = GenerateAbbreviationList();
            foreach (string abbrev in testList)
            {
                USStates state = USStatesFactory.GetState(abbrev);
                string? actualName = USStatesFactory.GetStateAbbreviation(state);
                Assert.Equal(abbrev, actualName);
            }
        }
        [Fact]
        public void GetStateByNameTest()
        {
            List<string> nameList = GenerateStateNameList();
            List<string> abbrevList = GenerateAbbreviationList();

            for (int count = 0; count < 53; count++)
            {
                string name = nameList[count];
                string abbrev = abbrevList[count];

                USStates enumA = USStatesFactory.GetState(name);
                USStates enumB = USStatesFactory.GetState(abbrev);
                Assert.Equal(enumA, enumB);

                Assert.Equal(USStatesFactory.GetStateName(enumA), name);
                Assert.Equal(USStatesFactory.GetStateAbbreviation(enumB), abbrev);

                USState stRefA = USStatesFactory.GetStateInstance(name);
                USState stRefB = USStatesFactory.GetStateInstance(abbrev);
                Assert.Equal(stRefB, stRefA);

            }
        }
        private static List<string> GenerateStateNameList()
        {
            return new List<string>()
            {
                "Alabama",
                "Alaska",
                "the Territory of American Samoa",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "the District of Columbia",
                "Florida",
                "Georgia",
                "Guam",
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Rhode Island",
                "South Carolina",
                "Tennessee",
                "Texas",
                "the Territory of the US Virgin Islands",
                "Utah",
                "Vermont",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming"
            };
        }

        private static List<string> GenerateAbbreviationList()
        {
            return new List<string>()
            {
                "AL",
                "AK",
                "AS",
                "AZ",
                "AR",
                "CA",
                "CO",
                "CT",
                "DE",
                "DC",
                "FL",
                "GA",
                "GU",
                "HI",
                "ID",
                "IL",
                "IN",
                "IA",
                "KS",
                "KY",
                "LA",
                "ME",
                "MD",
                "MA",
                "MI",
                "MN",
                "MS",
                "MO",
                "MT",
                "NE",
                "NV",
                "NH",
                "NJ",
                "NM",
                "NY",
                "NC",
                "ND",
                "OH",
                "OK",
                "OR",
                "PA",
                "RI",
                "SC",
                "TN",
                "TX",
                "VI",
                "UT",
                "VT",
                "VA",
                "WA",
                "WV",
                "WI",
                "WY"
            };
        }

    }
}
