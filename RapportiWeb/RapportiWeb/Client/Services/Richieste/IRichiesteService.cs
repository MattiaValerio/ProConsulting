﻿using MudBlazor;
using RapportiWeb.Shared;

namespace RapportiWeb.Client.Services.Richieste
{
    public interface IRichiesteService
    {
        List<Richiesta> ListaRichieste { get; set; }
        List<Richiesta> SearchedRichieste { get; set; }
        List<Richiesta> Filter { get; set; }
        string[] RagioniSociali { get; set; }
        
        Task<List<Richiesta>> GetRichieste();

        Task<string[]> GetRagioniSociali();

        Task<Richiesta?> GetRichiestaById(int id);
        
        Task<List<Richiesta>> GetRichiestaByRagSoc(string ragsoc);

        Task CreateRichiesta(Richiesta Richiesta);

        Task UpdateRichiesta(int id, Richiesta Richiesta);
        
        Task DeleteRichiesta(int id);

        Task<List<Richiesta>> RicercaPerData(DateTime? start, DateTime? end);
    }
}
