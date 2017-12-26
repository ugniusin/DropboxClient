using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dropbox.Api;
using Dropbox.Api.Files;

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
            return "{0} - {1}" + full.Name.DisplayName + full.Email;
        }
        
        public async Task<List<string>> ListRootFolder()
        {
            var list = await _dropboxClient.Files.ListFolderAsync("");

            // show folders then files
            List<string> folders = new List<string>();
            
            foreach (var item in list.Entries.Where(i => i.IsFolder)) {
                folders.Add(item.Name);
            }

            return folders;
        }
    }
}
