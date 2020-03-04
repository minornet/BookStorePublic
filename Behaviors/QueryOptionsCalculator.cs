using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using BookStore.ViewModels;

namespace BookStore.Behaviors
{
    public class QueryOptionsCalculator
    {
        public static int CalculateStart(QueryOptions queryOptions)
        {
            return (queryOptions.CurPage - 1) * queryOptions.PageSize;
        }

        public static int CalculateTotalPages(int count, int pageSize)
        {
            return (int)Math.Ceiling((double)count / pageSize);
        }
    }
}