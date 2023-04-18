using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Sockets;
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

    internal sealed class CommonService : ICommonService
    {
        #region Logger
        private readonly ILogger<CommonService> _logger;
        #endregion

        #region Properties & Object Initialization
        private readonly IHttpContextAccessor _httpContext;

        public CommonService(IHttpContextAccessor httpContext, ILogger<CommonService> logger)
        {
            _logger = logger;
            _httpContext = httpContext;
        }
        #endregion

        #region Operational Function
        #endregion

        #region Single Instances Loading Function
        public Guid GetCurrentUserId()
        {
            try
            {
                bool success = Guid.TryParse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
                if (success)
                {
                    return userId;
                }
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong in {Method}", nameof(GetCurrentUserId));
                throw;
            }
        }

        public string RemoteIpAddress
        {
            get
            {
                try
                {
                    var remoteIpAddress = _httpContext.HttpContext.Connection.RemoteIpAddress;

                    if (string.Equals(remoteIpAddress.ToString().Trim(), "::1", StringComparison.CurrentCultureIgnoreCase))
                    {
                        return "localhost";
                    }

                    return remoteIpAddress.ToString();
                }
                catch (ArgumentException)
                {
                    throw;
                }
                catch (SocketException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Something went wrong in {Property}", nameof(RemoteIpAddress));
                    throw;
                }
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
