using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
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

  public async Task<List<Post>> FindAll()
  {
    return await SQLServerDb.Posts.ToListAsync();
  }

  public async Task<Post> FindOneById(Guid id)
  {
    return await SQLServerDb.Posts.FirstOrDefaultAsync(u => u.Id == id);
  }
}