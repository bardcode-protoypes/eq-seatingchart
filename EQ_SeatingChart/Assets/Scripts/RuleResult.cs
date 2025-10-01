using System.Collections.Generic;

public class RuleResult
{
    public bool IsSuccessful => Errors.Count == 0;
    public List<string> Errors = new List<string>();
}