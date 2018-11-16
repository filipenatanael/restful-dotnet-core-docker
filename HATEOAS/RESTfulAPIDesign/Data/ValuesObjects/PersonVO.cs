using System.Runtime.Serialization;

namespace RESTfulAPIDesign.Data.ValuesObjects
{   
    [DataContract]
    public class PersonVO
    {   
        [DataMember (Order = 1, Name ="code")]
        public long? Id { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 5)]
        public string Address { get; set; }

        [DataMember(Order = 4)]
        public string Gender { get; set; }
    }
}
