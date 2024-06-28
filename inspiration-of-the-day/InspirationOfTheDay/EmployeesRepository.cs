using System.Collections.Generic;

namespace InspirationOfTheDay;

public interface EmployeesRepository
{
    List<Employee> GetEmployees();
}