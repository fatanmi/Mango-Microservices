namespace BaseLibrary.Implementation.Repository
{
    public class RequestParams
    {
        public int MaxPageSize { get; set; } = 50;
        public int PageNumber { get; set; } = 1;
        private int _PageSize { get; set; }
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = (value > 0 && value <= MaxPageSize) ? value : MaxPageSize; }
        }
    }
}
