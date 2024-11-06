using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Drawing;

namespace RS1_2024_25.API.Data.Models.Auth;

public class MyAppUser
{
    [Key]
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime RegistrationDate{ get; set; }
    public byte[] Image { get; set; }
    public string Address { get; set; }
    public bool Status { get; set; }

    //----------------
    public bool IsAdmin { get; set; }
    public bool IsManager { get; set; }

    /*
     
     Ako sistem nije zamišljen da podržava česte promjene rola i 
     ako se dodavanje novih rola svodi na manje promjene u kodu, 
    tada može biti dovoljno koristiti boolean polja kao što su IsAdmin, IsManager itd. 
    
    Ovaj pristup je jednostavan i efektivan u situacijama gdje su role stabilne i unaprijed definirane.

    Međutim, glavna prednost korištenja role entiteta dolazi do izražaja kada aplikacija potencijalno raste i 
    zahtjeva kompleksnije role i ovlaštenja. U scenarijima gdje se očekuje veći broj različitih rola ili kompleksniji 
    sistem ovlaštenja, dodavanje nove bool varijable može postati nepraktično i otežati održavanje.

    Dakle, za stabilne sisteme s manjim brojem fiksnih rola, boolean polja su sasvim razumno rješenje.
     */

}
