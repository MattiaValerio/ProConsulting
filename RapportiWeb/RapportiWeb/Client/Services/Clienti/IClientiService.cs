using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Clienti
{
    public interface IClientiService
    {
        List<Cliente> ListaClienti { get; set; }

        List<Cliente> SearchedClienti { get; set; }

        Task<string[]> GetArrayClienti();

		Task<List<Cliente>> GetClienti();

        Task<Cliente> GetCliente(string ragsoc);
        Task<Cliente> GetClienteById(int id);

        Task CreateCliente(Cliente Cliente);

        Task UpdateCliente(Cliente Cliente);

        Task <List<Cliente>> SearchClienti(string query);

        Task DeleteCliente(int Id);
    }
}
