﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RapportiWeb.Client.Pages;
using RapportiWeb.Server.Data;
using RapportiWeb.Shared;
using System.Buffers.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace RapportiWeb.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PDFController : ControllerBase
    {
		private readonly DataContext _context;
		private IJSRuntime _jsRunTime;
        private NavigationManager _nav;

        public PDFController(DataContext context, IJSRuntime jsRuntime, NavigationManager nav)
		{
			_context = context;
            _jsRunTime = jsRuntime;
            _nav = nav;
		}

        [HttpPost("DownloadRichiesta")]
        public async Task<string> DownloadRichiesta(Richiesta richiesta)
        {
            var pdfDocument = await CreateRichiestaPdf(richiesta);
            var pdfBytes = (pdfDocument).GeneratePdf();

            return Convert.ToBase64String(pdfBytes);
        }

        [HttpPost("DownloadRapporto")]
        public async Task<string> DownloadRapporto(Rapporto rap)
        {

            var pdfDocument = await CreateRapportoPdf(rap);
            var pdfBytes = (pdfDocument).GeneratePdf();

            return Convert.ToBase64String(pdfBytes);
        }



        private async Task<Document> CreateRichiestaPdf(Richiesta richiesta)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var listaClienti = await _context.Clienti.ToListAsync();
            var cliente = listaClienti.Find(c => c.id == richiesta.Clienteid);

            var listaRapporti = await _context.Rapporti.ToListAsync();
            var rapporti = listaRapporti.Find(r => r.id == richiesta.Clienteid);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(1, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                     .Row(row =>
                     {
                         row.RelativeColumn(0.5f).Image($"img/logo.png");
                         row.RelativeColumn(0.1f).Text("");
                         row.RelativeColumn(0.5f).Border((float) 0.5).AlignCenter().Text($"MODULO RICHIESTA DI INTERVENTO PRESSO IL CLIENTE: " +
                             $"                      Codice richiesta: {richiesta.id}" +
                             $"                               Data intervento : {richiesta.DataIntervento?.ToString("dd/MM/yyyy")}").Bold().FontSize((float) 14.5);

                     });

                    page.Content()
                 .PaddingVertical(1, Unit.Centimetre)
                 .Grid(grid =>
                 {
                     grid.VerticalSpacing(0);
                     grid.HorizontalSpacing(0);
                     grid.AlignCenter();
                     grid.Columns(10);

                     grid.Item(10).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"Ragione Sociale: {cliente.ragioneSociale} ").FontSize((float) 10.5);
                     grid.Item(10).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"Responsabile richiesta Sig./ra:  {richiesta.ResponsabileRic}").FontSize((float) 10.5);
                     grid.Item(3).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"Telefono:  {cliente.telefono} ").FontSize((float) 10.5);
                     grid.Item(3).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"Fax:  {cliente.telefono} ").FontSize((float) 10.5);
                     grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"Email:  {cliente.email} ").FontSize((float) 10.5);


                     grid.Item(10).Background(Colors.Grey.Lighten2).Border((float) 0.5).Padding(3).AlignCenter().Text($"Descrizione intervento richiesto: ").FontSize((float) 10.5);
                     grid.Item(10).Background(Colors.White).Border((float) 0.5).Padding(3).MinHeight(380).AlignLeft().Text($"{richiesta.Descrizione} ").FontSize((float) 10.5);

                     grid.Item(5).Background(Colors.Grey.Lighten2).Border((float) 0.5).Padding(3).AlignCenter().Text($"Figura Professionale Richiesta(barrare la casella) ").FontSize((float) 9.5);
                     grid.Item(5).Background(Colors.Grey.Lighten2).Border((float) 0.5).Padding(3).AlignCenter().Text($"Intervento (barrare la casella) ").FontSize((float) 9.5);

                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"{richiesta.FiguraProfessionale}");
                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"{richiesta.TipologiaIntervento}");


                     grid.Item(5).Background(Colors.Grey.Lighten2).Border((float) 0.5).Padding(3).AlignLeft().Text($"Data di intervento").FontSize((float) 9.5);
                     grid.Item(5).Background(Colors.Grey.Lighten2).Border((float) 0.5).Padding(3).AlignLeft().Text($"Durata di intervento").FontSize((float) 9.5);

                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"{richiesta.DataIntervento}").FontSize((float) 9.5);

                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"{richiesta.DurataIntervento}");



                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Pomeriggio");
                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Straordinario");

                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Height(50).Padding(3).AlignCenter().Text("Luogo e data").FontSize((float) 8.5);
                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Height(50).Padding(3).AlignCenter().Text("TIMBRO E FIRMA (l'incaricato)").FontSize((float) 8.5);

                     grid.Item(10).Background(Colors.White).BorderTop((float) 0.5).Padding(3).AlignCenter().Text("PRO CONSULTING SRL Viale Grigoletti 92/94 33170 Pordenone Tel +39 0434 555036 Fax +39 0434 552823").FontSize((float) 8.5);
                     grid.Item(10).Background(Colors.White).Padding(3).AlignCenter().Text("P.I. 01310340938").FontSize((float) 8.5);



                 });
                   

                });
            });

            return document;
        }



        private async Task<Document> CreateRapportoPdf(Rapporto rapporto)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var listaClienti = await _context.Clienti.ToListAsync();
            var cliente = listaClienti.Find(c => c.id == rapporto.Clienteid);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin((float)0.5, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                .Row(row =>
                {
                    row.RelativeColumn(0.5f).Image("img/logo.png");
                    row.RelativeColumn(0.1f).Text("");
                    row.RelativeColumn(0.5f).Border((float) 0.5).AlignCenter().Text($"RELAZIONE DI INTERVENTO           Codice richiesta: {rapporto.id}").Bold().FontSize((float) 14.5);

                });

                    page.Content()
                 .PaddingVertical(1, Unit.Centimetre)
                .Column(x =>
                {


                    x.Item().Border((float) 0.5).Padding(3).AlignLeft().Text(t =>
                    {
                        t.Span($"Ragione Sociale:  {cliente.ragioneSociale}").FontSize((float) 10.5).Bold(); ;

                    });
                    x.Item().Border((float) 0.5).Padding(3).AlignLeft().Text(t =>
                    {
                        t.Span($"Responsabile richiesta Sig./ra:  {cliente.Citta}").FontSize((float) 10.5);

                    });
                    x.Item().Text(t =>
                    {

                        t.Span("");

                    });
                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.Span($"Incaricato edll'intervento Sig./ra: {cliente.Indirizzo}").FontSize((float) 10.5).Bold();

                    });

                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.Span($"Data intervento: {rapporto.DataIntervento?.ToString("dd/MM/yy")}").FontSize((float) 10.5).Bold();

                    });

                    x.Item().Text(t =>
                    {
                        t.EmptyLine();
                        t.Span($"(Luogo Intervento)").FontSize(8);

                    });
                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.Span($"{rapporto.LuogoIntervento}").FontSize(10);

                    });
                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.Span($"Se intervento assistenza remota indica di seguito l'I.D. del collegamento remoto").FontSize(8);

                    });

                    x.Item().Border((float) 0.5).Background(Colors.Grey.Lighten2).AlignCenter().Text(t =>
                    {

                        t.Span("Descrizione:").FontSize((float) 10.5);

                    });
                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.Span($"{rapporto.Descrizione}").FontSize(10);
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();
                        //t.EmptyLine();


                    });
                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.Span($"IL CLIENTE E' TENUTO A CONSERVARE LA PRESENTE PER VERIFICA PRESTAZIONI EFFETTUATE ALLA RICEZIONE DELLA FATTURA ELETTORINICA CHE NON EREDITERA' DATA E CONTENUTO DELLA PRESTAZIONE MEDESIMA.").FontSize(7).Bold();

                    });
                    x.Item().Border((float) 0.5).Background(Colors.Grey.Lighten2).Text(t =>
                    {

                        t.Span($"Intervento Da:   {rapporto.TipoIntervento}                                           |                         Durata Intervento: {rapporto.DurataIntervento}                                     |                 PRO CONSULTING srl (l'incaricato)").FontSize(10);

                    });
                    x.Item().Border((float) 0.5).Text(t =>
                    {
                        t.Span($" {rapporto.TipoIntervento}                                                                                                {rapporto.DurataIntervento}").FontSize(9);

                        t.Span("   ");
                    });

                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.Span("CONDIZIONI GENERALI").FontSize(8);
                        t.EmptyLine();
                        t.Span("1. Con la presente relazione di intervento Pro Consulting srl attesta l'avvenuta esecuzione delle prestazioni sopra descritte. 2. Il Cliente con la firma apposta a fianco conferma l'accettazione della prestazione e autorizza l'addebito mediante fatturazione alle tariffe e condizioni concordate. 3. Dopo ogni singolo intervento il Cliente deve verificare il prodotto oggetto della prestazine. Eventali malfunzionamenti o reclami dovranno essere comunicati alla Pro Consulting srl entro e non oltre 30 giorni dalla data di accettazione della presente. 4. In ogni caso il pagamento della somma dovuta alla Pro Consulting srl in base al presente rapporto non dovrà essere ritardato o sospeso dal Cliente per nessuna ragione, con espressa rinuncia a qualsiasi eccezione. 5. Pro Consulting srl declina ogni responsabilità dall'inosservazione del Cliente delle norme per la salvaguardia dell'integrità del sistema informatico (si cita, a mero titolo esemplificativo, l'obbligo del Cliente di eseguire le copie di sicurezza su supporti previsti prima e dopo ogni intervento).")
                        .FontSize(7);
                        
                    });
                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.EmptyLine();
                        t.Span("Data e luogo_____________________________________________________").FontSize((float) 9);
                        t.Span("Timbro e Firma incaricato___________________________________________________").FontSize((float) 9);

                    });
                    x.Item().Border((float)0.5).Padding(3).Text(t =>
                    {
                       
                        t.Span("Il Cliente dichiara di aver letto attentamente e di accettare ai sensi e per gli effetti degli artt. 1341 e 1342 c.c. le presenti Condizioni Generali (con particolare attenzione agli artt.3,4,5,) e si impegna nei confronti di Pro Consulting a rispettarle.").FontSize(7).Bold();
                    });
                    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    {
                        t.EmptyLine();
                        t.Span("Data e luogo_____________________________________________________").FontSize((float) 9);
                        t.Span("Timbro e Firma incaricato___________________________________________________").FontSize((float) 9);

                    });
                    x.Item().AlignCenter().Text(t =>
                    {
                        t.Span("PRO CONSULTING SRL - Viale Grigoletti 92/94 33170 Prodenone Tel +39 0434 555036 Fax +39 0434 55823   P.I. 01310340938").FontSize(8).Bold();
                    });

                });



                });
            });

            return document;
        }


    }



}



