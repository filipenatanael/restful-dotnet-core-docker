using RESTfulAPIDesign.Data.ValuesObjects;

namespace RESTfulAPIDesign.Services
{
    interface IFileService
    {
        byte[] GetPDFFile(UserVO user);
    }
}
