using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;

namespace Project.Source.AntiCorruption
{
    public class DropboxFileUploader
    {
        private readonly DropboxClient _dropboxClient;
        
        public DropboxFileUploader(DropboxClient dropboxClient)
        {
            _dropboxClient = dropboxClient;
        }
       
        public async Task Upload(string folder, string file, string content)
        {
            using (var mem = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var updated = await _dropboxClient.Files.UploadAsync(
                    folder + "/" + file,
                    WriteMode.Overwrite.Instance,
                    body: mem
                );
            }
        }
    }
}
