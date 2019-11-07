using SQLite;

namespace SECU_Text.Models
{
    public class T_Entry
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Icon { get; set; }
        [MaxLength(100)]
        public string IconTitle { get; set; }
        public int IconIndex { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        [MaxLength(800)]
        public string Content { get; set; }
    }
}
