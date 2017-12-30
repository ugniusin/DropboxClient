namespace Project.Source.Domain
{
    public interface IFileManager
    {
        void Upload(string path, string fileName, byte[] content);
        byte[] Download(string path, string fileName);
    }
}
