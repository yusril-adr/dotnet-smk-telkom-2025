
namespace dotnet_smk_telkom_2025.Infrastructure.Dtos
{
    public class PaginationMeta
    {
        public int TotalPage { get; set; }
        public int Total { get; set; }
        public int Page { get; set; }
        public int PerPage { get; set; }
    }

    public class PaginationResult<T>
    {
        public List<T> Items { get; set; }

        public PaginationMeta Meta { get; set; }

        public static PaginationResult<T> Parse(List<T> data, int total, QueryParameter query)
        {
            decimal pageInCount = ((decimal)total) / query.PerPage;

            return new PaginationResult<T>
            {
                Items = data,
                Meta = new PaginationMeta
                {
                    TotalPage = (int)Math.Ceiling(pageInCount),
                    Total = total,
                    Page = query.Page,
                    PerPage = query.PerPage,
                }
            };
        }
    }
}