using Microsoft.AspNetCore.SignalR;
using HorseClub.Services.KhoiND.Realtime;

namespace HorseClub.RazorWebApp.KhoiND.Hubs
{
    // Anonymous on purpose: external .NET clients (Blazor, Worker) can't reuse a browser cookie.
    public class HorseClubHub : Hub
    {
        // Called by other clients (Blazor, Worker, browser JS) to fan a message out to everyone.
        public Task Broadcast(string message)
            => Clients.All.SendAsync(HubContract.Notification, message);
    }
}
