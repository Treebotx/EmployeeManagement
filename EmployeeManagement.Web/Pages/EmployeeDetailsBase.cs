using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EmployeeDetailsBase : ComponentBase
    {
        [Inject]
        public IEmployeeService _employeeService { get; set; }

        public Employee Employee { get; set; } = new Employee();

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await _employeeService.GetEmployee(int.Parse(Id));
        }
    }
}
