using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dropbox.Api;
using Project.Source.Application.DTO;

namespace Project.Source.AntiCorruption
{
    public class DropboxDataProvider
    {
        private readonly DropboxClient _dropboxClient;
        
        public DropboxDataProvider(DropboxClient dropboxClient)
        {
            _dropboxClient = dropboxClient;
        }
        
        public async Task<string> Name()
        {
            var full = await _dropboxClient.Users.GetCurrentAccountAsync();
            return full.Name.DisplayName;
        }
        
        public async Task<string> Email()
        {
            var full = await _dropboxClient.Users.GetCurrentAccountAsync();
            return full.Email;
        }
        
        public async Task<List<Folder>> ListFolder(string path)
        {
            var list = await _dropboxClient.Files.ListFolderAsync(path);

            List<Folder> folders = new List<Folder>();
            
            foreach (var item in list.Entries.Where(i => i.IsFolder)) {
                folders.Add(new Folder(item.Name, path));
            }

            return folders;
        }
        
        public async Task<List<File>> ListFiles(string path)
        {
            var list = await _dropboxClient.Files.ListFolderAsync(path);

            List<File> files = new List<File>();
            
            foreach (var item in list.Entries.Where(i => i.IsFile)) {
                files.Add(new File(item.Name, path));
            }

            return files;
        }
    }
}
