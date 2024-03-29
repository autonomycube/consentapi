﻿using Consent.Common.Repository.Helpers;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Consent.Common.Repository.Extensions
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class QueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize, bool IncludeTotalCount)
        {

            var total = IncludeTotalCount ? await query.CountAsync() : 0;
            var list = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(list, pageIndex, pageSize, total);
        }

        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var entities = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return entities as IQueryable<T>;
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string strSort, OrderBy direction)
        {
            if (source == null)
            {
                return source;
            }

            if (strSort == null)
            {
                return source;
            }

            //Get sort direction
            OrderBy enumDisplayStatus = direction;
            string sortDirection = enumDisplayStatus.ToString();

            var lstSort = strSort.Split(',');

            string sortExpression = string.Empty;

            foreach (var sortOption in lstSort)
            {

                sortExpression = sortExpression + sortOption + " " + sortDirection.ToLower() + ",";
            }

            if (!string.IsNullOrWhiteSpace(sortExpression))
            {
                // Note: system.linq.dynamic NuGet package is required here to operate OrderBy on string
                source = source.OrderBy(sortExpression.Remove(sortExpression.Count() - 1));
            }

            return source;
        }
    }
}
