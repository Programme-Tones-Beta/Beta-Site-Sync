using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSyncTracker.Models;

[Table("Statuses")]
public class Status
{
    [Key] public int Id { get; set; }
    [Required, StringLength(50)] public string StatusName { get; set; } = string.Empty;
    [StringLength(255)] public string? NotifyTo { get; set; }
}
