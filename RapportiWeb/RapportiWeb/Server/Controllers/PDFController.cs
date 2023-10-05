using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.JSInterop;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using RapportiWeb.Client.Pages;
using RapportiWeb.Server.Data;
using RapportiWeb.Shared;
using System.Security.Cryptography.X509Certificates;

namespace RapportiWeb.Server.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PDFController : ControllerBase
    {
		//await JSRuntime.InvokeAsync<object>("open", $"https://localhost:7124/PDF/{Cliente.ragioneSociale}_ric{Richiesta.id}.pdf", "_blank");

		private readonly DataContext _context;
		private IJSRuntime _jsRunTime;

		public PDFController(DataContext context, IJSRuntime jsRuntime)
		{
			_context = context;
            _jsRunTime = jsRuntime;
		}

		[HttpPost("DownloadRichiesta")]
        public async Task<IActionResult> DownloadRichiesta(Richiesta richiesta)
        {

            var listaClienti = await _context.Clienti.ToListAsync();
            var cliente = listaClienti.Find(c => c.id == richiesta.Clienteid);
            // Generate the PDF for Richiesta and save it on the server
            var pdfDocument = CreateRichiestaPdf(richiesta);
            var pdfBytes = (await pdfDocument).GeneratePdf();
            var pdfFileName = $"ric{richiesta.id}.pdf";

            var pdfFolder = "../Client/wwwroot/PDF";
            Directory.CreateDirectory(pdfFolder);
            var pdfFilePath = Path.Combine(pdfFolder, pdfFileName);

            System.IO.File.WriteAllBytes(pdfFilePath, pdfBytes);

			return Ok(pdfFileName);
        }

        [HttpGet("{FileName}")]
        public async Task GetFile(string filename)
        {
			await _jsRunTime.InvokeAsync<object>("open", $"/PDF/{filename}", "_blank");
		}

		[HttpPost("DownloadRapporto")]
        public async Task<IActionResult> DownloadRapporto(Rapporto rapporto)
        {
            var listaClienti = await _context.Clienti.ToListAsync();
            var cliente = listaClienti.Find(c => c.id == rapporto.Clienteid);

            // Generate the PDF for Richiesta and save it on the server
            var pdfDocument = CreateRapportoPdf(rapporto);
            var pdfBytes = (await pdfDocument).GeneratePdf();
            var pdfFileName = $"{rapporto.Clienteid}_rapporto_{rapporto.id}.pdf";

            var pdfFolder = Path.Combine(Directory.GetCurrentDirectory(), "PDFs");
            Directory.CreateDirectory(pdfFolder);
            var pdfFilePath = Path.Combine(pdfFolder, pdfFileName);

            System.IO.File.WriteAllBytes(pdfFilePath, pdfBytes);

            return Ok(pdfFileName);
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
                         row.RelativeColumn(0.5f).Image("../Client/wwwroot/img/logo.png");
                         row.RelativeColumn(0.1f).Text("");
                         row.RelativeColumn(0.5f).Border((float) 0.5).AlignCenter().Text($"MODULO RICHIESTA DI INTERVENTO PRESSO IL CLIENTE: " +
                             $"                      Codice richiesta: {richiesta.id}" +
                             $"                               Data intervento : {richiesta.DataIntervento?.ToString("dd/MM/yyyy")}").Bold().FontSize((float) 14.5);

                     });
                    //.Grid(grid =>
                    //{
                    //    grid.Item(5).AlignCenter().Image("../Client/wwwroot/img/logo.png");
                    //    grid.Item(1).Text("");
                    //    grid.Item(5).AlignCenter().Text("MODULO RICHIESTA DI INTERVENTO PRESSO IL CLIENTE:").Bold().FontSize((float) 12.5);

                    //    grid.Item(6).AlignCenter().Text("");
                    //    grid.Item(1).Text("");
                    //    grid.Item(4).AlignCenter().Border((float) 0.5).Padding(3).Text($"DATA RICHIESTA:    {richiesta.DataIntervento?.ToString("dd/MM/yyyy")}").Bold().FontSize((float) 12.5);

                    //    grid.Item(6).AlignCenter().Text("");
                    //    grid.Item(1).Text("");
                    //    grid.Item(4).AlignCenter().MinWidth(100000).MaxWidth(10000000).Border((float) 0.5).Padding(3).Text($"CODICE DELLA RICHIESTA:   {richiesta.id}").Bold().FontSize((float) 12.5);

                    //});

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
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Sistema Senior/Tecnico ERP");
                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Rif.Offerta/C.O.");

                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Consulente Applicativo Senior");
                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Contratto");

                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Consulente Master");
                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("In Garanzia");

                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Project Manager");
                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Altro");

                     grid.Item(5).Background(Colors.Grey.Lighten2).Border((float) 0.5).Padding(3).AlignLeft().Text($"Data di intervento").FontSize((float) 9.5);
                     grid.Item(5).Background(Colors.Grey.Lighten2).Border((float) 0.5).Padding(3).AlignLeft().Text($"Durata di intervento").FontSize((float) 9.5);

                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"{richiesta.DataIntervento}").FontSize((float) 9.5);

                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text($"{richiesta.DurataIntervento}");

                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Checkbox();
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Mattino");
                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Checkbox();
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Intera Giornata");



                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Pomeriggio");
                     //grid.Item(1).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("");
                     //grid.Item(4).Background(Colors.White).Border((float) 0.5).Padding(3).AlignLeft().Text("Straordinario");

                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Height(50).Padding(3).AlignCenter().Text("Luogo e data").FontSize((float) 8.5);
                     grid.Item(5).Background(Colors.White).Border((float) 0.5).Height(50).Padding(3).AlignCenter().Text("TIMBRO E FIRMA (l'incaricato)").FontSize((float) 8.5);

                     grid.Item(10).Background(Colors.White).BorderTop((float) 0.5).Padding(3).AlignCenter().Text("PRO CONSULTING SRL Viale Grigoletti 92/94 33170 Pordenone Tel +39 0434 555036 Fax +39 0434 552823").FontSize((float) 8.5);
                     grid.Item(10).Background(Colors.White).Padding(3).AlignCenter().Text("P.I. 01310340938").FontSize((float) 8.5);



                 });
                    //.Column(x =>
                    //{
                    //    x.Item().Border((float) 0.5).Padding(3).AlignLeft().Text(t =>
                    //    {
                    //        t.Span($"Ragione Sociale:  {cliente.ragioneSociale}").FontSize((float) 10.5);

                    //    });
                    //    x.Item().Border((float) 0.5).Padding(3).AlignLeft().Text(t =>
                    //    {
                    //        t.Span($"Responsabile richiesta Sig./ra:  {richiesta.ResponsabileRic}").FontSize((float) 10.5);

                    //    });
                    //    x.Item().Border((float) 0.5).Padding(3).Text(t =>
                    //    {
                    //        t.Span($"Indirizzo del cliente : {cliente.Indirizzo} {cliente.Citta}, {cliente.Provincia}, {cliente.Stato}").FontSize((float) 10.5);
                    //        t.EmptyLine();
                    //        t.Span($"Telefono: {cliente.telefono}").FontSize((float) 10.5);

                    //    });
                    //    x.Item().Border((float) 0.5).Background(Colors.Grey.Lighten2).AlignCenter().Text(t =>
                    //    {

                    //        t.Span("Descrizione intervento richiesta:").FontSize((float) 10.8);

                    //    });
                    //    x.Item().Border((float) 0.5).Text(t =>
                    //    {
                    //        t.Span($"{richiesta.Descrizione}").FontSize(10);

                    //        t.EmptyLine();

                    //    });


                    //    x.Item().Border((float) 0.5).Background(Colors.Grey.Lighten2).AlignLeft().Text(t =>
                    //    {
                    //        t.
                    //        t.Span($"Tipologia Intervento                                                                                                 |                                                           Figura Professionale Richiesta ").FontSize(10);

                    //    });
                    //    x.Item().Border((float) 0.5).Text(t =>
                    //    {
                    //        t.Span($"{richiesta.TipologiaIntervento}                                                                                                                                                                        {richiesta.FiguraProfessionale} ").FontSize(11);
                    //    });

                    //    x.Item().Border((float) 0.5).Background(Colors.Grey.Lighten2).AlignLeft().Text(t =>
                    //    {

                    //        t.Span($" Data di Intervento                                                                                                     |                                                                             Durata").FontSize(10);

                    //    });
                    //    x.Item().Border((float) 0.5).Text(t =>
                    //    {
                    //        t.Span($" data : {richiesta.DataIntervento?.ToString("dd/MM/yy")}                                                                                                                                                              {richiesta.DurataIntervento}").FontSize(11);
                    //        t.Span($"");
                    //    });

                    //    x.Item().Border((float) 0.5).Background(Colors.Grey.Lighten2).AlignCenter().Text(t =>
                    //    {
                    //        t.Span("Note").FontSize((float) 10.8);

                    //    });

                    //    x.Item().Border((float) 0.5).Text(t =>
                    //    {
                    //        t.Span("Il Cliente dichiara di aver letto attentamente e di accettare ai sensi" +
                    //            " e per gli effetti degli artt. 1341 e 1342 c.c. le presente Condizioni" +
                    //            "Generali (con particolare attenzione agli artt. 3,4,5,) e si impegna nei confronti di " +
                    //            "Pro Consulting Srl a rispettarle.").FontSize(9);


                    //    });
                    //    x.Item().Border((float) 0.5).Text(t =>
                    //    {
                    //        t.EmptyLine();
                    //        t.EmptyLine();
                    //        t.Span("Data e luogo_____________________________________________________").FontSize((float) 9);
                    //        t.Span("Timbro e Firma incaricato___________________________________________________").FontSize((float) 9);

                    //    });
                    //    x.Item().AlignCenter().Text(t =>
                    //    {
                    //        t.EmptyLine();
                    //        t.EmptyLine();
                    //        t.Span("PRO CONSULTING SRL- Viale Grigoletti 92/94 33170 Prodenone Tel +39 0434 555036 Fax +39 0434 55823   P.I. 01310340938").FontSize(10).Bold();
                    //    });
                    //});



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
                    row.RelativeColumn(0.5f).Image("../Shared/img/logo.jpg");
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



