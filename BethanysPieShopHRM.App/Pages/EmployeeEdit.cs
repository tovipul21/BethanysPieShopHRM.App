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

        [Parameter]
        public string EmployeeId { get; set; }

        public Employee Employee { get; set; } = new Employee();

        public List<Country> Countries { get; set; } = new List<Country>();

        public string CountryId { get; set; } = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployeeDetails(int.Parse(EmployeeId));
            
            CountryId = Employee.CountryId.ToString();

            Countries = (await CoutryDataService.GetAllCountries()).ToList();
        }
    }
}
