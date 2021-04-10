namespace backend.Domain.Pagination
{
    public class PageParameters
    {
        private int _pageSize = 10;
        private int _pageNumber = 0;
        const int MaxPageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
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
