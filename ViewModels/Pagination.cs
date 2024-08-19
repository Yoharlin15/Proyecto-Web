namespace Proyecto_Web.ViewModels
{
    public class Pagination
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }

        public int PageCount => (int)decimal.Ceiling(Total / (decimal)PageSize);

        public bool HasPrevious => Page > 1;
        public bool HasNext => Page >= 1 && Page < PageCount;
        public static PaginationInfo GetInfo(int page, int pageSize)
        {
            return (new PaginationInfo (page, pageSize));
        }
    }

    public class PaginationInfo
    {
        public int Skip { get; }
        public int Take { get; }
        public PaginationInfo(int page, int pageSize)
        {
            Skip = (page - 1) * pageSize;
            Take = pageSize;
    
        }
    }
}
