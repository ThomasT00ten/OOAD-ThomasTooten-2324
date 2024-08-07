using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//configuration en sql
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace CLActiBuddy
{
    //public: zichtbaar binnen het gehele project

    public class Persoon
    {
        //properties
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Login { get; set; }
        //passwordhashing niet vergeten
        //passwoord is nu nullable terug aanpassen
        public string Paswoord { get; set; }

        public byte[] Profielfoto { get; set; }

        public DateTime Regdatum { get; set; }

        //0=nee, 1= ja
        public bool Isadmin { get; set; }
        static string connString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        // Details voor de personen weergeven
        public static List<Persoon> GetAll()
        {
            List<Persoon> personen = new List<Persoon>();
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand("SELECT id, voornaam, achternaam, login, paswoord, profielfoto, regdatum, isadmin FROM Persoon", connection);
                SqlDataReader reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    personen.Add(new Persoon()
                    {
                        Id = Convert.ToInt32(reader["id"]),
                        Voornaam = Convert.ToString(reader["voornaam"]),
                        Achternaam = Convert.ToString(reader["achternaam"]),
                        Login = Convert.ToString(reader["login"]),
                        Paswoord = Convert.ToString(reader["paswoord"]),
                        Profielfoto = reader["profielfoto"] != DBNull.Value ? (byte[])reader["profielfoto"] : null,
                        Regdatum = Convert.ToDateTime(reader["regdatum"]),
                        Isadmin = Convert.ToInt32(reader["isadmin"]) == 1 // Correct conversion
                    });
                }
            }
            return personen;
        }

        //Ids ophalen
        public static Persoon GetById(int persoonId)
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand("SELECT * FROM Persoon WHERE ID = @parID", connection);
                comm.Parameters.AddWithValue("@parID", persoonId);
                SqlDataReader reader = comm.ExecuteReader();
                if (!reader.Read()) return null;
                return new Persoon()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Voornaam = Convert.ToString(reader["voornaam"]),
                    Achternaam = Convert.ToString(reader["achternaam"]),
                    Login = Convert.ToString(reader["login"]),
                    Paswoord = Convert.ToString(reader["paswoord"]),
                    Profielfoto = reader["profielfoto"] != DBNull.Value ? (byte[])reader["profielfoto"] : null,
                    Regdatum = Convert.ToDateTime(reader["regdatum"]),
                    Isadmin = Convert.ToInt32(reader["isadmin"]) == 1
                };
            }
        }

        //Persoon toevoegen
        public void AddPersoon()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(@"
                    INSERT INTO Persoon (voornaam, achternaam, login, paswoord, profielfoto, regdatum, isadmin)
                    OUTPUT INSERTED.Id VALUES(@Voornaam, @Achternaam, @Login, @Paswoord, @Profielfoto, @Regdatum, @Isadmin)", connection);

                comm.Parameters.AddWithValue("@Voornaam", Voornaam);
                comm.Parameters.AddWithValue("@Achternaam", Achternaam);
                comm.Parameters.AddWithValue("@Login", Login);
                comm.Parameters.AddWithValue("@Paswoord", Paswoord != null ? (object)Paswoord : DBNull.Value);
                comm.Parameters.AddWithValue("@Profielfoto", Profielfoto != null ? (object)Profielfoto : DBNull.Value);
                comm.Parameters.AddWithValue("@Regdatum", Regdatum);
                comm.Parameters.AddWithValue("@Isadmin", Isadmin ? 1 : 0); // Convert boolean to integer
                Id = (int)comm.ExecuteScalar();
            }
        }
       
       
        //personenfiche wijzigen
        public void UpdatePersoon()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand(@"
            UPDATE Persoon 
            SET voornaam = @Voornaam, achternaam = @Achternaam, paswoord = @Paswoord, 
                profielfoto = @Profielfoto, isadmin = @Isadmin 
            WHERE id = @Id", connection);

                comm.Parameters.AddWithValue("@Voornaam", Voornaam);
                comm.Parameters.AddWithValue("@Achternaam", Achternaam);
                comm.Parameters.AddWithValue("@Paswoord", Paswoord != null ? (object)Paswoord : DBNull.Value);
                comm.Parameters.AddWithValue("@Profielfoto", Profielfoto != null ? (object)Profielfoto : DBNull.Value);
                comm.Parameters.AddWithValue("@Isadmin", Isadmin ? 1 : 0); // Convert boolean to integer
                comm.Parameters.AddWithValue("@Id", Id);

                comm.ExecuteNonQuery();
            }
        }



        public void DeleteFromDB()
        {
            using (SqlConnection connection = new SqlConnection(connString))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand("DELETE FROM Persoon WHERE ID = @parID", connection);
                comm.Parameters.AddWithValue("@parID", Id);
                comm.ExecuteNonQuery();
            }
        }
       

    }

}