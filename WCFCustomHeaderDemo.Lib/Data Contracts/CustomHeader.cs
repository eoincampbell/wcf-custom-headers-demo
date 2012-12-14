using System;
using System.Runtime.Serialization;

namespace WCFCustomHeaderDemo
{
    [DataContract]
    public class CustomHeader
    {
        [DataMember]
        public string WebUserId { get; set; }
        [DataMember]
        public int WebNodeId { get; set; }
        [DataMember]
        public Guid WebSessionId { get; set; }
    }
}