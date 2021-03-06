using AutoMapper;
using EmployeeManagement.Models;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService _employeeService { get; set; }

        [Inject]
        public IDepartmentService _departmentService { get; set; }

        [Inject]
        public IMapper _mapper { get; set; }

        [Inject]
        public NavigationManager _navigationManager { get; set; }

        private Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();
        public List<Department> Departments { get; set; } = new List<Department>();

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Employee = await _employeeService.GetEmployee(int.Parse(Id));
            Departments = (await _departmentService.GetDepartments()).ToList();

            _mapper.Map(Employee, EditEmployeeModel);
        }

        protected async Task HandleValidSubmit()
        {
            _mapper.Map(EditEmployeeModel, Employee);

            var result = await _employeeService.UpdateEmployee(Employee);

            if (result is not null)
            {
                _navigationManager.NavigateTo("/");
            }
        }
    }
}
