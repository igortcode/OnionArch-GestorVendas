﻿using Gestao.Application.Enums;
using System;
using System.Collections.Generic;

namespace Gestao.Application.DTO.Generic
{
    public class MessageDTO
    {
        public string Mensagem { get; set; }
        public IList<string> Mensagens { get; set; } = new List<string>();
        public TipoNotificacao TpNotif { get; set; }
        public Exception Ex { get; set; }


        public MessageDTO(string mensagem, TipoNotificacao tpNotif = TipoNotificacao.Sucess, Exception ex = null)
        {
            Mensagem = mensagem;
            TpNotif = tpNotif;
            Ex = ex;

            Mensagens.Add(Mensagem);
        }

        public MessageDTO(IList<string> mensagens, TipoNotificacao tpNotif = TipoNotificacao.Sucess, Exception ex = null)
        {
            Mensagens = mensagens;
            TpNotif = tpNotif;
            Ex = ex;

            
        }
    }
}
