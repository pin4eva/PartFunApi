
using System.ComponentModel.DataAnnotations;
using PartyFunApi.Model;

namespace PartyFunApi.DTO;

public class GetAllSalesDTO
{
  public int Id { get; set; }
  public Decimal UnitPrice { get; set; }
  public Decimal TotalAmount { get; set; }
  public Decimal? Discount { get; set; }
  public int Quantity { get; set; }
  public int CashierId { get; set; }
  public int ProductId { get; set; }
  public string ReceiptNo { get; set; } = string.Empty;
  public required string PaymentMethod { get; set; } = "Cash";
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}


public class GetSingleSalesDTO : GetAllSalesDTO
{
  public GetProductsDTO? Product { get; set; }
  public User? Cashier { get; set; }

}


public class CreateSalesDTO
{

  [Required, Range(1, int.MaxValue)] public Decimal UnitPrice { get; set; }
  [Required, Range(1, int.MaxValue)] public Decimal TotalAmount { get; set; }
  public Decimal? Discount { get; set; }
  [Range(1, 1000)] public int Quantity { get; set; }
  [Range(1, int.MaxValue)] public int ProductId { get; set; }
  [Required] public string PaymentMethod { get; set; } = "Cash";
  public string Note { get; set; } = string.Empty;
  public string PosNumber { get; set; } = string.Empty;
}

public class UpdateSalesDTO : CreateSalesDTO
{ public int Id { get; set; } }


public class CreateBulkSalesDTO
{
  public required List<CreateSalesDTO> Sales { get; set; } = [];

}
