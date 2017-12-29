using System.Runtime.Serialization;

namespace Project.Source.Application.DTO
{
    public class File : ISerializable, IFileSystemNode
    {
        private readonly string _type = "file";
        private readonly string _title;
        private readonly string _path;
        
        public File(string title, string path)
        {
            _title = title;
            _path = path;
        }

        public string GetTitle()
        {
            return _title;
        }
        
        public string GetPath()
        {
            return _path;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("type", _type);
            info.AddValue("title", _title);
            info.AddValue("path", _path);
        }
    }
}
