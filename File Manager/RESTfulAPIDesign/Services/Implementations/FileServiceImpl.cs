using RESTfulAPIDesign.Data.ValuesObjects;
using System.IO;

namespace RESTfulAPIDesign.Services.Implementations
{
    public class FileServiceImpl : IFileService
    {
        public byte[] GetPDFFile(UserVO user)
        {
            string path = Directory.GetCurrentDirectory();
            var fullpath = path + "\\Others\\sample-pdf-for-download.pdf";
            return File.ReadAllBytes(fullpath);
        }
    }
}
