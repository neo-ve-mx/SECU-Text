using System;
using System.Collections.Generic;
using System.Text;

namespace SECU_Text.Models
{
    public class HomePageDetailEntryItem
    {
        public HomePageDetailEntryItem()
        {
            TargetType = typeof(HomePageDetailEntryItem);
        }
        public int Id { get; set; }
        public string Icon { get; set; }
        public string IconTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Type TargetType { get; set; }
    }
}
