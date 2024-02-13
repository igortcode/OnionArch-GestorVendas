using System;


namespace Gestao.Core.Validations.Exceptions
{
    public class DomainExceptionValidate : Exception
    {
        public DomainExceptionValidate(string message) : base(message) { }

    }
}
