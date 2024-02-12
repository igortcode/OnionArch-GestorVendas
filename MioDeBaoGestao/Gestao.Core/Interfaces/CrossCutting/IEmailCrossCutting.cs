using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Core.Interfaces.CrossCutting
{
    public interface IEmailCrossCutting
    {
        Task Enviar(
           string para,
           string userName,
           string template,
           string assunto,
           IEnumerable<KeyValuePair<string, string>> parametros = null);
    }
}
