using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Erp.Hubs
{
    public class MonitoringHub : Hub
    {
        public async Task updateDeployList(string Id, string name)
        {
            await Clients.All.SendAsync("updateDeployList", Id, name);
            
        }
    }
}