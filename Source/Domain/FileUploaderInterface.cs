namespace Project.Source.Domain
{
    public interface FileUploaderInterface
    {
        void Upload(string folder, string file, string content);
    }
}