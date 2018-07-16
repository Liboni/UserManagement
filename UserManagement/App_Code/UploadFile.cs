
namespace UserManagement
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public class UploadFile
    {
        public static async Task<string> SaveFileInWebRoot(IFormFile file, string webRootPath)
        {
            try
            {
                if (file.Length <= 0) return Path.Combine(webRootPath, "user-image.jpg");
                string fileName = DateTime.Now.Ticks + file.FileName;
                string filePath = Path.Combine(webRootPath, fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                    stream.Dispose();
                }
                return fileName;
            }
            catch (Exception) { return String.Empty; }
        }
    }
}
