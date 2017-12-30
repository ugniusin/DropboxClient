using Dropbox.Api.Files;
using Dropbox.Api.Stone;
using Project.Source.Domain;
using Project.Source.AntiCorruption;

namespace Project.Source.Infrastructure
{
    public class FileManager : IFileManager
    {
        private readonly DropboxFileManager _dropboxFileManager;
        
        public FileManager(DropboxFileManager dropboxFileManager)
        {
            _dropboxFileManager = dropboxFileManager;
        }
        
        public async void Upload(string path, string fileName, byte[] content)
        {
            await _dropboxFileManager.Upload(path, fileName, content);
        }
        
        public byte[] Download(string path, string fileName)
        {
            return _dropboxFileManager.Download(path, fileName).Result;
        }
    }
}
