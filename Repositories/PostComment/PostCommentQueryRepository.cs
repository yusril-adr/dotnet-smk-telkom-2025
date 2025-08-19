using System.Linq.Expressions;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_smk_telkom_2025.Repositories;

public class PostCommentQueryRepository
{
  public SQLServerDBContext SQLServerDb { get; set; }

  public PostCommentQueryRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDb = sqlServerDb;
  }

  public async Task<(List<PostComment> postComments, int totalCount)> FindAllPaginated(
    PostCommentQueryParameter parameter
  )
  {
    var query = SQLServerDb.PostComments.AsQueryable();

    int skip = (parameter.Page - 1) * parameter.PerPage;

    query = QuerySearch(query, parameter);
    query = QueryFilter(query, parameter);
    query = QuerySort(query, parameter);

    var postComments = await query
      .Skip(skip)
      .Take(parameter.PerPage)
      .ToListAsync();
    var totalCount = await query.CountAsync();
    return (postComments, totalCount);
  }

  private IQueryable<PostComment> QuerySearch(
    IQueryable<PostComment> query,
    PostCommentQueryParameter parameter
  )
  {
    if (!string.IsNullOrEmpty(parameter.Search))
    {
      query = query.Where(
        data => EF.Functions.Like(data.Content, $"%{parameter.Search}%")
        );
    }

    return query;
  }

  private IQueryable<PostComment> QueryFilter(
    IQueryable<PostComment> query,
    PostCommentQueryParameter parameter
  )
  {
    if (parameter.PostId != Guid.Empty)
    {
      query = query.Where(data => data.PostId == parameter.PostId);
    }

    if (parameter.AuthorUserId != Guid.Empty)
    {
      query = query.Where(data => data.AuthorUserId == parameter.AuthorUserId);
    }

    return query;
  }

  private IQueryable<PostComment> QuerySort(
    IQueryable<PostComment> query,
    PostCommentQueryParameter parameter
  )
  {
    if (string.IsNullOrEmpty(parameter.SortBy))
    {
      parameter.SortBy = "updated_at";
    }

    Dictionary<string, Expression<Func<PostComment, object>>> sortFunctions = new()
    {
      { "updated_at", data => data.UpdatedAt },
      { "created_at", data => data.CreatedAt },
      { "content", data => data.Content },
    };

    if (!sortFunctions.TryGetValue(parameter.SortBy, out Expression<Func<PostComment, object>> value))
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

  public async Task<List<PostComment>> FindAll()
  {
    return await SQLServerDb.PostComments.ToListAsync();
  }

  public async Task<PostComment> FindOneById(Guid id)
  {
    return await SQLServerDb.PostComments.FirstOrDefaultAsync(u => u.Id == id);
  }
}
