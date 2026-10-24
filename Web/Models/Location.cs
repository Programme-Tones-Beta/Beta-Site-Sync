using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSyncTracker.Models;

[Table("Locations")]
public class Location
{
    [Key] public int Id { get; set; }
    [Required, StringLength(100)] public string ZoneName { get; set; } = string.Empty;
    [StringLength(50)] public string? GPSCoordinates { get; set; }
    [StringLength(255)] public string? NotifyTo { get; set; }
}
