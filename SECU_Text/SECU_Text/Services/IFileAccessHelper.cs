namespace SECU_Text.Services
{
    public interface IFileAccessHelper
    {
        string GetLocalFilePath(string fileName, bool secure);
        void CopyFile(string sourceFilename, string destinationFilename, bool overwrite, int typeCopy);
        bool DoesFileExist(string fileName, bool secure);
        bool DeleteFile(string fileName, bool secure);
        bool ExternalStorageCanWrite();
        bool ExternalStorageCanRead();
    }
}
