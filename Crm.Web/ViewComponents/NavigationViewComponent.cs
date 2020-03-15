using Crm.Domains.Enumerations;
using DNI.Core.Contracts;
using DNI.Core.Services;
using DNI.Core.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Crm.Web.ViewComponents
{
    public class NavigationViewComponent : DefaultViewComponentBase
    {
        private readonly ISwitch<NavigationView, string> _navigationSwitch;

        public NavigationViewComponent()
        {
            _navigationSwitch = Switch.Create<NavigationView, string>()
                .CaseWhen(NavigationView.Header, "Header")
                .CaseWhen(NavigationView.Footer, "Footer");
        }

        public Task<IViewComponentResult> InvokeAsync(NavigationView navigationView)
        {
            return Task.FromResult<IViewComponentResult>(View(_navigationSwitch.Case(navigationView)));
        }
    }
}
