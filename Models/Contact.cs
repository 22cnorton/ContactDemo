using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactDemo.Models;

public enum ContactType
{
    Friend, Family, Enemy
}

public class Contact
{
    public Guid Id { get; set; }

    [Required]
    [Display(Name = "First Name")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public required string FirstName { get; set; }
    [Required]
    [Display(Name = "Last Name")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public required string LastName { get; set; }

    [NotMapped]
    public string Name => $"{FirstName} {LastName}";

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number")]
    [Phone]
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }

    [EnumDataType(typeof(ContactType))]
    [Display(Name = "Contact Type")]
    public ContactType? Type { get; set; }
}