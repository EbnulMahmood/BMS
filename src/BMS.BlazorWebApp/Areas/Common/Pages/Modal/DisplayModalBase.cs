using Microsoft.AspNetCore.Components;
using static BMS.BlazorWebApp.Helpers.WebHelper;

namespace BMS.BlazorWebApp.Areas.Common.Pages.Modal
{
    public class DisplayModalBase : ComponentBase
    {
        [Parameter]
        public string Caption { get; set; } = string.Empty;
        [Parameter]
        public string Message { get; set; } = string.Empty;
        [Parameter]
        public EventCallback<bool> OnCloseAsync { get; set; }
        [Parameter]
        public ModalCategory Type { get; set; }

        protected Task CancelAsync()
        {
            return OnCloseAsync.InvokeAsync(false);
        }
        protected Task OkAsync()
        {
            return OnCloseAsync.InvokeAsync(true);
        }
    }
}
