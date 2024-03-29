﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gestao.Application.Interfaces.Facade
{
    public interface IEmailFacade
    {
        Task Enviar(
           string para,
           string userName,
           string template,
           string assunto,
           IEnumerable<KeyValuePair<string, string>> parametros = null);
    }
}
