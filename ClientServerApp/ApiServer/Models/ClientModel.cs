using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiServer.Models
{
    public class ClientModel
    {
        internal static List<Client> clients = new List<Client>();

        public List<Client> GetClients()
        {
            return clients;
        }

        public Client GetClient(string name, string ipAddress)
        {
            var client = clients.Where(x => x.HostName == name && x.IPAddress == ipAddress).FirstOrDefault();
            return client ?? GetTestClient();
        }

        public Guid Connect(string name, string ipAddress)
        {
            var client = GetClient(name, ipAddress);
            if (client == null)
            {
                client = new Client() { Id = Guid.NewGuid(), HostName = name, IPAddress = ipAddress };
                clients.Add(client);
            }
            return client.Id;
        }

        public List<Upload> GetRecentUploads(string name, string ipAddress)
        {
            var client = GetClient(name, ipAddress);
            return client.RecentUploads;
        }

        public bool Disconnect(string name, string ipAddress)
        {
            var client = GetClient(name, ipAddress);
            if (client != null)
            {
                clients.Remove(client);
            }

            return true;
        }        

        public void GetUserData(out string callerIp, out string callerName)
        {
            callerName = callerIp = string.Empty;
            try
            {
                //userName = HttpContext.Current.User.Identity.Name;
                callerIp = HttpContext.Current.Request.UserHostAddress;
                callerName = HttpContext.Current.Request.UserHostName;
            }
            catch { }
        }

        private Client GetTestClient()
        {
            var client = clients.Where(x => x.HostName == "TestHost").FirstOrDefault();
            if (client == null)
            {
                client = new Client() { HostName = "TestHost", Id = Guid.NewGuid(), IPAddress = "TestIP" };
                clients.Add(client);
            }
            return client;
        }
    }
}