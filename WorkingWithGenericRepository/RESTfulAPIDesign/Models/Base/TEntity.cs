using System.Runtime.Serialization;

namespace RESTfulAPIDesign.Models.Base
{
    /* Contract between attributes and table structure */
    [DataContract]
    public class TEntity
    {
        public long Id { get; set; }
    }
}
