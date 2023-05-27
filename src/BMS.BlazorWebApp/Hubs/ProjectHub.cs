using BMS.UseCases.Services;
using Microsoft.AspNetCore.SignalR;

namespace BMS.BlazorWebApp.Hubs
{
    public class ProjectHub : Hub
    {
        private readonly IProjectService _service;

        public ProjectHub(IProjectService service)
        {
            _service = service;
        }

        public async Task SendProjectsAsync(CancellationToken token = default)
        {
            var projects = await _service.LoadProjectDtoDropdownAsync();
            await Clients.All.SendAsync("ReceivedProjects", projects, token);
        }
    }
}
