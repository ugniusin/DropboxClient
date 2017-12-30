using System.IO;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace Project.Source.AntiCorruption
{
    public class DropboxFileManager
    {
        private readonly DropboxClient _dropboxClient;
        
        public DropboxFileManager(DropboxClient dropboxClient)
        {
            _dropboxClient = dropboxClient;
        }
        
        public async Task Upload(string path, string fileName, byte[] content)
        {
            using (var mem = new MemoryStream(content))
            {
                await _dropboxClient.Files.UploadAsync(
                    path + "/" + fileName,
                    WriteMode.Overwrite.Instance,
                    body: mem
                );
            }
        }
        
        public async Task<byte[]> Download(string path, string fileName)
        {
            using (var response = await _dropboxClient.Files.DownloadAsync(path + "/" + fileName))
            {   
                return await response.GetContentAsByteArrayAsync();
            }
        }
    }
}
