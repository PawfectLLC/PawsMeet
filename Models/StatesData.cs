namespace PawfectAppCore.Models
{
    public class StatesData
    {
        public static Dictionary<string, List<string>> NeighboringStates { get; } = new Dictionary<string, List<string>>()
        {
            {"Alabama", new List<string>{"Mississippi", "Tennessee", "Georgia", "Florida"}},
            {"Alaska", new List<string>{}},
            {"Arizona", new List<string>{"California", "Nevada", "Utah", "Colorado", "New Mexico"}},
            {"Arkansas", new List<string>{"Missouri", "Tennessee", "Mississippi", "Louisiana", "Texas", "Oklahoma"}},
            {"California", new List<string>{"Oregon", "Nevada", "Arizona"}},
            {"Colorado", new List<string>{"Wyoming", "Nebraska", "Kansas", "Oklahoma", "New Mexico", "Utah"}},
            {"Connecticut", new List<string>{"New York", "Massachusetts", "Rhode Island"}},
            {"Delaware", new List<string>{"Maryland", "Pennsylvania", "New Jersey"}},
            {"Florida", new List<string>{"Alabama", "Georgia"}},
            {"Georgia", new List<string>{"Florida", "Alabama", "Tennessee", "North Carolina", "South Carolina"}},
            {"Hawaii", new List<string>{}},
            {"Idaho", new List<string>{"Washington", "Oregon", "Montana", "Wyoming", "Utah", "Nevada"}},
            {"Illinois", new List<string>{"Wisconsin", "Indiana", "Kentucky", "Missouri", "Iowa"}},
            {"Indiana", new List<string>{"Michigan", "Ohio", "Kentucky", "Illinois"}},
            {"Iowa", new List<string>{"Minnesota", "Wisconsin", "Illinois", "Missouri", "Nebraska", "South Dakota"}},
            {"Kansas", new List<string>{"Nebraska", "Missouri", "Oklahoma", "Colorado"}},
            {"Kentucky", new List<string>{"Illinois", "Indiana", "Ohio", "West Virginia", "Virginia", "Tennessee", "Missouri"}},
            {"Louisiana", new List<string>{"Arkansas", "Mississippi", "Texas"}},
            {"Maine", new List<string>{"New Hampshire"}},
            {"Maryland", new List<string>{"Pennsylvania", "Delaware", "Virginia", "West Virginia"}},
            {"Massachusetts", new List<string>{"Connecticut", "Rhode Island", "New York", "Vermont", "New Hampshire"}},
            {"Michigan", new List<string>{"Wisconsin", "Indiana", "Ohio"}},
            {"Minnesota", new List<string>{"North Dakota", "South Dakota", "Iowa", "Wisconsin"}},
            {"Mississippi", new List<string>{"Louisiana", "Arkansas", "Tennessee", "Alabama"}},
            {"Missouri", new List<string>{"Iowa", "Illinois", "Kentucky", "Tennessee", "Arkansas", "Oklahoma", "Kansas", "Nebraska"}},
            {"Montana", new List<string>{"Idaho", "Wyoming", "North Dakota", "South Dakota"}},
            {"Nebraska", new List<string>{"South Dakota", "Wyoming", "Colorado", "Kansas", "Missouri", "Iowa"}},
            {"Nevada", new List<string>{"California", "Oregon", "Idaho", "Utah", "Arizona"}},
            {"New Hampshire", new List<string>{"Vermont", "Maine", "Massachusetts"}},
            {"New Jersey", new List<string>{"New York", "Pennsylvania", "Delaware"}},
            {"New Mexico", new List<string>{"Arizona", "Utah", "Colorado", "Oklahoma", "Texas"}},
            {"New York", new List<string>{"Vermont", "Massachusetts", "Connecticut", "New Jersey", "Pennsylvania"}},
            {"North Carolina", new List<string>{"Virginia", "Tennessee", "Georgia", "South Carolina"}},
            {"North Dakota", new List<string>{"Montana", "South Dakota", "Minnesota"}},
            {"Ohio", new List<string>{"Michigan", "Indiana", "Kentucky", "West Virginia", "Pennsylvania"}},
            {"Oklahoma", new List<string>{"Kansas", "Missouri", "Arkansas", "Texas", "New Mexico", "Colorado"}},
            {"Oregon", new List<string>{"Washington", "Idaho", "Nevada", "California"}},
            {"Pennsylvania", new List<string>{"New York", "Ohio", "West Virginia", "Maryland", "New Jersey"}},
            {"Rhode Island", new List<string>{"Connecticut", "Massachusetts"}},
            {"South Carolina", new List<string>{"Georgia", "North Carolina"}},
            {"South Dakota", new List<string>{"North Dakota", "Minnesota", "Iowa", "Nebraska", "Wyoming", "Montana"}},
            {"Tennessee", new List<string>{"Kentucky", "Virginia", "North Carolina", "Georgia", "Alabama", "Mississippi", "Arkansas", "Missouri"}},
        };

    }
}
