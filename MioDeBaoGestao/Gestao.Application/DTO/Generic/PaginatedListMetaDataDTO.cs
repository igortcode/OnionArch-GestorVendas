namespace Gestao.Application.DTO.Generic
{
    public class PagedListMetaDataDTO
    {
        public int PageCount { get;  private set; }

        public int PageNumber { get; private set; }

        public int PageSize { get;  private  set; }

        public bool HasPreviousPage { get; private set; }

        public bool HasNextPage { get; private set; }

        public PagedListMetaDataDTO(int pageCount, int pageNumber, int pageSize, bool hasPreviousPage, bool hasNextPage)
        {
            PageCount= pageCount;
            PageNumber= pageNumber;
            PageSize= pageSize;
            HasPreviousPage= hasPreviousPage;
            HasNextPage= hasNextPage;
        }

        public PagedListMetaDataDTO()
        {

        }

    }
}
