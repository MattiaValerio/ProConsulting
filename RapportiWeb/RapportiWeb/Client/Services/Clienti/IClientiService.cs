using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Clienti
{
    public interface IClientiService
    {
        List<Cliente> ListaClienti { get; set; }

        List<Cliente> SearchedClienti { get; set; }


        Task<List<Cliente>> GetClienti();

        Task CreateCliente(Cliente Cliente);

        Task UpdateCliente(Cliente Cliente);

        Task <List<Cliente>> SearchClienti(string query);

        Task DeleteCliente(int Id);
    }
}
