using System.ComponentModel.DataAnnotations;

namespace dotnet_smk_telkom_2025.Infrastructure.Dtos;

public enum SortOrder
{
  Asc,
  Desc,
}

public class QueryParameter
{
  public QueryParameter()
  {
    PerPage = 10;
    Page = 1;
    Order = SortOrder.Desc;
  }

  public string Search { get; set; }

  [Range(1, 100)]
  public int PerPage { get; set; }

  [Range(1, int.MaxValue)]
  public int Page { get; set; }

  public string SortBy { get; set; }

  public SortOrder Order { get; set; }
}
