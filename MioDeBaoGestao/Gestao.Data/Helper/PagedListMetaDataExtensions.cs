using Gestao.Application.DTO.Generic;
using X.PagedList;

namespace Gestao.Data.Helper
{
    public static class PagedListMetaDataExtensions
    {
        public static PagedListMetaData TryParceMetaDataDTO(this PagedListMetaData pagedListMetaData, out PagedListMetaDataDTO dto)
        {
            dto = new PagedListMetaDataDTO(
                    pagedListMetaData.PageCount, 
                    pagedListMetaData.PageNumber, 
                    pagedListMetaData.PageSize, 
                    pagedListMetaData.HasPreviousPage, 
                    pagedListMetaData.HasNextPage);


            return pagedListMetaData;
        }
    }
}
