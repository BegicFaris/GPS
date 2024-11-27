using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GPS.API.Interfaces;

namespace GPS.API.Data.Models
{
    public class PassengerCreditCard : IMustHaveTenant
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Passenger))]
        public required int PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
        [ForeignKey(nameof(CreditCard))]
        public required int CreditCardId { get; set; }
        public CreditCard? CreditCard { get; set; }
        public required DateTime SavingDate { get; set; }
        public string? TenantId { get; set; }
    }
}
