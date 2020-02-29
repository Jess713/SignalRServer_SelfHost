using System;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;

using Microsoft.AspNet.SignalR.Hubs;


namespace SignalRServer_SelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
        // This will *ONLY* bind to localhost, if you want to bind to adll addresses
        // use http://*:8080 to bind to all addresses. 
        // See http://msdn.microsoft.com/library/system.net.httplistener.aspx 
        // for more information.
        //https://docs.microsoft.com/en-us/aspnet/signalr/overview/guide-to-the-api/handling-connection-lifetime-events

            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
           


            string url = "http://+:8089";
            WebApp.Start<Startup>(url);

            Console.WriteLine("Server running on {0}", url);
            Console.ReadLine();

        }
    }
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }

    [HubName("MyHub")]
    public class MyHub : Hub
    {

        public void Send(string message)
        {
           
            Console.WriteLine("from client: {0}", message);
           
        }
        public void NotifyAllClients()
        {
            Clients.All.Notify();
        }


    }
}