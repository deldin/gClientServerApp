using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [DataContract]
    public class ExecutionResult
    {
        [DataMember]
        [DisplayName("Value")]
        public bool Value { get; set; }

        [DataMember]
        [DisplayName("ErrorsOrMessages")]        
        public string ErrorsOrMessages { get; set; }
        
    }
}
