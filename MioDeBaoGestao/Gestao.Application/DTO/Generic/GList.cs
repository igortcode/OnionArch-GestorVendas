using System.Collections.Generic;

namespace Gestao.Application.DTO.Generic
{
    public class GList<T>
    {
        public IList<T> DTOs { get; set; }

        public PagedListMetaDataDTO MetaData { get; set; }

        public MessageDTO Message { get; set; }
    }
}
