namespace PartyFunApi.Model;

public class Sales
{
  public int Id { get; set; }
  public Decimal UnitPrice { get; set; }
  public Decimal TotalAmount { get; set; }
  public Decimal? Discount { get; set; }
  public int Quantity { get; set; }
  public int CashierId { get; set; }
  public User Cashier { get; set; } = null!;
  public int ProductId { get; set; }
  public string InvoiceNo { get; set; } = string.Empty;
  public string Note { get; set; } = string.Empty;
  public string PosNumber { get; set; } = string.Empty;
  public Product Product { get; set; } = null!;
  public required string PaymentMethod { get; set; } = "Cash";

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }

}
