using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace InspirationOfTheDay.Tests
{
    public class InspirationOfTheDayTest
    {
        private QuotesService _quotesService;
        private InspirationSender _inspirationSender;
        private RandomGenerator _randomGenerator;
        private MotivationOfTheDay _inspirationOfTheDay;
        private EmployeesRepository _employeesRepository;

        [SetUp]
        public void SetUp()
        {
            _quotesService = Substitute.For<QuotesService>();
            _inspirationSender = Substitute.For<InspirationSender>();
            _randomGenerator = Substitute.For<RandomGenerator>();
            _employeesRepository = Substitute.For<EmployeesRepository>();
            _inspirationOfTheDay = new MotivationOfTheDay(_quotesService, _inspirationSender, _employeesRepository, _randomGenerator);
        }

        [Test]
        public void obtain_1_quote_and_send_to_the_single_employee()
        {
            var wordChosen = "aWord";
            var quote = $"Quote than contain {wordChosen}";
            _quotesService.GetQoutesBy(wordChosen).Returns(new List<string>() { quote });
            var employee = new Employee("Name", "Contact");
            _employeesRepository.GetEmployees().Returns(new List<Employee>() { employee });
            _randomGenerator.GetNumberLowerThan(Arg.Any<int>()).Returns(0);

            _inspirationOfTheDay.InspireSomeone(wordChosen);

            _inspirationSender.Received(1).SendQuote(quote, employee.Contact);
        }

        [Test]
        public void obtain_several_quotes_and_send_a_random_one_to_the_single_employee()
        {
            var wordChosen = "aWord";
            var quote = $"Quote than contain {wordChosen}";
            var otherQuote = $"{wordChosen} in a quote";
            var quotes = new List<string>() { quote, otherQuote};
            _quotesService.GetQoutesBy(wordChosen).Returns(quotes);
            var employee = new Employee("Name", "Contact");
            var employees = new List<Employee>() { employee };
            _employeesRepository.GetEmployees().Returns(employees);
            _randomGenerator.GetNumberLowerThan(quotes.Count).Returns(1);
            _randomGenerator.GetNumberLowerThan(employees.Count).Returns(0);

            _inspirationOfTheDay.InspireSomeone(wordChosen);

            _inspirationSender.Received(1).SendQuote(otherQuote, employee.Contact);
        }

        [Test]
        public void send_quote_to_a_random_employee()
        {
            var wordChosen = "aWord";
            var quote = $"Quote than contain {wordChosen}";
            var quotes = new List<string>() { quote };
            _quotesService.GetQoutesBy(wordChosen).Returns(quotes);
            var employee = new Employee("Fulanito", "111");
            var otherEmployee = new Employee("Menganita", "222");
            var employees = new List<Employee>() { employee, otherEmployee };
            _employeesRepository.GetEmployees().Returns(employees);
            _randomGenerator.GetNumberLowerThan(quotes.Count).Returns(0);
            _randomGenerator.GetNumberLowerThan(employees.Count).Returns(1);

            _inspirationOfTheDay.InspireSomeone(wordChosen);

            _inspirationSender.Received(1).SendQuote(quote, otherEmployee.Contact);
        }
    }
}
