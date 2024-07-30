namespace Web.SpeechToTextApi.Areas.v1.Contracts.Requests.Shared;

public sealed class SpeechAdaptation
{
    public IEnumerable<PhraseSet> PhraseSets { get; set; }
    
    public IEnumerable<string> PhraseSetReferences { get; set; }
    
    public IEnumerable<CustomClass> CustomClasses { get; set; }
 
    public ABNFGrammar AbnfGrammar { get; set; }
}