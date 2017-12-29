using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        
        public async Task<string> DropBoxInfo()
        {
            var full = await _dropboxClient.Users.GetCurrentAccountAsync();
            return full.Name.DisplayName + full.Email;
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
        
        public async Task<List<Application.DTO.File>> ListFiles(string path)
        {
            var list = await _dropboxClient.Files.ListFolderAsync(path);

            List<Application.DTO.File> files = new List<Application.DTO.File>();
            
            foreach (var item in list.Entries.Where(i => i.IsFile)) {
                files.Add(new Application.DTO.File(item.Name, path));
            }

            return files;
        }
    }
}
