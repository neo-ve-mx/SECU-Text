using GalaSoft.MvvmLight.Command;
using SECU_Text.Helpers;
using SECU_Text.Interfaces;
using SECU_Text.Services;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace SECU_Text.ViewModels
{
    public class OptionsViewModel : BaseViewModel
    {
        #region Atributes
        private bool isenable;
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get { return isenable; }
            set { SetValue(ref isenable, value); }
        }
        #endregion

        #region Constructors
        public OptionsViewModel()
        {
            IsEnabled = true;
        }
        #endregion

        #region Commands
        public ICommand RestoreCommand
        {
            get
            {
                return new RelayCommand(Restore);
            }
        }

        private async void Restore()
        {
            try
            {
                bool result = await Application.Current.MainPage.DisplayAlert(
                Languages.AppLiteral1,
                Languages.OptionsLiteral13,
                Languages.ViewItemLiteral3,
                Languages.ViewItemLiteral4);
                if (result)
                {
                    bool fileExists;
                    var fileAccessHelper = DependencyService.Get<IFileAccessHelper>();

                    if (!fileAccessHelper.ExternalStorageCanRead() || !fileAccessHelper.ExternalStorageCanWrite())
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.ExceptionLiteral1,
                        Languages.OptionsLiteral6,
                        Languages.ExceptionLiteral3);
                        return;
                    }

                    IsEnabled = false;

                    var dbBackupFileName = "SECUTextDB-Backup.db3";

                    fileExists = fileAccessHelper.DoesFileExist(dbBackupFileName, false);
                    if (!fileExists)
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.AppLiteral1,
                        Languages.OptionsLiteral9,
                        Languages.ExceptionLiteral3);
                        IsEnabled = true;
                        return;
                    }

                    fileAccessHelper.DeleteFile("SECUTextDB.db3", true);
                    fileAccessHelper.CopyFile(dbBackupFileName, "SECUTextDB.db3", false, 2);
                    fileExists = fileAccessHelper.DoesFileExist("SECUTextDB.db3", true);

                    IsEnabled = true;

                    if (fileExists)
                    {
                        DependencyService.Get<Toast>().Show(Languages.OptionsLiteral10);
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show(Languages.OptionsLiteral11);
                    }
                }
            }
            catch (Exception ex)
            {
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1,
                    Languages.ExceptionLiteral2,
                    Languages.ExceptionLiteral3);
            }
        }

        public ICommand BackupCommand
        {
            get
            {
                return new RelayCommand(Backup);
            }
        }

        private async void Backup()
        {
            try
            {
                bool result = await Application.Current.MainPage.DisplayAlert(
                Languages.AppLiteral1,
                Languages.OptionsLiteral12,
                Languages.ViewItemLiteral3,
                Languages.ViewItemLiteral4);
                if (result)
                {
                    bool fileExists;
                    var fileAccessHelper = DependencyService.Get<IFileAccessHelper>();

                    if (!fileAccessHelper.ExternalStorageCanRead() || !fileAccessHelper.ExternalStorageCanWrite())
                    {
                        await Application.Current.MainPage.DisplayAlert(
                        Languages.ExceptionLiteral1,
                        Languages.OptionsLiteral6,
                        Languages.ExceptionLiteral3);
                        return;
                    }

                    IsEnabled = false;

                    var dbBackupFileName = "SECUTextDB-Backup.db3";

                    fileExists = fileAccessHelper.DoesFileExist(dbBackupFileName, false);
                    if (fileExists)
                    {
                        fileAccessHelper.DeleteFile(dbBackupFileName, false);
                    }

                    fileAccessHelper.CopyFile("SECUTextDB.db3", dbBackupFileName, true, 1);
                    fileExists = fileAccessHelper.DoesFileExist(dbBackupFileName, false);

                    IsEnabled = true;

                    if (fileExists)
                    {
                        DependencyService.Get<Toast>().Show(Languages.OptionsLiteral7);
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show(Languages.OptionsLiteral8);
                    }
                }
            }
            catch (Exception ex)
            {
                IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    Languages.ExceptionLiteral1,
                    Languages.ExceptionLiteral2,
                    Languages.ExceptionLiteral3);
            }
        }
        #endregion

        #region Helpers

        #endregion
    }
}
