using SQLite;

namespace Progetto_Fitness_App_Finale;

public class UtenteService
{
    private static SQLiteAsyncConnection _database;

    public static async Task Init()
    {
        if (_database != null)
            return;

        var path = Path.Combine(FileSystem.AppDataDirectory, "Utenti.db");
        _database = new SQLiteAsyncConnection(path);

        await _database.CreateTableAsync<Utente>();
    }

    public static async Task<int> AggiungiUtente(Utente utente)
    {
        await Init();
        return await _database.InsertAsync(utente);
    }

    public static async Task<List<Utente>> PrendiUtenti()
    {
        await Init();
        return await _database.Table<Utente>().ToListAsync();
    }


    public static async Task<Utente> GetUtenteAsync()
    {
        await Init();

        return await _database.Table<Utente>().FirstOrDefaultAsync();
    }
    public static async Task<int> AggiornaUtente(Utente utente)
    {
        await Init();
        return await _database.UpdateAsync(utente);
    }




}