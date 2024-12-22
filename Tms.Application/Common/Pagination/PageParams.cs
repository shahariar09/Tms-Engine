using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tms.Application.Common.Pagination
{
    public class PageParams
    {
        public bool IsPaginationDisabled { get; set; }
        private const int MaxPageSize = 1000;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;
        private string? _searchKey = string.Empty;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? SearchKey
        {
            get => _searchKey;
            set => _searchKey = !string.IsNullOrEmpty(value) ? HttpUtility.UrlDecode(value) : value;
        }
    }
}
