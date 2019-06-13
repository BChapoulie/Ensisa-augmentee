
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;
using UnityEditor;
using System;

public class DBCommands
{
    static string connection;


    static IDbConnection dbcon;

    // Use this for initialization
    void Start() {
        connection = "URI=file:" + Application.persistentDataPath + "/" + "db7.db";
        dbcon = new SqliteConnection(connection);
    }

    public static void OpenDBConnection() {
        connection = "URI=file:" + Application.persistentDataPath + "/" + "db7.db";
        dbcon = new SqliteConnection(connection);
        dbcon.Open();
    }

    public static void CloseDBConnection() {
        dbcon.Close();
    }

    public static int InsertUser(string lastname, string firstname, string mail, string password) {
        IDbCommand cmnd = dbcon.CreateCommand();
        int id = LastId("Utilisateur") + 1;
        cmnd.CommandText = "INSERT INTO Utilisateur (id, prenom, nom, mail, mdp) VALUES (@id, @firstname,@lastname,@mail,@password)";
        cmnd.Parameters.Add(new SqliteParameter("@id", id));
        cmnd.Parameters.Add(new SqliteParameter("@firstname", firstname));
        cmnd.Parameters.Add(new SqliteParameter("@lastname", lastname));
        cmnd.Parameters.Add(new SqliteParameter("@mail", mail));
        cmnd.Parameters.Add(new SqliteParameter("@password", password));
        cmnd.ExecuteNonQuery();

        return id;
    }

    public static int InsertStudent(string firstname, string lastname, int idPromo, string mail, string password) {
        int id = InsertUser(lastname, firstname, mail, password);
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO Etudiant (id, idPromo) VALUES (@id, @idPromo)";
        cmnd.Parameters.Add(new SqliteParameter("@id", LastId("Utilisateur")));
        cmnd.Parameters.Add(new SqliteParameter("@idPromo", idPromo));
        cmnd.ExecuteNonQuery();

        return id;
    }

    public static int InsertTeacher(string firstname, string lastname, string mail, string password) {
        int id = InsertUser(lastname, firstname, mail, password);
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "INSERT INTO Professeur (id) VALUES (@id)";
        cmnd.Parameters.Add(new SqliteParameter("@id", LastId("Utilisateur")));
        cmnd.ExecuteNonQuery();

        return id;
    }

    public static int InsertRoom(int room, int floor, string name, string type, int nbPlaces, int nbPc) {
        IDbCommand cmnd = dbcon.CreateCommand();
        int id = LastId("Salle") + 1;
        cmnd.CommandText = "INSERT INTO Salle (id, salle, etage, nom, type, nbPlaces, nbPC) VALUES (@id, @room,@floor,@name,@type,@nbPlaces,@nbPc)";
        cmnd.Parameters.Add(new SqliteParameter("@id", id));
        cmnd.Parameters.Add(new SqliteParameter("@room", room));
        cmnd.Parameters.Add(new SqliteParameter("@floor", floor));
        cmnd.Parameters.Add(new SqliteParameter("@name", name));
        cmnd.Parameters.Add(new SqliteParameter("@type", type));
        cmnd.Parameters.Add(new SqliteParameter("@nbPlaces", nbPlaces));
        cmnd.Parameters.Add(new SqliteParameter("@nbPc", nbPc));
        cmnd.ExecuteNonQuery();

        return id;
    }

    public static int InsertLesson(int room, int floor, SQLiteDateFormats day, Time hour, Time duration, int idPromo, int idTeacher, string description) {
        IDbCommand cmnd = dbcon.CreateCommand();
        int id = LastId("Cours") + 1;
        cmnd.CommandText = "INSERT INTO Cours (id, salle, etage, jour, heure, duree, idPromo, idProf, description) VALUES (@id, @room, @day, @hour, @duration, @idPromo, @idTeacher, @description)";
        cmnd.Parameters.Add(new SqliteParameter("@id", id));
        cmnd.Parameters.Add(new SqliteParameter("@room", room));
        cmnd.Parameters.Add(new SqliteParameter("@floor", floor));
        cmnd.Parameters.Add(new SqliteParameter("@day", day));
        cmnd.Parameters.Add(new SqliteParameter("@hour", hour));
        cmnd.Parameters.Add(new SqliteParameter("@duration", duration));
        cmnd.Parameters.Add(new SqliteParameter("@idPromo", LastId("Promo")));
        cmnd.Parameters.Add(new SqliteParameter("@idTeacher", LastId("Professeur")));
        cmnd.Parameters.Add(new SqliteParameter("@description", description));
        cmnd.ExecuteNonQuery();

        return id;
    }

