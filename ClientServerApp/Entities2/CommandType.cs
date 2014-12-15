using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Entities
{
    [DataContract]
    public enum CommandType
    {
        [EnumMember]
        [Description("Connect")]
        Connect = 0,
        [EnumMember]
        [Description("Upload")]
        Upload = 1,
        [EnumMember]
        [Description("Compile")]
        Compile = 2,
        [EnumMember]
        [Description("RunTests")]
        RunTests = 3,
        [EnumMember]
        [Description("Disconnect")]
        Disconnect = 4
    }
}
