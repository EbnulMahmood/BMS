using Microsoft.AspNetCore.Components;

namespace BMS.BlazorWebApp.Areas.Common.Pages.Button
{
    public class ButtonCloseBase : ComponentBase
    {
        [Parameter]
        public EventCallback OnButtonClosed { get; set; }
    }
}
