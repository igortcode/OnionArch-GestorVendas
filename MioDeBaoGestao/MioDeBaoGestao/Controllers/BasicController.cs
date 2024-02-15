using Gestao.Application.DTO.Generic;
using Gestao.Application.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MioDeBaoGestao.Controllers
{
    public abstract class BasicController : Controller
    {
        public ViewResult AdapterView<T>(string viewName, T objeto, MessageDTO message)
        {
            AddNotification(message);

            return View(viewName, objeto);
        }

        public ViewResult AdapterView<T>(T objeto, MessageDTO message)
        {
            AddNotification(message);

            return View(objeto);
        }

        protected void AddNotification(MessageDTO message)
        {
            switch (message.TpNotif)
            {
                case TipoNotificacao.Sucess:
                    SuccessNotification(message.Mensagens);
                    break;
                case TipoNotificacao.Info:
                    InfoNotification(message.Mensagens);
                    break;
                case TipoNotificacao.Alert:
                    WarningNotification(message.Mensagens);
                    break;
                case TipoNotificacao.Erro:
                    ErrorNotification(message.Mensagens);
                    break;
            }


        }

        protected void AddNotification(params string[] message)
        {
            WarningNotification(message.ToList());
        }

        private void ErrorNotification(IList<string> mensagens)
        {
            ViewData["type"] = "Error";
            ViewData["messages"] = mensagens;
        }

        private void WarningNotification(IList<string> mensagens)
        {
            ViewData["type"] = "Warn";
            ViewData["messages"] = mensagens;
        }

        private void InfoNotification(IList<string> mensagens)
        {
            ViewData["type"] = "Info";
            ViewData["messages"] = mensagens;
        }

        private void SuccessNotification(IList<string> mensagens)
        {
            ViewData["type"] = "Success";
            ViewData["messages"] = mensagens;
        }
    }
}
