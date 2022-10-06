using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PeliculasApi.DTOs;
using PeliculasApi.Extensions;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;


namespace PeliculasApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<List<TDTO>> FilterSortPaginate<TEntity, TDTO>(
            this IQueryable<TEntity> queryable,
            BaseFilter baseFilter,
            IMapper mapper,
            IActionContextAccessor actionContextAccessor) where TEntity : class, new()
        {
            queryable = queryable.Filter<TEntity>(baseFilter.Filters);

            var count = await queryable.CountAsync();

            queryable = queryable.Sort(baseFilter.Sort);

            queryable = queryable.Paginate(baseFilter.Range, actionContextAccessor, count);

            return mapper.Map<List<TDTO>>(queryable);
        }

        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> queryable, string filtersString)
            where TEntity : class, new()
        {
            if (String.IsNullOrEmpty(filtersString)) return queryable;

            // See comment on DTOs/Filters/BaseFilter.cs line 8
            var filters = JsonConvert.DeserializeObject<List<FilterValue>>(filtersString);
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    string query = $"{filter.Field} == @0"; // Basic query

                    try
                    {
                        // When filtering in parallel (OR) the queryable is already filtered at this point
                        if (!filter.Field.Contains("_OR")) queryable = queryable.Where(query, SanitizeFilterValue(filter.Value));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
            return queryable;
        }

        public static IQueryable<TEntity> Sort<TEntity>(this IQueryable<TEntity> queryable, Sort sort)
        {
            if (sort == null) return queryable;
            var order = sort.IsAscending ? "" : "descending";
            return queryable.OrderBy($"{sort.Field} {order}");
        }

        public static IQueryable<TEntity> Paginate<TEntity>(this IQueryable<TEntity> queryable, DTOs.Range range , IActionContextAccessor actionContextAccessor, int totalRecordsAmount)
        {
            var entityTypeName = typeof(TEntity).Name.ToLower();
            actionContextAccessor.ActionContext.HttpContext.InsertPaginationParams(entityTypeName, range.Start, range.End, totalRecordsAmount);
            return queryable.Skip(range.Start).Take(range.End - range.Start + 1);
        }

        private static string SanitizeFilterValue(JToken value)
        {
            return value.ToString().Replace("[", "").Replace("]", "").Replace("{", "").Replace("}", "").Replace("\"", "").Trim();
        }
    }
}
