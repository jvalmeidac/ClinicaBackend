namespace backend.Domain.Pagination
{
    public class PageParameters
    {
        private int _pageSize = 10;
        private int _pageNumber = 0;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
        }

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }
            set
            {
                _pageNumber = (value == 1) ? 0 : (value - 1) * PageSize;
            }
        }
    }
}
