using Microsoft.AspNetCore.Components;

namespace BMS.BlazorWebApp.Areas.Helper.Pages.Button
{
    public class ButtonCloseBase : ComponentBase
    {
        [Parameter]
        public EventCallback OnButtonClosed { get; set; }
    }
}
