using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalRTest
{
    internal class HubClient
    {
        HubConnection _con;
        string _server;
        public HubClient(string server)
        {

            _server = server;

        }
        public async Task Connect()
        {
            _con = new HubConnectionBuilder().WithUrl(_server).Build();
            _con.Closed += async (error) =>
            {
                Console.WriteLine($"connection closed with error: {error.Message ?? "None"}");
                await Task.Delay(1000);
                await Connect();
            };

            _con.On("userconnected", (string userid, int usercount) =>
            {
                Console.WriteLine($"{userid} has joined the chat, total users: {usercount}");
            });
            _con.On("ReceiveMessageFromApi", (string sender, string message) =>
            {
                Console.WriteLine($"{sender} has just send you this message: {message}");
            });
            _con.On("userdisconnected", (string userid, int usercount) =>
            {
                Console.WriteLine($"{userid} has left the program, remaining users: {usercount}");
            });

            _con.On("GetPrivateMessageFromApi", (string message) =>
            {
                Console.WriteLine($" message: {message}");
            });

            Console.WriteLine("connection established");
            await _con.StartAsync();
            await _con.InvokeAsync("SendMessage", "hello all connected users");
        }
    }
}
