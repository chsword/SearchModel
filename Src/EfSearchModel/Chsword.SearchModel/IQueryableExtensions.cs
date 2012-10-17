//-----------------------------------------------------------------------
// <copyright file="IQueryableExtensions.cs">
// </copyright>
// <author>Zou Jian</author>
// <addtime>2010-09-03</addtime>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Chsword
{
    /// <summary>
    /// 对IQueryable的扩展方法
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// zoujian add , 使IQueryable支持QueryModel
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="table">IQueryable的查询对象</param>
        /// <param name="model">QueryModel对象</param>
        /// <param name="prefix">使用前缀区分查询条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, SearchModel model, string prefix) where TEntity : class
        {
            Contract.Requires(table != null);
            if (table == null)
                throw new ArgumentNullException("table");
            if (model == null)
                throw new ArgumentNullException("model");
            return Where(table, model.Items, prefix);
        }

        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, SearchModel model) where TEntity : class
        {
            Contract.Requires(table != null);
            if (table == null)
                throw new ArgumentNullException("table");
            if (model == null)
                throw new ArgumentNullException("model");
            return Where(table, model.Items, string.Empty);
        }

        private static IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> table, IEnumerable<ConditionItem> items, string prefix = "")
        {
            Contract.Requires(table != null);   
            if (table == null)
                throw new ArgumentNullException("table");
            ICollection<ConditionItem> filterItems =
                (string.IsNullOrWhiteSpace(prefix)
                    ? items.Where(c => string.IsNullOrEmpty(c.Prefix))
                    : items.Where(c => c.Prefix == prefix)).ToList();
            if (!filterItems.Any()) return table;
            return new QueryableSearcher<TEntity>(table, filterItems).Search();
        }
    }
}
