using System.Linq;

namespace InspirationOfTheDay;

public class MotivationOfTheDay
{
    private readonly QuotesService _quotesService;
    private readonly InspirationSender _inspirationSender;
    private readonly EmployeesRepository _employeesRepository;
    private readonly RandomGenerator _randomGenerator;

    public MotivationOfTheDay(QuotesService quotesService, InspirationSender inspirationSender,
        EmployeesRepository employeesRepository, RandomGenerator randomGenerator)
    {
        _quotesService = quotesService;
        _inspirationSender = inspirationSender;
        _employeesRepository = employeesRepository;
        _randomGenerator = randomGenerator;
    }

    public void InspireSomeone(string word)
    {
        var quotes = _quotesService.GetQoutesBy(word);
        var employees = _employeesRepository.GetEmployees();
        var randomNumberForQuote = _randomGenerator.GetNumberLowerThan(quotes.Count);
        var randomNumberForEmployee = _randomGenerator.GetNumberLowerThan(employees.Count);
        _inspirationSender.SendQuote(quotes[randomNumberForQuote], employees[randomNumberForEmployee].Contact);
    }
}