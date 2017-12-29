using System.Runtime.Serialization;

namespace Project.Source.Application.DTO
{
    public interface IFileSystemNode
    {
        string GetTitle();
        string GetPath();
    }
}
