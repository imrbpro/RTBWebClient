using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using RTBWebClient.Models;

namespace RTBWebClient.Services
{
    public class SignalRService  : IAsyncDisposable
    {
        HubConnection? _hubConnection;
        public readonly NavigationManager _navigationManager;
        public event Action<TaskItem> OnTaskItemReceived;
        public event Action<int> OnUpdateMemberCount;
        public SignalRService(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public async Task StartConnection()
        {
            if (_hubConnection is not null) return;

            _hubConnection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7197/TeamBoard")
                .Build();
            _hubConnection.On<TaskItem>("ReceiveTeamItem", item => OnTaskItemReceived.Invoke(item));
            _hubConnection.On<int>("UpdateConnectedMembersCount", count => OnUpdateMemberCount.Invoke(count));

            await _hubConnection.StartAsync();
        }

        public async Task SendMessage(TaskItem task)
        {
            await _hubConnection.SendAsync("AddTeamUpdate", task);
        }

        public async ValueTask DisposeAsync()
        {
            if(_hubConnection is not null)
            {
               await _hubConnection.DisposeAsync();
            }
        }
    }
}
