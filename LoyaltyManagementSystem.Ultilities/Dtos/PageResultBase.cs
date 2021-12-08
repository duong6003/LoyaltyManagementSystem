using System;

namespace LoyaltyManagementSystem.Ultilities.Dtos
{
    public class PageResultBase
    {
        public int CurrentIndex { get; set; }

        public int IndexCount
        {
            get
            {
                var pageCount = (double)RowCount / PageSize;
                return (int)Math.Ceiling(pageCount);
            }
            set { IndexCount = value; }
        }

        public int RowCount { get; set; }
        public int PageSize { get; set; }

        public int FirstRowOnPage
        {
            get
            {
                return (CurrentIndex - 1) * PageSize + 1;
            }
        }

        public int LastRowOnPage
        {
            get
            {
                return Math.Min(CurrentIndex * PageSize, RowCount);
            }
        }
    }
}