using Flunt.Notifications;
using Flunt.Validations;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Entities.Bases;

namespace ProjetoCompeticao.Domain.Academias.Entities
{
    public class Academia : BaseEntity, IValidateEntity
    {
        public string Nome { get; private set; }
        public string Endereco { get; private set; }

        protected Academia() { }

        public Academia(string nome, string endereco)
        {
            Nome = nome;
            Endereco = endereco;

            Validate();
        }

        public Academia AlterarNome(string nome)
        {
            Nome = nome;
            return this;
        }

        public Academia AlterarEndereco(string endereco)
        {
            Endereco = endereco;
            return this;
        }

        public void Validate()
        {
            AddNotifications(new Contract<Notification>()
                .Requires()
                .AreNotEquals(Id, Guid.Empty, "A Academia é inválida")
                .IsNotNullOrEmpty(Nome, nameof(Nome), "O nome é obrigatório.")
                .IsGreaterThan(Nome, 4, nameof(Nome), "O nome deve conter no mínimo 4 caracteres.")
                .IsLowerThan(Nome, 50, nameof(Nome), "O nome deve conter no máximo 50 caracteres")
                .IsNotNullOrEmpty(Endereco, nameof(Endereco), "O Endereço é obrigatório.")
                .IsGreaterThan(Endereco, 4, nameof(Endereco), "O Endereço deve conter no máximo 200 caracteres.")
                .IsLowerThan(Endereco, 200, nameof(Endereco), "O nome deve conter no mínimo 4 caracteres"));
        }
    }
}
