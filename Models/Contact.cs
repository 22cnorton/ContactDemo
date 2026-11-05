using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

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
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [Display(Name = "Last Name")]
    [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
    public string LastName { get; set; } = string.Empty;

    [NotMapped]
    public string Name => $"{FirstName} {LastName}";

    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? Email { get; set; }

    [DataType(DataType.PhoneNumber)]
    [Display(Name = "Phone Number")]
    [Phone]
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }

    [EnumDataType(typeof(ContactType))]
    [Display(Name = "Contact Type")]
    public ContactType? Type { get; set; }
    [DataType(DataType.Date)]
    public DateTime? Birthday { get; set; }
    [NotMapped]
    public bool HasInfo => !string.IsNullOrEmpty(PhoneNumber) || Address is not null || Type is not null || Birthday is not null || !string.IsNullOrEmpty(Email);

    [NotMapped]
    public int? Age
    {

        get
        {
            if (Birthday is DateTime birthday)
            {
                var now = DateTime.UtcNow;
                int years = now.Year - birthday.Year;

                if (birthday.Month == now.Month &&// if the start month and the end month are the same
                    now.Day < birthday.Day// AND the end day is less than the start day
                    || now.Month < birthday.Month)// OR if the end month is less than the start month
                {
                    years--;
                }

                return years;
            }
            else
            {
                return null;
            }
        }
    }
}

[Owned]
public class Address
{
    [Required]
    public string Line { get; set; } = string.Empty;
    public string? Line2 { get; set; }
    [Required]
    public string Town { get; set; } = string.Empty;
    [Required]
    public string State { get; set; } = string.Empty;
    [Required]
    [DataType(DataType.PostalCode)]
    [Unicode(false)]
    public string Zip { get; set; } = string.Empty;
}