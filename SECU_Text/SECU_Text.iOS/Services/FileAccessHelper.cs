using SECU_Text.iOS.Services;
using SECU_Text.Services;

[assembly: Xamarin.Forms.Dependency(typeof(FileAccessHelper))]

namespace SECU_Text.iOS.Services
{
    public class FileAccessHelper// : IFileAccessHelper
    {
        public string GetLocalFilePath(string filename)
        {
            // Storing the database here is a best practice.
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, filename);
        }

        public void CopyFile(string sourceFilename, string destinationFilename, bool overwrite)
        {
            var sourcePath = GetLocalFilePath(sourceFilename);
            var destinationPath = GetLocalFilePath(destinationFilename);
            System.IO.File.Copy(sourcePath, destinationPath, overwrite);
        }

        public bool DoesFileExist(string filename)
        {
            var fullPath = GetLocalFilePath(filename);
            return System.IO.File.Exists(fullPath);
        }

        public bool DeleteFile(string filename)
        {
            var fullPath = GetLocalFilePath(filename);
            System.IO.File.Delete(fullPath);
            return true;
        }
    }
}