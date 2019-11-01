using System;
using System.Collections.Generic;
using System.Text;
using SECU_Text.ViewModels;

namespace SECU_Text.Infrastructure
{
    public class InstanceLocator
    {
        #region Properties
        public MainViewModel Main { get; set; }
        #endregion

        #region Constructors
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion
    }
}
