using Project.Source.Domain;
using Project.Source.AntiCorruption;

namespace Project.Source.Infrastructure
{
    public class FileUploader : FileUploaderInterface
    {
        private readonly DropboxFileUploader _dropboxFileUploader;
        
        public FileUploader(DropboxFileUploader dropboxFileUploader)
        {
            _dropboxFileUploader = dropboxFileUploader;
        }
        
        public async void Upload(string folder, string file, string content)
        {
            await _dropboxFileUploader.Upload(folder, file, content);
        }
    }
}
