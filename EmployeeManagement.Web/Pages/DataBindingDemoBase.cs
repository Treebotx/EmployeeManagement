using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages
{
    public class DataBindingDemoBase : ComponentBase
    {
        public string Description { get; set; } = string.Empty;

        protected string Name { get; set; } = "Tom";
        protected string Gender { get; set; } = "Male";

        protected string Color { get; set; } = "background-color:white";
    }
}
