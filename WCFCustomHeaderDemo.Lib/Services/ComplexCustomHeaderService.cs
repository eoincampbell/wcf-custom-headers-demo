using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using WCFCustomHeaderDemo.Lib.Contracts;
using WCFCustomHeaderDemo.Lib.Extensions;

namespace WCFCustomHeaderDemo.Lib.Services
{
    [CustomInspectorBehavior]
    public class ComplexCustomHeaderService : IComplexCustomHeaderService
    {
        public CustomHeader HeaderInformation { get
        {
            var customHeader =
                OperationContext.Current.IncomingMessageProperties.FirstOrDefault(f => f.Key == "CustomHeader").Value as
                CustomHeader;

            return customHeader;
        } }

        public string DoWork()
        {
            var s = string.Format("HeaderInfo: {0}, {1}, {2}", 
                HeaderInformation.WebNodeId, 
                HeaderInformation.WebSessionId, 
                HeaderInformation.WebUserId );

            return s;
        }
    }
}
