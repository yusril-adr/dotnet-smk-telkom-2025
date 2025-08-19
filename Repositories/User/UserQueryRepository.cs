using System.Linq.Expressions;
using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Infrastructure.Databases;
using dotnet_smk_telkom_2025.Infrastructure.Dtos;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_smk_telkom_2025.Repositories;

public class UserQueryRepository
{
  public readonly SQLServerDBContext SQLServerDB;

  public UserQueryRepository(
    SQLServerDBContext sqlServerDb
  )
  {
    SQLServerDB = sqlServerDb;
  }

  public async Task<(List<User> users, int totalCount)> FindAllPaginated(
  UserQueryParameter parameter
)
  {
    var query = SQLServerDB.Users.AsQueryable();

    int skip = (parameter.Page - 1) * parameter.PerPage;

    query = QuerySearch(query, parameter);
    query = QuerySort(query, parameter);

    var users = await query
      .Skip(skip)
      .Take(parameter.PerPage)
      .ToListAsync();
    var totalCount = await query.CountAsync();
    return (users, totalCount);
  }

  private IQueryable<User> QuerySearch(
    IQueryable<User> query,
    UserQueryParameter parameter
  )
  {
    if (!string.IsNullOrEmpty(parameter.Search))
    {
      query = query.Where(
        data =>
          EF.Functions.Like(data.Name, $"%{parameter.Search}%")
          || EF.Functions.Like(data.Email, $"%{parameter.Search}%")
        );
    }

    return query;
  }

  private IQueryable<User> QuerySort(
    IQueryable<User> query,
    UserQueryParameter parameter
  )
  {
    if (string.IsNullOrEmpty(parameter.SortBy))
    {
      parameter.SortBy = "updated_at";
    }

    Dictionary<string, Expression<Func<User, object>>> sortFunctions = new()
    {
      { "updated_at", data => data.UpdatedAt },
      { "created_at", data => data.CreatedAt },
      { "name", data => data.Name },
      { "email", data => data.Email },
    };

    if (!sortFunctions.TryGetValue(parameter.SortBy, out Expression<Func<User, object>> value))
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

  public async Task<List<User>> FindAll()
  {
    return await SQLServerDB.Users.ToListAsync();
  }

  public async Task<User> FindOneById(Guid id)
  {
    return await SQLServerDB.Users.FirstOrDefaultAsync(u => u.Id == id);
  }

  public async Task<User> FindOneByEmail(string email)
  {
    return await SQLServerDB.Users.FirstOrDefaultAsync(u => u.Email == email);
  }
}
