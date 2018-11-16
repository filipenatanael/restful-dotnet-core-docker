using System.Collections.Generic;

namespace RESTfulAPIDesign.Data.Conveter
{
    public interface IParser<Origin, Destiny>
    {
        /* Origin can be a Value Objects(VO) and Destiny can be a entity */
        Destiny Parse(Origin origin);
        List<Destiny> ParseList(List<Origin> origin);
    }
}
