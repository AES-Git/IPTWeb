using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestmentPortfolioTracker.Web.Models;

[Table("holdings", Schema = "iptweb_dbo")]
public class Holding
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    private string _symbol = string.Empty;
    
    [Required]
    [MaxLength(10)]
    [Column("symbol")]
    public string Symbol
    {
        get => _symbol;
        set => _symbol = value?.ToUpperInvariant() ?? string.Empty;
    }

    [Required]
    [MaxLength(100)]
    [Column("assetname")]
    public string AssetName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    [Column("assettype")]
    public string AssetType { get; set; } = string.Empty;

    [Required]
    [Column("quantity", TypeName = "decimal(18,8)")]
    public decimal Quantity { get; set; }

    [Required]
    [Column("purchaseprice", TypeName = "decimal(18,2)")]
    public decimal PurchasePrice { get; set; }

    [Required]
    [Column("purchasedate")]
    public DateTime PurchaseDate { get; set; }

    [Column("currentprice", TypeName = "decimal(18,2)")]
    public decimal? CurrentPrice { get; set; }

    [Column("lastpriceupdate")]
    public DateTime? LastPriceUpdate { get; set; }

    [Column("createdat")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Calculated properties
    [NotMapped]
    public decimal CostBasis => Quantity * PurchasePrice;

    [NotMapped]
    public decimal? CurrentValue => CurrentPrice.HasValue ? Quantity * CurrentPrice.Value : null;

    [NotMapped]
    public decimal? GainLoss => CurrentValue.HasValue ? CurrentValue.Value - CostBasis : null;

    [NotMapped]
    public decimal? GainLossPercent => CostBasis > 0 && GainLoss.HasValue ? (GainLoss.Value / CostBasis) * 100 : null;
}