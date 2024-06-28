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
        public void get_1_quote_and_send_to_the_single_employee()
        {
            var wordChosen = "aWord";
            var quote = $"Quote than contain {wordChosen}";
            _quotesService.GetQoutesBy(wordChosen).Returns(new List<string>() { quote });
            var employee = new Employee("Name", "Contact");
            _employeesRepository.GetEmployees().Returns(new List<Employee>() { employee });

            _inspirationOfTheDay.InspireSomeone(wordChosen);

            _inspirationSender.Received(1).SendQuote(quote, employee.Contact);
        }
    }
}
