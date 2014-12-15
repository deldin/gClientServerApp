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
    public class Upload
    {
        [DisplayName("Id")]
        [DataMember]
        public Guid Id { get; set; }

        [DisplayName("FileName")]
        [DataMember]
        public string FileName { get; set; }

        [DisplayName("FilePath")]
        [DataMember]
        public string FilePath { get; set; }

        [DisplayName("Created")]
        [DataMember]
        public DateTime Created { get; set; }
    }
}
