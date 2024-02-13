using System.Collections.Generic;

namespace Gestao.Core.DTO.Generic
{
    public class GList<T>
    {
        public IList<T> DTOs { get; set; }

        public MessageDTO Message { get; set; }
    }
}
