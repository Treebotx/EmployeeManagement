using EmployeeManagement.Models;
using EmployeeManagement.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
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

        protected string Coordinates { get; set; }

        protected bool ShowFooter { get; set; } = true;
        protected string ButtonText { get; set; } = "Hide Footer";
        protected string CssClass { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            Id = Id ?? "1";
            Employee = await _employeeService.GetEmployee(int.Parse(Id));
        }

        protected void EmployeeImg_Mouse_Move(MouseEventArgs e)
        {
            Coordinates = $"X = {e.ClientX} Y = {e.ClientY}";
        }

        protected void EmployeeImg_Mouse_Out(MouseEventArgs e)
        {
            Coordinates = "";
        }

        protected void Button_Click(MouseEventArgs e)
        {
            if (ShowFooter)
            {
                ShowFooter = false;
                ButtonText = "Show Footer";
                CssClass = "HideFooter";
            }
            else
            {
                ShowFooter = true;
                ButtonText = "Hide Footer";
                CssClass = null;
            }
        }
    }
}
