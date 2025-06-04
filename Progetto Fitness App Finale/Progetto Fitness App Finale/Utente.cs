using SQLite;

namespace Progetto_Fitness_App_Finale;

public class Utente
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? NomeUtente { get; set; }
    public string? Password { get; set; }
    public int Eta { get; set; }
    public int Peso { get; set; }
    public int Altezza { get; set; }
    public string? Sesso { get; set; }
    public string? LivelloAtt { get; set; }
    public string? Obbiettivo { get; set; }
    public string? AllergiePreferenze { get; set; }
    //per salvataggio da implementare
    public string? PianoGenerato { get; set; }



}