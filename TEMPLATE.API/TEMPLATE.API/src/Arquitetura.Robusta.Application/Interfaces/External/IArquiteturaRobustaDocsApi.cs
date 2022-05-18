using Template.Domain.Aggregates.External.Clientes;
using Refit;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Template.Application.Interfaces.External
{
    public interface IArquiteturaRobustaDocsApi
    {
        [Post("/docs/clientes")]
        Task<HttpContent> GerarDocumentoClientes(List<ClienteExternal> clienteExternal);
    }
}
