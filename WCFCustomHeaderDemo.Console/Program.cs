using System;
using System.ServiceModel;
using WCFCustomHeaderDemo.Console.ComplexCustomHeaderService;
using WCFCustomHeaderDemo.Console.SimpleCustomHeaderService;
using WCFCustomHeaderDemo.Lib.Extensions;

namespace WCFCustomHeaderDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            CallSimpleCustomHeaderService();

            CallComplexCustomHeaderService();
        }

        private static void CallComplexCustomHeaderService()
        {
            using(var client = new ComplexCustomHeaderServiceClient())
            {
                ClientCustomHeaderContext.HeaderInformation.WebNodeId = 465;
                ClientCustomHeaderContext.HeaderInformation.WebSessionId = Guid.NewGuid();
                ClientCustomHeaderContext.HeaderInformation.WebUserId = "joe.bloggs";
                System.Console.WriteLine(client.DoWork());
            }
        }

        private static void CallSimpleCustomHeaderService()
        {
            using (var client = new SimpleCustomHeaderServiceClient())
            {
                using (var scope = new OperationContextScope(client.InnerChannel))
                {
                    //Log the user who's web session resulted in this service call
                    var webUser = new MessageHeader<string>("joe.bloggs");
                    var webUserHeader = webUser.GetUntypedHeader("web-user", "ns");

                    //Log which webnode we were on behind the load balancer
                    var webNodeId = new MessageHeader<int>(1234);
                    var webNodeIdHeader = webNodeId.GetUntypedHeader("web-node-id", "ns");

                    //Log a unique session id
                    var webSessionId = new MessageHeader<Guid>(Guid.NewGuid());
                    var webSessionIdHeader = webSessionId.GetUntypedHeader("web-session-id", "ns");

                    OperationContext.Current.OutgoingMessageHeaders.Add(webUserHeader);
                    OperationContext.Current.OutgoingMessageHeaders.Add(webNodeIdHeader);
                    OperationContext.Current.OutgoingMessageHeaders.Add(webSessionIdHeader);

                    System.Console.WriteLine(client.DoWork());
                }
            }
        }
    }
}
