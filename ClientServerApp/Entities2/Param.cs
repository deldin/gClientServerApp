using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Entities
{
    [DataContract]
    public class Param
    {
        [DisplayName("Name")]
        [DataMember]
        public string Name { get; set; }

        [DisplayName("Description")]
        [DataMember]
        public string Description { get; set; }
    }
}
