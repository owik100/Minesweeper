using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Controls;
using Dapper;

namespace Minesweeper.BazaDanych
{
    class Database
    {
       private SqlConnection con;

        public void Connect()
        {
            //con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|DatabaseMinesweeper.mdf;Integrated Security=True");
            con = new SqlConnection(@"Data Source=DESKTOP-FGH1MTQ\SQLEXPRESS;Initial Catalog=Baza_do_Sapera_Test;Integrated Security=True");

            con.Open();

            try
            {
               string s = @"CREATE TABLE Wyniki (ID int NOT NULL IDENTITY(1,1) PRIMARY KEY, Name varchar(255) NOT NULL, Time int NOT NULL)";

                SqlCommand cmd = new SqlCommand(s, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //Do naprawienia...
            }

        }

        public void AddData(string name, int time)
        {
            string querry = $"INSERT INTO Wyniki VALUES ('{name}','{time}')";

            con.Query(querry);

            PrintData();
        }

        public List <Wyniki> PrintData()
        {
            string querry = @"SELECT * FROM Wyniki";

            var wynikiList = con.Query<Wyniki>(querry).ToList();

            return wynikiList;

        }

        public void DeleteData(int id)
        {
            string querry = $@"DELETE FROM Wyniki WHERE ID={id}";

            con.Query(querry);
        }

        public void DeleteAll()
        {
            string querry = @"DELETE FROM Wyniki";

            con.Query(querry);
        }
    }
}
