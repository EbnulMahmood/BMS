using BMS.CoreBusiness.ViewModels;
using BMS.UseCases.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BMS.BlazorWebApp.Areas.Developer.Pages.Project
{
    public class DetailsProjectBase : ComponentBase
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        [Parameter]
        public ProjectViewModelDetils ViewModelDetils { get; set; } = new();
        #endregion

        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        #endregion

        #region List Loading Function
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
