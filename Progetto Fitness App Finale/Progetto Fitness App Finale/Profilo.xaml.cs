namespace Progetto_Fitness_App_Finale;

public partial class Profilo : ContentPage
{
	public Utente UtenteLog { get; set; }
	public Profilo(Utente utente)
	{
		InitializeComponent();
		//CaricaPianoGenerato();
		UtenteLog = utente;
	}

	private async void OnVisualizzaPianoClicked(object sender, EventArgs e)
	{
		await DisplayAlert("Piano", PianiGeneratiEditor.Text, "close");

	}
	private async void LogOutClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());

	}
	private async void ModificaDati_Clicked(object sender, EventArgs e)
	{
		var utente = UtenteLog;

		if (utente != null)
		{
			string nome = utente.NomeUtente;
			string password = utente.Password;
			await Navigation.PushAsync(new inserimento_dati(nome, password));
		}
		else
		{
			await DisplayAlert("Errore", "Impossibile caricare i dati utente.", "OK");
		}
	}
	private async void CaricaPianoGenerato()
	{
		try
		{
			var utente = UtenteLog;

			if (utente != null)
			{
				Console.WriteLine("DEBUG: PianoGenerato ricevuto dal database:");
				Console.WriteLine(utente.PianoGenerato ?? "NULL o vuoto"); //se utente piano è nullo, allora stampa null

				if (!string.IsNullOrWhiteSpace(utente.PianoGenerato))
				{
					PianiGeneratiEditor.Text = utente.PianoGenerato;
				}
				else
				{
					PianiGeneratiEditor.Text = "Nessun piano salvato.";
				}
			}
			else
			{
				await DisplayAlert("Errore", "Impossibile recuperare il profilo utente TRALLALERO TRALLALA", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Errore", $"Qualcosa è andato storto: {ex.Message}", "OK");
		}
	}




	protected override async void OnAppearing() //per errore
	{
		base.OnAppearing();

		var utente = UtenteLog;

		if (utente != null)
		{
			NomeLabel.Text = $"Nome: {utente.NomeUtente}";
			EtaLabel.Text = $"Età: {utente.Eta}";
			PesoLabel.Text = $"Peso: {utente.Peso} kg";
			AltezzaLabel.Text = $"Altezza: {utente.Altezza} cm";

			PianiGeneratiEditor.Text = !string.IsNullOrWhiteSpace(utente.PianoGenerato)
				? utente.PianoGenerato
				: "Nessun piano salvato.";
		}
		else
		{
			NomeLabel.Text = "Nome: (non trovato)";
			EtaLabel.Text = "Età: -";
			PesoLabel.Text = "Peso: -";
			AltezzaLabel.Text = "Altezza: -";

			PianiGeneratiEditor.Text = "Piano non disponibile.";
		}
	}




}