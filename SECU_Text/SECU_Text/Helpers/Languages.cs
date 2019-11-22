using Xamarin.Forms;
using SECU_Text.Interfaces;
using SECU_Text.Resources;

namespace SECU_Text.Helpers
{
    public static class Languages
    {
        static Languages()
        {
            var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
            Resource.Culture = ci;
            DependencyService.Get<ILocalize>().SetLocale(ci);
        }

        #region AppLiterals
        public static string AppLiteral1 { get { return Resource.AppLiteral1; } }
        public static string AppLiteral2 { get { return Resource.AppLiteral2; } }
        public static string AppLiteral3 { get { return Resource.AppLiteral3; } }
        public static string AppLiteral4 { get { return Resource.AppLiteral4; } }
        public static string AppLiteral5 { get { return Resource.AppLiteral5; } }
        public static string AppLiteral6 { get { return Resource.AppLiteral6; } }
        public static string AppLiteral7 { get { return Resource.AppLiteral7; } }
        public static string AppLiteral8 { get { return Resource.AppLiteral8; } }
        #endregion

        #region Exception Error
        public static string ExceptionLiteral1 { get { return Resource.ExceptionLiteral1; } }
        public static string ExceptionLiteral2 { get { return Resource.ExceptionLiteral2; } }
        public static string ExceptionLiteral3 { get { return Resource.ExceptionLiteral3; } }
        #endregion

        #region LoginViewModel and LoginView
        public static string LoginLiteral1 { get { return Resource.LoginLiteral1; } }
        public static string LoginLiteral2 { get { return Resource.LoginLiteral2; } }
        public static string LoginLiteral3 { get { return Resource.LoginLiteral3; } }
        public static string LoginLiteral4 { get { return Resource.LoginLiteral4; } }
        public static string LoginLiteral5 { get { return Resource.LoginLiteral5; } }
        public static string LoginLiteral6 { get { return Resource.LoginLiteral6; } }
        public static string LoginLiteral7 { get { return Resource.LoginLiteral7; } }
        #endregion

        #region RegisterViewModel and RegisterView
        public static string RegisterLiteral1 { get { return Resource.RegisterLiteral1; } }
        public static string RegisterLiteral2 { get { return Resource.RegisterLiteral2; } }
        public static string RegisterLiteral3 { get { return Resource.RegisterLiteral3; } }
        public static string RegisterLiteral4 { get { return Resource.RegisterLiteral4; } }
        #endregion

        #region HomePageDetailViewModel and HomePageDetailView
        public static string HomePageDetailLiteral1 { get { return Resource.HomePageDetailLiteral1; } }
        #endregion

        #region HomePageMasterViewModel and HomePageMasterView
        public static string HomePageMasterLiteral1 { get { return Resource.HomePageMasterLiteral1; } }
        public static string HomePageMasterLiteral2 { get { return Resource.HomePageMasterLiteral2; } }
        public static string HomePageMasterLiteral3 { get { return Resource.HomePageMasterLiteral3; } }
        public static string HomePageMasterLiteral4 { get { return Resource.HomePageMasterLiteral4; } }
        #endregion

        #region SecurityViewModel and SecurityView
        public static string SecurityLiteral1 { get { return Resource.SecurityLiteral1; } }
        public static string SecurityLiteral2 { get { return Resource.SecurityLiteral2; } }
        public static string SecurityLiteral3 { get { return Resource.SecurityLiteral3; } }
        public static string SecurityLiteral4 { get { return Resource.SecurityLiteral4; } }
        public static string SecurityLiteral5 { get { return Resource.SecurityLiteral5; } }
        public static string SecurityLiteral6 { get { return Resource.SecurityLiteral6; } }
        public static string SecurityLiteral7 { get { return Resource.SecurityLiteral7; } }
        public static string SecurityLiteral8 { get { return Resource.SecurityLiteral8; } }
        #endregion

