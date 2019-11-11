using SQLite;

namespace SECU_Text.Models
{
    public class T_Config
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [MaxLength(50)]
        public string ItemsOrder { get; set; }
        public bool FingerPrintAllow { get; set; }
    }
}
