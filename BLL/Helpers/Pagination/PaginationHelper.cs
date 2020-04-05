using System.Collections.Generic;
using System.Linq;

namespace BLL.Helpers.Pagination
{
    public static class PaginationHelper<T> where T : class
    {
        public static IEnumerable<T> GetPageValues(IEnumerable<T> entity, int pageSize, int pageNumber)
        {
            var result = entity
                .Skip(pageSize* (pageNumber- 1))
                .Take(pageSize);

            return result;
        }
    }
}
