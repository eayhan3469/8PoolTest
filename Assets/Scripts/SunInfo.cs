public class SunInfo
{
    public SunInfoResults results { get; set; }
    public string status { get; set; }
}

public class SunInfoResults
{
    public string sunrise { get; set; }
    public string sunset { get; set; }
}
