using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStore.ViewModels
{
    public class QueryOptions
    {
        // want the user to be able to change these . . .
        public QueryOptions()
        {
            CurPage = 1;
            PageSize = 5;
            SortField = "Id";
            SortOrder = SortOrder.ASC;
        }

        public int CurPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }
        
        public string SortField { get; set; }

        public SortOrder SortOrder { get; set; }

        public string Sort
        {
            get
            {
                return string.Format("{0} {1}", SortField, SortOrder.ToString());
            }
            // need validation, if string etc . . .
            set { }
        }
    }
}