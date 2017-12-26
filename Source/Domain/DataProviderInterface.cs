using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dropbox.Api;

namespace Project.Source.Domain
{
    public interface DataProviderInterface
    {
        string DropBoxInfo();
        List<string> ListRootFolder();
    }
}