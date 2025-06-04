namespace Progetto_Fitness_App_Finale;

public partial class inserimento_dati : ContentPage
{
	public string _nome { get; set; }
	public string _password { get; set; }

	public inserimento_dati(string nome, string password)
	{
		InitializeComponent();
		_nome = nome;
		_password = password;
	}

	private async void OnSubmitClicked(object sender, EventArgs e)
	{
		if (!int.TryParse(AgeEntry.Text, out _) ||
			!int.TryParse(WeightEntry.Text, out _) || //valore che viene restituito al metodo
			!int.TryParse(HeightEntry.Text, out _) ||
			GenderPicker.SelectedIndex == -1 ||
			ActivityLevelPicker.SelectedIndex == -1 || //elemento selezionato canbia , non selezionato -1
			TargetPicker.SelectedIndex == -1)
		{
			await DisplayAlert("Errore", "Compila tutti i campi obbligatori con valori validi.", "OK");
			return;
		}

		var nuovoUtente = new Utente
		{
			NomeUtente = _nome,
			Password = _password,
			Eta = int.Parse(AgeEntry.Text),
			Peso = int.Parse(WeightEntry.Text),
			Altezza = int.Parse(HeightEntry.Text),
			Sesso = GenderPicker.SelectedItem?.ToString(),
			LivelloAtt = ActivityLevelPicker.SelectedItem?.ToString(),
			Obbiettivo = TargetPicker.SelectedItem?.ToString(),
			AllergiePreferenze = AllergieEntry.Text
		};

		await UtenteService.Init();
		var utenti = await UtenteService.PrendiUtenti();

		var utenteEsistente = utenti.FirstOrDefault(u => u.NomeUtente == _nome && u.Password == _password);

		if (utenteEsistente != null)
		{
			utenteEsistente.Eta = nuovoUtente.Eta;
			utenteEsistente.Peso = nuovoUtente.Peso;
			utenteEsistente.Altezza = nuovoUtente.Altezza;
			utenteEsistente.Sesso = nuovoUtente.Sesso;
			utenteEsistente.LivelloAtt = nuovoUtente.LivelloAtt;
			utenteEsistente.Obbiettivo = nuovoUtente.Obbiettivo;
			utenteEsistente.AllergiePreferenze = nuovoUtente.AllergiePreferenze;

			await UtenteService.AggiornaUtente(utenteEsistente);
		}
		else
		{
			await UtenteService.AggiungiUtente(nuovoUtente);
		}

		await DisplayAlert("Successo", "Dati salvati nel database!", "OK");
		await Navigation.PushAsync(new Pagina_AI(nuovoUtente));
	}
}