        #region AboutViewModel and AboutView
        public static string AboutLiteral1 { get { return Resource.AboutLiteral1; } }
        public static string AboutLiteral2 { get { return Resource.AboutLiteral2; } }
        public static string AboutLiteral3 { get { return Resource.AboutLiteral3; } }
        public static string AboutLiteral4 { get { return Resource.AboutLiteral4; } }
        public static string AboutLiteral5 { get { return Resource.AboutLiteral5; } }
        #endregion

        #region ViewItemViewModel and ViewItemView
        public static string ViewItemLiteral1 { get { return Resource.ViewItemLiteral1; } }
        public static string ViewItemLiteral2 { get { return Resource.ViewItemLiteral2; } }
        public static string ViewItemLiteral3 { get { return Resource.ViewItemLiteral3; } }
        public static string ViewItemLiteral4 { get { return Resource.ViewItemLiteral4; } }
        public static string ViewItemLiteral5 { get { return Resource.ViewItemLiteral5; } }
        public static string ViewItemLiteral6 { get { return Resource.ViewItemLiteral6; } }
        public static string ViewItemLiteral7 { get { return Resource.ViewItemLiteral7; } }
        public static string ViewItemLiteral8 { get { return Resource.ViewItemLiteral8; } }
        public static string ViewItemLiteral9 { get { return Resource.ViewItemLiteral9; } }
        public static string ViewItemLiteral10 { get { return Resource.ViewItemLiteral10; } }
        public static string ViewItemLiteral11 { get { return Resource.ViewItemLiteral11; } }
        #endregion

        #region ItemTypes
        public static string ItemTypeLiteral1 { get { return Resource.ItemTypeLiteral1; } }
        public static string ItemTypeLiteral2 { get { return Resource.ItemTypeLiteral2; } }
        public static string ItemTypeLiteral3 { get { return Resource.ItemTypeLiteral3; } }
        public static string ItemTypeLiteral4 { get { return Resource.ItemTypeLiteral4; } }
        public static string ItemTypeLiteral5 { get { return Resource.ItemTypeLiteral5; } }
        #endregion

        #region AddItemViewModel and AddItemView
        public static string AddItemLiteral1 { get { return Resource.AddItemLiteral1; } }
        public static string AddItemLiteral2 { get { return Resource.AddItemLiteral2; } }
        public static string AddItemLiteral3 { get { return Resource.AddItemLiteral3; } }
        public static string AddItemLiteral4 { get { return Resource.AddItemLiteral4; } }
        public static string AddItemLiteral5 { get { return Resource.AddItemLiteral5; } }
        public static string AddItemLiteral6 { get { return Resource.AddItemLiteral6; } }
        #endregion

        #region EditItemViewModel and EditItemView
        public static string EditItemLiteral1 { get { return Resource.EditItemLiteral1; } }
        public static string EditItemLiteral2 { get { return Resource.EditItemLiteral2; } }
        #endregion

        #region OptionsViewModel and OptionsView
        public static string OptionsLiteral1 { get { return Resource.OptionsLiteral1; } }
        public static string OptionsLiteral2 { get { return Resource.OptionsLiteral2; } }
        public static string OptionsLiteral3 { get { return Resource.OptionsLiteral3; } }
        public static string OptionsLiteral4 { get { return Resource.OptionsLiteral4; } }
        public static string OptionsLiteral5 { get { return Resource.OptionsLiteral5; } }
        public static string OptionsLiteral6 { get { return Resource.OptionsLiteral6; } }
        public static string OptionsLiteral7 { get { return Resource.OptionsLiteral7; } }
        public static string OptionsLiteral8 { get { return Resource.OptionsLiteral8; } }
        public static string OptionsLiteral9 { get { return Resource.OptionsLiteral9; } }
        public static string OptionsLiteral10 { get { return Resource.OptionsLiteral10; } }
        public static string OptionsLiteral11 { get { return Resource.OptionsLiteral11; } }
        public static string OptionsLiteral12 { get { return Resource.OptionsLiteral12; } }
        public static string OptionsLiteral13 { get { return Resource.OptionsLiteral13; } }
        public static string OptionsLiteral14 { get { return Resource.OptionsLiteral14; } }
        #endregion
    }
}
