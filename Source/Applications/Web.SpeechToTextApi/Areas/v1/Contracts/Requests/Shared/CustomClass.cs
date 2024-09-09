namespace Web.SpeechToTextApi.Areas.v1.Contracts.Requests.Shared;

public class CustomClass
{
    public string Name { get; set; }
    
    public string CustomClassId { get; set; }
    
    public IEnumerable<ClassItem> Items { get; set; }
    
    public class ClassItem
    {
        public string Value { get; set; }
    }
}