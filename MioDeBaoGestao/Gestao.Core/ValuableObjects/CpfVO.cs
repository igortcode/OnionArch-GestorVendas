using Gestao.Core.Validations.Common;
using Gestao.Core.Validations.Exceptions;

namespace Gestao.Core.ValuableObjects
{
    public class CpfVO
    {
        public string Value { get; private set; }
        public CpfVO(string value)
        {
            validate(value);
            Value = value;
        }
        private void validate(string value)
        {
            DomainExceptionValidate.When(!GenericValidations.ValidateCPF(value), "Cpf inválido.");
        }
    }
}
