using System;
using System.Collections.Generic;
using Project.Source.Domain;
using Project.Source.AntiCorruption;
using Project.Source.Application.DTO;

namespace Project.Source.Infrastructure
{
    public class DataProvider : IDataProvider
    {
        private readonly DropboxDataProvider _dropboxDataProvider;
        
        public DataProvider(DropboxDataProvider dropboxDataProvider)
        {
            _dropboxDataProvider = dropboxDataProvider;
        }

        public string Name()
        {
            return _dropboxDataProvider.Name().Result;
        }

        public string Email()
        {
            return _dropboxDataProvider.Email().Result;
        }

        public List<Folder> ListFolder(string path)
        {   
            return _dropboxDataProvider.ListFolder(path).Result;
        }

        public List<File> ListFiles(string path)
        {   
            return _dropboxDataProvider.ListFiles(path).Result;
        }
    }
}
