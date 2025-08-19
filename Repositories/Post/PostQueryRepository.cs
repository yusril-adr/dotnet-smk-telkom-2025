using System.Linq.Expressions;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostQueryRepository
{
  public readonly SQLServerDBContext SQLServerDb;

  public PostQueryRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDb = sqlServerDb;
  }

  public async Task<(List<Post> posts, int totalCount)> FindAllPaginated(
    PostQueryParameter parameter
  )
  {
    var query = SQLServerDb.Posts.AsQueryable();

    int skip = (parameter.Page - 1) * parameter.PerPage;

    query = QuerySearch(query, parameter);
    query = QueryFilter(query, parameter);
    query = QuerySort(query, parameter);

    var posts = await query
      .Skip(skip)
      .Take(parameter.PerPage)
      .ToListAsync();
    var totalCount = await query.CountAsync();
    return (posts, totalCount);
  }

  private IQueryable<Post> QuerySearch(
    IQueryable<Post> query,
    PostQueryParameter parameter
  )
  {
    if (!string.IsNullOrEmpty(parameter.Search))
    {
      query = query.Where(
        data =>
          EF.Functions.Like(data.Title, $"%{parameter.Search}%")
          || EF.Functions.Like(data.Content, $"%{parameter.Search}%")
        );
    }

    return query;
  }

  private IQueryable<Post> QueryFilter(
    IQueryable<Post> query,
    PostQueryParameter parameter
  )
  {
    if (parameter.AuthorUserId != Guid.Empty)
    {
      query = query.Where(data => data.AuthorUserId == parameter.AuthorUserId);
    }

    return query;
  }

  private IQueryable<Post> QuerySort(
    IQueryable<Post> query,
    PostQueryParameter parameter
  )
  {
    if (string.IsNullOrEmpty(parameter.SortBy))
    {
      parameter.SortBy = "updated_at";
    }

    Dictionary<string, Expression<Func<Post, object>>> sortFunctions = new()
    {
      { "updated_at", data => data.UpdatedAt },
      { "created_at", data => data.UpdatedAt },
      { "title", data => data.Title },
      { "content", data => data.Content },
    };

    if (!sortFunctions.TryGetValue(parameter.SortBy, out Expression<Func<Post, object>> value))
    {
      throw new BadParameterException(
        $"Invalid sort column: {parameter.SortBy}, available sort columns: " + string.Join(", ", sortFunctions.Keys)
      );
    }

    query = parameter.Order == SortOrder.Asc
        ? query.OrderBy(value)
        : query.OrderByDescending(value);

    return query;
  }

  public async Task<List<Post>> FindAll()
  {
    return await SQLServerDb.Posts.ToListAsync();
  }

  public async Task<Post> FindOneById(Guid id)
  {
    return await SQLServerDb.Posts.FirstOrDefaultAsync(u => u.Id == id);
  }
}