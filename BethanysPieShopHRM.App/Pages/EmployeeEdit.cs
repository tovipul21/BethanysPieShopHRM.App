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

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();

        public List<JobCategory> JobCategories { get; set; } = new List<JobCategory>();

        public string CountryId { get; set; } = string.Empty;
        public string JobCategoryId { get; set; } = string.Empty;

        // Store state of the screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected async override Task OnInitializedAsync()
        {
            Saved = false;

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
            Saved = false;
            Employee.JobCategoryId = int.Parse(CountryId);
            Employee.CountryId = int.Parse(CountryId);

            if (Employee.EmployeeId == 0)
            {
                var addedEmployee = EmployeeService.AddEmployee(Employee);

                if (addedEmployee != null)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully!!";
                    Saved = true;
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding new employeed. Plz try later";
                    Saved = false;
                }
            }
            else
                await EmployeeService.UpdateEmployee(Employee);
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "Something went wrong adding new employeed. Plz try later";
        }

        protected async Task DeleteEmployee()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);

            StatusClass = "alert-success";
            Message = "Deleted successfully";

            Saved = true;
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/employeeoverview");
        }
    }
}
