using Flunt.Notifications;
using ProjetoCompeticao.Application.Academias.Contracts;
using ProjetoCompeticao.Application.Academias.Dtos;
using ProjetoCompeticao.Infra.Integrations.Contracts;
using ProjetoCompeticao.Shared.Entities;
using ProjetoCompeticao.Shared.Enums;
using ProjetoCompeticao.Shared.Notifications;

namespace ProjetoCompeticao.Application.Academias.Services
{
    public class AcademiaEnderecoApplicationServices : IAcademiaEnderecoApplicationServices
    {
        private readonly INotificationServices _notificationServices;
        private readonly ICepServices _cepServices;

        public AcademiaEnderecoApplicationServices(INotificationServices notificationServices, ICepServices cepServices)
        {
            _notificationServices = notificationServices;
            _cepServices = cepServices;
        }

        public async Task<ICommandResult> BuscarEnderecoPorCepAsync(string cep)
        {
            if (string.IsNullOrEmpty(cep))
            {
                var notification = new Notification("cep", "O cep é inválido");

                _notificationServices.AddNotification(notification, StatusCodeOperation.BadRequest);

                return new CommandResult(notification, false, "Problemas ao efetuar a busca de cep.");
            }

            var enderecoEncontrado = await _cepServices.RecuperarEnderecoPorCepAsync(cep);

            if (enderecoEncontrado is null)
            {
                var notification = new Notification("cep", "Cep não encontrado");

                _notificationServices.AddNotification(notification, StatusCodeOperation.NotFound);

                return new CommandResult(notification, false);
            }

            var enderecoDto = new EnderecoDto
            {
                Rua = enderecoEncontrado.Logradouro,
                Bairro = enderecoEncontrado.Bairro,
                Cidade = enderecoEncontrado.Localidade,
                Cep = enderecoEncontrado.Cep,
                Estado = enderecoEncontrado.Uf
            };

            return new CommandResult(enderecoDto, true);
        }
    }
}
