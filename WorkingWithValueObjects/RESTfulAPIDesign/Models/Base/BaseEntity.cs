using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace RESTfulAPIDesign.Models.Base
{
    /* Contract between attributes and table structure */
    // [DataContract]
    public class BaseEntity
    {
        [Column("id")]
        public long? Id { get; set; }
    }
}
