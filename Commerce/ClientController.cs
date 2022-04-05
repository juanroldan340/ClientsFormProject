using Data;
using Model;
using System;
using System.Data;
using System.Windows.Forms;

namespace Controller
{
    public class ClientController
    {
        ClientData _data = new ClientData();
        public bool ValidateClient(ClientModel client) 
        {
            bool Error = false;
            if (client.Name == string.Empty && client.LastName == string.Empty) 
            {
                MessageBox.Show("Ingrese el nombre y el apellido de su cliente.");
            }
            else if (client.Name == string.Empty)
            {
                Error = true;
                MessageBox.Show("Ingrese el nombre de su cliente.");
            }
            else if (client.LastName == string.Empty) 
            {
                Error = true;
                MessageBox.Show("Ingrese el apellido de su cliente.");
            }

            return Error;
        }

        public void TryConnection()
        {
            
            _data.TryConnectionDb();
        }

        public void CreateClient(ClientModel client) 
        {
            _data.Create(client);
        }

        public void UpdateClient(ClientModel client)
        { 
            _data.Update(client);
        }

        public void DeleteClient(ClientModel client) 
        {
            _data.Delete(client);
        }

        public DataSet ReadClients() 
        {
            return _data.Read();
        }

        public bool ExistsClient(ClientModel client) 
        {
            bool Exists;
            Exists = _data.Exists(client.Id);
            return Exists;
        }
    }
}
