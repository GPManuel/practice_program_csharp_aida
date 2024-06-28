using System.Collections.Generic;

namespace InspirationOfTheDay;

public interface QuotesService
{
    List<string> GetQoutesBy(string word);
}