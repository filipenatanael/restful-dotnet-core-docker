using RESTfulAPIDesign.Data.ValuesObjects;

namespace RESTfulAPIDesign.Services
{
    public interface IFileService
    {
        byte[] GetPDFFile();
    }
}
