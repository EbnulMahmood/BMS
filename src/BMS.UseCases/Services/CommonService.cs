using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BMS.UseCases.Services
{
    public interface ICommonService
    {
        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        string RemoteIpAddress { get; }
        Guid GetCurrentUserId();
        #endregion

        #region List Loading Function
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }

    public sealed class CommonService : ICommonService
    {
        #region Logger
        #endregion

        #region Properties & Object Initialization
        private readonly IHttpContextAccessor _httpContext;

        public CommonService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        #endregion

        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        public Guid GetCurrentUserId() => Guid.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public string RemoteIpAddress
        {
            get
            {
                var remoteIpAddress = _httpContext.HttpContext.Connection.RemoteIpAddress;

                return remoteIpAddress.ToString();
            }
        }
        #endregion

        #region List Loading Function
        #endregion

        #region Others Function
        #endregion

        #region Helper Function
        #endregion
    }
}
