using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteSyncTracker.Models;

[Table("InventoryItems")]
public class InventoryItem
{
    [Key] public int Id { get; set; }
    [Required, StringLength(50)] public string ManifestNumber { get; set; } = string.Empty;
    [Required, StringLength(255)] public string Description { get; set; } = string.Empty;
    [Required] public int QuantityExpected { get; set; }
    [Required] public int QuantityReceived { get; set; }
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    [Required] public int CurrentStatusId { get; set; }
    [ForeignKey(nameof(CurrentStatusId))] public Status? CurrentStatus { get; set; }
    [Required] public int CurrentLocationId { get; set; }
    [ForeignKey(nameof(CurrentLocationId))] public Location? CurrentLocation { get; set; }
    [StringLength(255)] public string? NotifyTo { get; set; }
}
