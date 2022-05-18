using System.Threading.Tasks;

namespace Template.Application.Services.External
{
    public interface IClienteExternalAppService
    {
        Task<byte[]> GerarDocumentoClientes();
    }
}
