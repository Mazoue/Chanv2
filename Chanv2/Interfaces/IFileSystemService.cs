namespace Chanv2.Interfaces
{
    public interface IFileSystemService
    {
        string CreateFileDestination(string boardName, string threadName);
        string GenerateFilePath(string baseFolder, string fileName, string fileExtension);

    }
}
