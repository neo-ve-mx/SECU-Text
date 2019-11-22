using SECU_Text.Droid.Services;
using SECU_Text.Services;

[assembly: Xamarin.Forms.Dependency(typeof(FileAccessHelper))]

namespace SECU_Text.Droid.Services
{
    public class FileAccessHelper : IFileAccessHelper
    {
        public string GetLocalFilePath(string filename, bool secure)
        {
            string path = string.Empty;
            if (secure)
            {
                path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            }
            else
            {
                path = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).AbsolutePath;
            }
            // Storing the database here is a best practice.
            
            return System.IO.Path.Combine(path, filename);
        }

        public void CopyFile(string sourceFilename, string destinationFilename, bool overwrite, int typeCopy)
        {
            switch (typeCopy)
            {
                case 1:
                    var sourcePatht1 = GetLocalFilePath(sourceFilename, true);
                    var destinationPatht1 = GetLocalFilePath(destinationFilename, false);
                    System.IO.File.Copy(sourcePatht1, destinationPatht1, overwrite);
                    break;
                case 2:
                    var sourcePatht2 = GetLocalFilePath(sourceFilename, false);
                    var destinationPatht2 = GetLocalFilePath(destinationFilename, true);
                    System.IO.File.Copy(sourcePatht2, destinationPatht2, overwrite);
                    break;
            }
        }

        public bool DoesFileExist(string filename, bool secure)
        {
            string fullPath = string.Empty;
            if (secure)
            {
                fullPath = GetLocalFilePath(filename, true);
            }
            else
            {
                fullPath = GetLocalFilePath(filename, false);
            }
            return System.IO.File.Exists(fullPath);
        }

        public bool DeleteFile(string filename, bool secure)
        {
            if (secure)
            {
                var fullPath = GetLocalFilePath(filename, true);
                System.IO.File.Delete(fullPath);
            }
            else
            {
                var fullPath = GetLocalFilePath(filename, false);
                System.IO.File.Delete(fullPath);
            }
            return true;
        }

        public bool ExternalStorageCanWrite()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).CanWrite();
        }

        public bool ExternalStorageCanRead()
        {
            return Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryDownloads).CanRead();
        }
    }
}