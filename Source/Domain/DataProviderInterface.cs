using System.Collections.Generic;
using Project.Source.Application.DTO;

namespace Project.Source.Domain
{
    public interface DataProviderInterface
    {
        string DropBoxInfo();
        List<Folder> ListFolder(string path);
        List<File> ListFiles(string path);
    }
}