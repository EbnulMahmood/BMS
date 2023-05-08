using Microsoft.AspNetCore.Components;

namespace BMS.BlazorWebApp.Areas.Helper.Pages.Aleart
{
    public class DisplayAleartBase : ComponentBase
    {
        [Parameter]
        public string Message { get; set; } = string.Empty;
        [Parameter]
        public string MessageType { get; set; } = string.Empty;
        [Parameter]
        public EventCallback OnAleartDismissed { get; set; }
    }
}
