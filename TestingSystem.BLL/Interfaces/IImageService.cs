using System.Web;
using System.Threading.Tasks;

namespace TestingSystem.BLL.Interfaces
{
    public interface IImageService
    {
        Task<byte[]> GetImageData(HttpPostedFileBase image);
        Task<byte[]> GetImageDataFromFS(string path);
    }
}
