using SQLite;

namespace SECU_Text.Models
{
    public class T_Appuser
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
