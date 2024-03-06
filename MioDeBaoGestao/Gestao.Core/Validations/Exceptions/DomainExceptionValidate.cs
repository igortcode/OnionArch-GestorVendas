using System;


namespace Gestao.Core.Validations.Exceptions
{
    public class DomainExceptionValidate : Exception
    {
        public DomainExceptionValidate(string mensagem) : base(mensagem) { }


        public static void When(bool invalido, string mensagem)
        {
            if (invalido)
                throw new DomainExceptionValidate(mensagem);

        }
    }
}
