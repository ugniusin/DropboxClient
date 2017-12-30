namespace Project.Source.Domain
{
    public interface IFileUploader
    {
        void Upload(string folder, string file, byte[] content);
    }
}
