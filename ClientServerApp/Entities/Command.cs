using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Entities
{
    [DataContract]
    public class Command
    {
        [DisplayName("Id")]
        [DataMember]
        public int Id { get; set; }

        [DisplayName("Cmd")]
        [DataMember]
        public string Cmd { get; set; }

        [DisplayName("Description")]
        [DataMember]
        public string  Description { get; set; }

        [DisplayName("Params")]
        [DataMember]
        public string Instructions { get; set; }

        //[DisplayName("Type")]
        //[DataMember]
        //public CommandType Type { get; set; }

        //[DisplayName("Params")]
        //[DataMember]
        //public List<Param> Params{ get; set; }

        //[DisplayName("Instructions")]
        //[DataMember]
        
    }
}
