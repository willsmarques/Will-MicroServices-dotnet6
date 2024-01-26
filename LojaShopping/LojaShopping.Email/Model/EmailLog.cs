using System.ComponentModel.DataAnnotations.Schema;
using LojaShopping.Email.Model.Base;

namespace LojaShopping.Email.Model
{
    [Table("email_logs")]
    public class EmailLog :BaseEntity
    {

        [Column("email")]
        public string Email {  get; set; }

        [Column("log")]
        public string Log {  get; set; }

        [Column("Data_Envio")]
        public DateTime DataEnvio { get; set; }

    }
}
