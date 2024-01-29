namespace PartyFunApi.Model;

public class Sales
{
  public int Id { get; set; }
  public Decimal UnitPrice { get; set; }
  public Decimal TotalAmount { get; set; }
  public Decimal? Discount { get; set; }
  public int Quantity { get; set; }
  public int CashierId { get; set; }
  public int ProductId { get; set; }
  public required Product Product { get; set; }
  public required string PaymentMethod { get; set; } = "Cash";

}
