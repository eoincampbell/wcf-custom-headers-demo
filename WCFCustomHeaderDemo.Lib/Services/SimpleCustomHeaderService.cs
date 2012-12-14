using System;
using System.Diagnostics;
using System.ServiceModel;
using WCFCustomHeaderDemo.Lib.Contracts;

namespace WCFCustomHeaderDemo.Lib.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SimpleCustomHeaderService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select SimpleCustomHeaderService.svc or SimpleCustomHeaderService.svc.cs at the Solution Explorer and start debugging.
    public class SimpleCustomHeaderService : ISimpleCustomHeaderService
    {
        public string DoWork()
        {
            //Do Work
            //...

            //Capture Headers
            var userName = GetHeader<string>("web-user", "ns");
            var webNodeId = GetHeader<int>("web-node-id", "ns");
            var webSessionId = GetHeader<Guid>("web-session-id", "ns");

            Debug.WriteLine("User: {0} / Node: {1} / Session: {2}", userName, webNodeId, webSessionId);
            var s = string.Format("HeaderInfo: {0}, {1}, {2}",
                userName,
                webNodeId,
                webSessionId);

            return s;
        }

        private static T GetHeader<T>(string name, string ns)
        {
            return OperationContext.Current.IncomingMessageHeaders.FindHeader(name, ns) > -1 
                ? OperationContext.Current.IncomingMessageHeaders.GetHeader<T>(name, ns) 
                : default(T);
        }
    }
}
