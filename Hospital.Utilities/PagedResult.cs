using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Utilities
{

        public class PagedResult<T> where T : class
        {
            public PagedResult()
            {
            }

            public List<T> Data { get; set; }

            public int TotalItems { get; set; }

            public int PageNumber { get; set; }

            public int PageSize { get; set; }
        }

    }

