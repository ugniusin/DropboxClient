using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Project.Source.Domain;
using Project.Source.AntiCorruption;

namespace Project.Source.Infrastructure
{
    public class DataProvider : DataProviderInterface
    {
        private readonly DropboxDataProvider _dropboxDataProvider;
        
        public DataProvider(DropboxDataProvider dropboxDataProvider)
        {
            _dropboxDataProvider = dropboxDataProvider;
        }

        public string DropBoxInfo()
        {
            try {
                return _dropboxDataProvider.DropBoxInfo().Result;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public List<string> ListRootFolder()
        {   
            return _dropboxDataProvider.ListRootFolder().Result;
        }
    }
}
