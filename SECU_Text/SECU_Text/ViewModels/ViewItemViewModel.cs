using SECU_Text.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SECU_Text.ViewModels
{
    public class ViewItemViewModel : BaseViewModel
    {
        #region Atributes
        private string icon;
        private string icontitle;
        private string title;
        private string content;
        #endregion

        #region Properties
        public string Icon
        {
            get { return icon; }
            set { SetValue(ref icon, value); }
        }

        public string IconTitle 
        {
            get { return icontitle; }
            set { SetValue(ref icontitle, value); }
        }

        public string Title
        {
            get { return title; }
            set { SetValue(ref title, value); }
        }

        public string Content
        {
            get { return content; }
            set { SetValue(ref content, value); }
        }
        #endregion

        #region Constructors
        public ViewItemViewModel(T_Entry data)
        {
            if (data.Id != 0)
            {
                Icon = data.Icon;
                IconTitle = data.IconTitle;
                Title = data.Title;
                Content = Base64Decode(data.Content);
            }
        }
        #endregion

        #region Helpers
        //private static IEnumerable<T_Appuser> SELECT_WHERE(SQLiteConnection db, string name)
        //{
        //    return db.Query<T_Appuser>("SELECT * FROM T_Appuser where Name = ?", name);
        //}

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodeBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodeBytes);
        }
        #endregion
    }
}
