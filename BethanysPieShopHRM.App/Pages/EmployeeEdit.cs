using BethanysPieShopHRM.App.Services;
using BethanysPieShopHRM.Shared;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.Pages
{
    public partial class EmployeeEdit
    {
        [Inject]
        public IEmployeeDataService EmployeeService {  get; set; }

        [Inject]
        public ICountryDataService CoutryDataService { get; set; }

        [Inject]
        public IJobCategoryDataService JobCategoryService { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        public string CountryId { get; set; } = string.Empty;
        public string JobCategoryId { get; set; } = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            int.TryParse(EmployeeId, out var employeeId);

            if (employeeId == 0) // New employee being created
                Employee = new Employee { CountryId = 1, JobCategoryId = 1, BirthDate = System.DateTime.Now, JoinedDate = System.DateTime.Now };
            else // Existing employee being updated
                Employee = await EmployeeService.GetEmployeeDetails(int.Parse(EmployeeId));

            CountryId = Employee.CountryId.ToString();
            JobCategoryId = Employee.JobCategoryId.ToString();

            Countries = (await CoutryDataService.GetAllCountries()).ToList();
            JobCategories = (await JobCategoryService.GetAllJobCategories()).ToList();

        }

        protected async Task HandleValidSubmit()
        {
            Employee.JobCategoryId = int.Parse(CountryId);
            Employee.CountryId = int.Parse(CountryId);

            if (Employee.EmployeeId == 0)
            {
                var addedEmployee = EmployeeService.AddEmployee(Employee);
            }
            else
            {
                await EmployeeService.UpdateEmployee(Employee);
            }


        }
    }
}
