using Template.Application.Interfaces.External;
using Template.Domain.Aggregates.External.Clientes;
using Template.Domain.Aggregates.Clientes.Repository;
using AutoMapper;
using Polly;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Template.Application.Services.External
{
    public class ClienteExternalAppService : IClienteExternalAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IArquiteturaRobustaDocsApi _arquiteturaRobustaDocsApi;
        private readonly IMapper _mapper;

        private const int TENTATIVAS = 2;
        private const int TEMPO_ESPERA = 3;

        public ClienteExternalAppService(
            IClienteRepository clienteRepository,
            IArquiteturaRobustaDocsApi arquiteturaRobustaDocsApi,
            IMapper mapper
        )
        {
            _clienteRepository = clienteRepository;
            _arquiteturaRobustaDocsApi = arquiteturaRobustaDocsApi;
            _mapper = mapper;
        }
        public async Task<byte[]> GerarDocumentoClientes()
        {
            var clientes = await _clienteRepository.GetAllAsync();

            var pdf = await Policy
                            .Handle<Exception>()
                            .WaitAndRetryAsync(TENTATIVAS, retry => TimeSpan.FromSeconds(TEMPO_ESPERA))
                            .ExecuteAsync(async () => await _arquiteturaRobustaDocsApi.GerarDocumentoClientes(_mapper.Map<List<ClienteExternal>>(clientes)))
                            .ConfigureAwait(false);

            return await pdf.ReadAsByteArrayAsync();
        }
    }
}
