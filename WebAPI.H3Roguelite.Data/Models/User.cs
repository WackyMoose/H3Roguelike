using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using WebAPI.Data.Models;

namespace WebAPI.H3Roguelite.Data.Models;

[Table("User")]
public class User : Model
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(64)]
    public string? Username { get; set; }

    [Required]
    [StringLength(256)]
    //[JsonIgnore]
    public string? Password { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [PropertyAfterSaveBehavior()]
    public DateTime Updated { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [PropertyAfterSaveBehavior()]
    public DateTime Created { get; set; }
}
