using System.ComponentModel.DataAnnotations;

namespace API;

public class AppUser
{

    public int Id { get; set; } //because of the name Id , E.F is going to automatically use this as the primary key :)
    public string UserName { get; set;} //we can validate the username field here itself about if its empty or not by putting [Required] above this
     // and carrying out the migrations but we'll do this migration at thte DTO level.                           
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; } // after adding prop tell the db to create new columns by migration
    
}
