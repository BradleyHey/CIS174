using System.Linq.Expressions;

namespace FirstResponsiveWebAppHey.Models.DataLayer
{
    public class QueryOptions<T>
    {
        public Expression<Func<T, object>> OrderBy { get; set; } = null!;
        public Expression<Func<T, bool>> Where { get; set; } = null!;
        public string OrderByDirection { get; set; } = "asc";

        private string[] includes = Array.Empty<string>();

        public string Includes {
            set => includes = value.Replace(" ", "").Split(',');
        }

        public string[] GetIncludes() => includes;

        public bool HasWhere => Where != null;
        public bool HasOrderBy => OrderBy != null;
    }
}
