using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Controller;

namespace View
{
    public partial class ClientView : Form
    {
        ClientController clientCo = new ClientController();
        public ClientView()
        {
            InitializeComponent();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            LoadData();
            picPhoto.ImageLocation = "~/Content/Camera.jpg";
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            AvoidForm();
        }

        private void lnkSelect_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdPhoto.FileName = string.Empty;

            if (ofdPhoto.ShowDialog() == DialogResult.OK)
            {
                picPhoto.Load(ofdPhoto.FileName);
            }
            ofdPhoto.FileName = string.Empty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ClientModel client = new ClientModel();

            if (txtName.Text != string.Empty && txtLastName.Text != string.Empty)
            {
                client.Id = (int)nudID.Value;
                client.Name = txtName.Text;
                client.LastName = txtLastName.Text;
                client.Photo = picPhoto.ImageLocation;

                if (!clientCo.ValidateClient(client))
                {
                    if (client.Id == 0)
                    {
                        clientCo.CreateClient(client);
                    }
                    else
                    {
                        clientCo.UpdateClient(client);
                    }
                }
                else
                {
                    MessageBox.Show("Hubo un error en la validación de su cliente.");
                }
                client = new ClientModel();
            }
            LoadData();
            AvoidForm();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            ClientModel client = new ClientModel();

            if (txtName.Text != string.Empty && txtLastName.Text != string.Empty)
            {
                if (MessageBox.Show("¿Seguro que desea eliminar éste registro?", "Título", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    client.Id = (int)nudID.Value;
                    client.Name = txtName.Text;
                    client.LastName = txtLastName.Text;
                    client.Photo = picPhoto.ImageLocation;

                    clientCo.DeleteClient(client);
                }
            }

            client = new ClientModel();

            LoadData();
            AvoidForm();
        }

        public void AvoidForm() 
        {
            nudID.Value = 0;
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            picPhoto.ImageLocation = string.Empty;
        }
        private void LoadData() 
        { 
            grdClient.DataSource =  clientCo.ReadClients().Tables["tblClients"];
        }

        private void grdClient_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            nudID.Value = (int)grdClient.CurrentRow.Cells["Id"].Value;
            txtName.Text = grdClient.CurrentRow.Cells["Name"].Value.ToString();
            txtLastName.Text = grdClient.CurrentRow.Cells["LastName"].Value.ToString();
            picPhoto.ImageLocation = grdClient.CurrentRow.Cells["Photo"].Value.ToString();
        }

        private void grdClient_MouseHover(object sender, EventArgs e)
        {
            
        }
    }
}