    public static int InsertPromo(int year, string spe) {
        IDbCommand cmnd = dbcon.CreateCommand();
        int id = LastId("Promo") + 1;
        cmnd.CommandText = "INSERT INTO Promo (id, annee, specialite) VALUES (@id, @year, @spe)";
        cmnd.Parameters.Add(new SqliteParameter("@id", id));
        cmnd.Parameters.Add(new SqliteParameter("@year", year));
        cmnd.Parameters.Add(new SqliteParameter("@spe", spe));
        cmnd.ExecuteNonQuery();

        return id;
    }


    public static int LastId(string table) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        int id = -1;
        string query = "SELECT MAX(id) FROM " + table;
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        while(reader.Read()) {
            id = Int32.Parse(reader[0].ToString());
        }

        return id;
    }


    public static int IsStudent(int id) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT idPromo FROM Etudiant WHERE id = @id";
        cmnd_read.Parameters.Add(new SqliteParameter("@id", id));
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int promo = -1;

        while (reader.Read()) {
            promo = Int32.Parse(reader[0].ToString());
        }

        return promo;
    }

    public static int IsStudent(string mail, string password) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT idPromo FROM Etudiant e INNER JOIN Utilisateur u on e.id = u.id WHERE mail = @mail AND mdp = @password ";
        cmnd_read.Parameters.Add(new SqliteParameter("@mail", mail));
        cmnd_read.Parameters.Add(new SqliteParameter("@password", password));
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();
        int promo = -1;

        while (reader.Read()) {
            promo = Int32.Parse(reader[0].ToString());
        }

        return promo;
    }

    /*public static Boolean Exist(string lastname, string firstname) {
        IDbCommand cmnd = dbcon.CreateCommand();
        cmnd.CommandText = "SELECT COUNT(id) FROM Etudiant WHERE id IN (SELECT id FROM Utilisateur WHERE prenom = @firstname AND nom = @lastname)";
        cmnd.Parameters.Add(new SqliteParameter("@lastname", lastname));
        cmnd.Parameters.Add(new SqliteParameter("@firstname", firstname));
        int nb = 0;
        try {
            nb = (int)cmnd.ExecuteScalar();
        }
        catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
        if (nb > 0) return true;
        else return false;
    }*/

    public static IDataReader RetrieveUser(int id) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        int iduser = IsStudent(id);
        if (iduser != -1) {
            string query = "SELECT prenom, nom, mail, mdp, annee, specialite from Utilisateur u INNER JOIN Etudiant e on u.id = e.id INNER JOIN Promo p on e.idPromo = p.id  ";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
            return reader;
        }
        else {
            string query = "SELECT prenom, nom, mail, mdp, description FROM Utilisateur u INNER JOIN Professeur p on u.id = p.id INNER JOIN Cours c on p.id = c.idProf ";
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
        }

        return reader;
    }

    public static IDataReader RetrieveUser(string mail, string password) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        int iduser = IsStudent(mail, password);
        if (iduser != -1) {
            string query = "SELECT prenom, nom, annee, specialite from Utilisateur u INNER JOIN Etudiant e on u.id = e.id INNER JOIN Promo p on p.id = e.idPromo WHERE mail = @mail AND mdp = @password";
            cmnd_read.Parameters.Add(new SqliteParameter("@mail", mail));
            cmnd_read.Parameters.Add(new SqliteParameter("@password", password));
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
        }
        else {
            string query = "SELECT prenom, nom FROM Utilisateur u INNER JOIN Professeur p on u.id = p.id WHERE mail = @mail AND mdp = @password";
            cmnd_read.Parameters.Add(new SqliteParameter("@mail", mail));
            cmnd_read.Parameters.Add(new SqliteParameter("@password", password));
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
        }

        return reader;
    }

    public static IDataReader RetrieveStudentPromo(string mail, string password) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader = null;
        int iduser = IsStudent(mail, password);
        if (iduser != -1) {
            string query = "SELECT annee, specialite from Utilisateur u INNER JOIN Etudiant e on u.id = e.id INNER JOIN Promo p on p.id = e.idPromo WHERE mail = @mail AND mdp = @password";
            cmnd_read.Parameters.Add(new SqliteParameter("@mail", mail));
            cmnd_read.Parameters.Add(new SqliteParameter("@password", password));
            cmnd_read.CommandText = query;
            reader = cmnd_read.ExecuteReader();
        }

        return reader;
    }

    public static IDataReader RetrieveRoomByID(int room, int floor) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT nom, type, nbPlaces, nbPC FROM Salle WHERE salle = @room and etage = @floor ";
        cmnd_read.Parameters.Add(new SqliteParameter("@room", room));
        cmnd_read.Parameters.Add(new SqliteParameter("@floor", floor));
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }

    public static IDataReader RetrieveRomByName(string name) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT salle, etage, type, nbPlaces, nbPC FROM Salle WHERE nom = @name ";
        cmnd_read.Parameters.Add(new SqliteParameter("@name", name));
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }

    public static IDataReader RetrieveLesson(Room room, DateTime horaire) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string jour = horaire.ToString("yyyy-MM-dd");
        string heure = horaire.ToString("HH:mm");
        string query = "SELECT description, nom, prenom, annee, specialite FROM Cours c INNER JOIN Utilisateur u on c.idProf = u.id INNER JOIN Promo p on c.idPromo = p.id WHERE salle = @room AND etage = @floor AND jour = @day AND heure = @hour";
        cmnd_read.Parameters.Add(new SqliteParameter("@day", jour));
        cmnd_read.Parameters.Add(new SqliteParameter("@hour", heure));
        cmnd_read.Parameters.Add(new SqliteParameter("@room", room.RoomId));
        cmnd_read.Parameters.Add(new SqliteParameter("@floor", room.Floor));
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }

    public static IDataReader RetrieveSchedule(DateTime dateStart, DateTime dateEnd) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string start = dateStart.ToString("yyyy-MM-dd");
        string end = dateEnd.ToString("yyyy-MM-dd");
        string query = "SELECT c.* nom, prenom FROM Cours c INNER JOIN Promo p on c.idPromo = p.id INNER JOIN Professeur a on c.idProf = a.id INNER JOIN Utilisateur u on u.id = c.idProf WHERE jour between @dateStart and @dateEnd";
        cmnd_read.Parameters.Add(new SqliteParameter("@daystart", dateStart));
        cmnd_read.Parameters.Add(new SqliteParameter("@dayend", dateEnd));
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }


    public static IDataReader UpdateUserProfile(User user, string nom, string prenom, string mail, string mdp) {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "UPDATE Utilisateur SET nom = @nom, prenom = @prenom, mail = @mail, mdp = @mdp WHERE nom = @oldname AND prenom = @oldfirstname";
        cmnd_read.Parameters.Add(new SqliteParameter("@oldname", user.Lastname));
        cmnd_read.Parameters.Add(new SqliteParameter("@oldfirstname", user.Firstname));
        cmnd_read.Parameters.Add(new SqliteParameter("@prenom", prenom));
        cmnd_read.Parameters.Add(new SqliteParameter("@nom", nom));
        cmnd_read.Parameters.Add(new SqliteParameter("@mail", mail));
        cmnd_read.Parameters.Add(new SqliteParameter("@mdp", mdp));
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }


    public static IDataReader RetrieveUsers() {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Utilisateur";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }

    public static IDataReader RetrieveRooms() {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Salle";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }

    public static IDataReader RetrieveLessons() {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Cours";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }

    public static IDataReader RetrievePromos() {
        IDbCommand cmnd_read = dbcon.CreateCommand();
        IDataReader reader;
        string query = "SELECT * FROM Promo";
        cmnd_read.CommandText = query;
        reader = cmnd_read.ExecuteReader();

        return reader;
    }
}

