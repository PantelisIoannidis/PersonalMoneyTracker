using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMT.Models
{
    public class Pager
    {
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int Skip { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int FirstPage { get; set; }
        public int LastPage { get; set; }
        public bool IsFirstPage { get; set; }
        public bool IsLastPage { get; set; }
        public List<int> NearPages { get; set; }
        public Pager(int totalItems,int page=1,int pageSize = 10)
        {
            CurrentPage = page;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            if (TotalPages <= 0)
                TotalPages = 1;
            if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;
            Skip = ((CurrentPage - 1) * pageSize);
            HasNextPage = CurrentPage < TotalPages;
            HasPreviousPage = CurrentPage > 1;
            NextPage = HasNextPage ? CurrentPage + 1 : CurrentPage;
            PreviousPage = HasPreviousPage ? CurrentPage - 1 : CurrentPage;
            FirstPage = 1;
            LastPage = TotalPages;

            IsFirstPage = CurrentPage == 1;
            IsLastPage = (CurrentPage == LastPage);

            NearPages = new List<int>();
            var len = 3;
            for (int i = CurrentPage - len; i <= CurrentPage + len; i++)
                if (i > 0 && i <= TotalPages) NearPages.Add(i);

        }
    }
}
