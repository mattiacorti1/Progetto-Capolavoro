namespace Progetto_Fitness_App_Finale
{
	public partial class Pagina_AI : ContentPage
	{
		private readonly Utente _utente;
		private readonly HttpClient _client;

		public Pagina_AI(Utente utente)
		{
			InitializeComponent();


			if (utente == null)
			{
				DisplayAlert("Errore", "Utente non valido", "OK");
				return;
			}

			_utente = utente;
			_client = HttpClientHelper.CreateHttpClient();



		}

		private async void OnProfiloClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Profilo(_utente));
		}

		private async void OnGeneraPianoClicked(object sender, EventArgs e)
		{
			try
			{
				if (_utente == null)
				{
					await DisplayAlert("Errore", "Utente non trovato", "OK");
					return;
				}

				// Costruisci il prompt con i dettagli dell'utente
				string prompt = CostruisciPromptConProfilo(_utente);
				Console.WriteLine(prompt);

				// Ottieni la risposta dal servizio Gemini
				string risposta = await GeminiAIService.GeneraPianoAlimentareAsync(prompt, _client);
				Console.WriteLine(risposta);

				// Se la risposta non è vuota, mostra e salva
				if (!string.IsNullOrWhiteSpace(risposta))
				{
					// Mostra il piano alimentare generato nell'interfaccia
					PianoAI1.Text = risposta;

					// Salva il testo generato nel profilo utente
					_utente.PianoGenerato = risposta; // Assicurati che _utente abbia il campo TestoGenerato
					await UtenteService.AggiornaUtente(_utente); // Aggiorna il database

					
					await DisplayAlert("Successo", "Piano alimentare generato e salvato!", "OK");
				}
				else
				{
					await DisplayAlert("Errore", "Risposta vuota dal server", "OK");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Errore dettagliato: {ex}");
				await DisplayAlert("Errore", $"Qualcosa è andato storto:\n{ex.Message}", "OK");
			}
		}

		private string CostruisciPromptConProfilo(Utente utente)
		{
			return $"Crea un piano alimentare giornaliero per una persona con queste caratteristiche:\n" +
				   $"- Età: {utente.Eta}\n" +
				   $"- Peso: {utente.Peso} kg\n" +
				   $"- Altezza: {utente.Altezza} cm\n" +
				   $"- Sesso: (se viene indicato altro è perchè l'utente non è ne maschio ne femmina) {utente.Sesso}\n" +
				   $"- Livello di attività: {utente.LivelloAtt}\n" +
				   $"- Obiettivo: {utente.Obbiettivo}\n" +
				   $"- Allergie/Preferenze: {utente.AllergiePreferenze ?? "Nessuna"}\n\n" +
				   $"Includi colazione, pranzo, cena e spuntini, con alimenti consigliati." +
				   $"Inoltre Genera un Piano da Allenamento per questa persona, Schematizza il piano in modo che sia chiaro e comprensibile, " +
				   $"includendo esercizi, ripetizioni e serie. SCRIVI SOLO IL PIANO, NON SCRIVERE ALTRO, NON AGGIUNGERE COMMENTI O INFORMAZIONI SUPPLEMENTARI.";
				   
		}





	}
}