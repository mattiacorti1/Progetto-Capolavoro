namespace Progetto_Fitness_App_Finale;

public partial class MainPage : ContentPage

{

	public MainPage()
	{
		InitializeComponent();
		

	}
	private async void OnNavigateClicked(object sender, EventArgs e)
	{
		string nome = NomeEntry.Text;
		string password = PasswordEntry.Text;
		
		if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(password))
		{
			await DisplayAlert("Errore", "Inserisci nome e password", "OK");
			return;
		}


		await Navigation.PushAsync(new inserimento_dati(nome, password));
	}

	private async void LogOnNavigateClicked(object sender, EventArgs e)
	{
		string nome = NomeEntry.Text;
		string password = PasswordEntry.Text;

		List<Utente> utentiDb = await UtenteService.PrendiUtenti();


		Utente utente = utentiDb.FirstOrDefault(u => u.NomeUtente == nome && u.Password == password);

		if (utente != null)
		{

			await Navigation.PushAsync(new Pagina_AI(utente));
		}
		else
		{
			await DisplayAlert("Accesso negato", "Password errata o utente non trovato", "Ok");
		}
	}




}


