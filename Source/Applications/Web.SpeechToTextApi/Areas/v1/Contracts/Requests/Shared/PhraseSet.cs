namespace Web.SpeechToTextApi.Areas.v1.Contracts.Requests.Shared;

public class PhraseSet
{
    public string Name { get; set; } = null!;
    
    public IEnumerable<Phrase> Phrases { get; set; }
    
    [Obsolete("Google specified")]
    public int Boost { get; set; }
    
    public class Phrase
    {
        public string Value { get; set; }
        
        [Obsolete("Google specified")]
        public int Boost { get; set; }
    }
}