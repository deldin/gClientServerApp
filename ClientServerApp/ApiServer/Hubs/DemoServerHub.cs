﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace ApiServer
{
    public class ServerHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello();
        }
    }
}