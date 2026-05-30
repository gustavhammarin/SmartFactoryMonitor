using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace FactoryApi.Hubs
{
    public class TelemetryHub : Hub
    {
        public async Task JoinMachineGroup(string machineId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, machineId);
        }

        public async Task LeaveMachineGroup(string machineId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, machineId);
        }
    }
}