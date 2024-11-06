using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RS1_2024_25.API.Data.Models
{
    public class PassengerCreditCard
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Passenger))]
        public int PassengerId { get; set; }
        public Passenger Passenger { get; set; }
        [ForeignKey(nameof(CreditCard))]
        public int CreditCardId { get; set;}
        public CreditCard CreditCard { get; set; }
        public DateTime SavingDate { get; set; }
    }
}
