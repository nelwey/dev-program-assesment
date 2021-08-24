using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment
{
    public class PaginationString : IPagination<string>
    {
        private readonly IEnumerable<string> data;
        private readonly int pageSize;
        private int currentPage;

        public PaginationString(string source, int pageSize, IElementsProvider<string> provider)
        {
            data = provider.ProcessData(source);
            currentPage = 1;
            this.pageSize = pageSize;
        }
        public void FirstPage()
        {
            this.currentPage = 1;
        }

        public void GoToPage(int page)
        {
            if (IsValidPage(page))
            {
                this.currentPage = page;
            }

        }
        public void LastPage()
        {
            this.currentPage = Pages();
        }

        public void NextPage()
        {
            if (IsValidPage(this.currentPage + 1))
            {
                this.currentPage++;
            }
        }

        public void PrevPage()
        {
            if (IsValidPage(this.currentPage - 1))
            {
                this.currentPage--;
            }
        }

        public IEnumerable<string> GetVisibleItems()
        {
            return data.Skip((currentPage - 1) * pageSize).Take(pageSize);
        }

        public int CurrentPage()
        {
            return this.currentPage;
        }

        public int Pages()
        {
            decimal aux = this.data.Count() / (decimal)this.pageSize;
            return Convert.ToInt32(Math.Ceiling(aux));
        }
        public bool IsValidPage(int pageNumber)
        {
            return pageNumber <= this.Pages() && pageNumber >= 1;
        }
    }
}