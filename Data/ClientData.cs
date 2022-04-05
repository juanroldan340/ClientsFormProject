using System;
using Model;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Data;

namespace Data
{
    public class ClientData
    {
        private string ConnectionString = @"Data Source=LENOVO-IDEAPAD-\LOCALHOSTDB;Initial Catalog=ClientsForm;Persist Security Info=True;User ID=sa;Password=juansql340;MultipleActiveResultSets=True;";
        
        public void TryConnectionDb() 
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);

            try
            {
                sqlConnection.Open();
                MessageBox.Show("Conectado!");
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error de conexión: "+ex.Message);
            }
            sqlConnection.Close();
        }

        public void Create(ClientModel client) 
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            try
            {
                string query = $"INSERT INTO Clients(Name, LastName, Photo) VALUES('{client.Name}', '{client.LastName}', '{client.Photo}')";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();

                MessageBox.Show("Su cliente ha sido registrado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de registro: " + ex.Message);
            }
            sqlConnection.Close();
        }
        public DataSet Read() 
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            SqlDataAdapter adapter;
            DataSet data = new DataSet();
            try
            {
                string Query = "SELECT Id, Name, LastName, Photo FROM Clients";
                data = new DataSet();
                adapter = new SqlDataAdapter(Query, sqlConnection);

                adapter.Fill(data, "tblClients");
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al leer los datos: " + ex.Message);
                throw;
            }
            return data;
        }

        public void Update(ClientModel client) 
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            try
            {
                string query = $"UPDATE Clients SET Name = '{client.Name}', LastName = '{client.LastName}', Photo = '{client.Photo}' WHERE Id = '{ client.Id }'";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();

                MessageBox.Show("Su cliente ha sido actualizado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de actualización: " + ex.Message);
            }
            sqlConnection.Close();
        }

        public void Delete(ClientModel client) 
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();

            try
            {
                string query = $"DELETE FROM Clients WHERE Id = { client.Id }";
                SqlCommand command = new SqlCommand(query, sqlConnection);
                command.ExecuteNonQuery();

                MessageBox.Show("Su cliente ha sido eliminado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de eliminación: " + ex.Message);
            }
            sqlConnection.Close();
        }

        public bool Exists(int Id) 
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
            bool Exists = false;
            try
            {
                string Query = $"SELECT * FROM Clients WHERE Id = { Id }";
                SqlCommand command = new SqlCommand(Query, sqlConnection);
                if (command.ExecuteNonQuery() > 0) {
                    Exists = true;
                } 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar su cliente: " + ex.Message);
            }
            return Exists;
        }
    }
}
