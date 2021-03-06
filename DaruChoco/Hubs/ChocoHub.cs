﻿using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace DaruChoco.Hubs
{
    [HubName("choco")]
    public class ChocoHub : Hub
    {
        private static long _giriCount;
        private static long _honmeiCount;
        private static long _homoCount;

        private static readonly ConcurrentDictionary<string, bool> _connections = new ConcurrentDictionary<string, bool>(); 

        public static void Serialize(string path)
        {
            var data = string.Format("{0},{1},{2}", _giriCount, _honmeiCount, _homoCount);

            File.WriteAllText(path, data);
        }

        public static void Deserialize(string path)
        {
            if (!File.Exists(path))
            {
                return;
            }

            var data = File.ReadAllText(path);

            var values = data.Split(',').Select(int.Parse).ToArray();

            _giriCount = values[0];
            _honmeiCount = values[1];
            _homoCount = values[2];
        }

        public void Present(int type)
        {
            switch (type)
            {
                case 1:
                    _giriCount += 1;
                    break;

                case 2:
                    _honmeiCount += 1;
                    break;

                default:
                    _homoCount += 1;
                    break;
            }

            Clients.All.NotifyCount(_giriCount, _honmeiCount, _homoCount);
        }

        public override Task OnConnected()
        {
            _connections.TryAdd(Context.ConnectionId, true);

            Clients.All.NotifyCount(_giriCount, _honmeiCount, _homoCount);

            return Clients.All.NotifyUserCount(_connections.Count);
        }

        public override Task OnDisconnected()
        {
            bool value;

            _connections.TryRemove(Context.ConnectionId, out value);

            return Clients.All.NotifyUserCount(_connections.Count);
        }

        public override Task OnReconnected()
        {
            _connections.TryAdd(Context.ConnectionId, true);

            Clients.All.NotifyCount(_giriCount, _honmeiCount, _homoCount);

            return Clients.All.NotifyUserCount(_connections.Count);
        }
    }
}