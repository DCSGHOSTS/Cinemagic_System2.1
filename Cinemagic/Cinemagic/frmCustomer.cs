﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Data.SqlClient;
using RandomProj;

namespace Cinemagic
{

    public partial class frmCustomer : Form
    {
        private string id;
        private string name;
        private string surname;
        private string phone;
        private string email;

        private string connection;
        private SqlCommand command;
        private DataTable dt = new DataTable();
        private SqlDataReader dr;


        public frmCustomer()
        {
            InitializeComponent();
        }


        public DialogResult AddCustomerFrom()
        {
            Form form = new Form();        

            Label lblName = new Label();
            Label lblSurname = new Label();
            Label lblEmail = new Label();
            Label lblPhone = new Label();

           
            lblName.Text = "Name:";
            lblSurname.Text = "Surname:";
            lblPhone.Text = "Phone:";
            lblEmail.Text = "E-Mail:";

            TextBox txtName = new TextBox();
            TextBox txtSurname = new TextBox();
         
            TextBox txtEmail = new TextBox();
            TextBox txtPhone = new TextBox();

            Button btnAdd = new Button();
            Button btnCancel = new Button();
            btnAdd.DialogResult = DialogResult.OK;
            btnCancel.DialogResult = DialogResult.Cancel;

            lblName.Location = new Point(60, 75);
            lblSurname.Location = new Point(60, 115);
            lblPhone.Location = new Point(60, 155);
            lblEmail.Location = new Point(60, 195);

            lblName.Size = new Size(80, 20);
            lblSurname.Size = new Size(80, 20);
            lblPhone.Size = new Size(80, 20);
            lblEmail.Size = new Size(80, 20);
            
            txtName.Location = new Point(150, 75);
            txtSurname.Location = new Point(150, 115);
            txtPhone.Location = new Point(150, 155);
            txtEmail.Location = new Point(150, 195);

            txtName.Size = new Size(140, 20);
            txtSurname.Size = new Size(140, 20);
            txtPhone.Size = new Size(140, 20);
            txtEmail.Size = new Size(140, 20);

            btnAdd.Size = new Size(80,40);
            btnCancel.Size = new Size(80, 40);
            btnAdd.Location = new Point(60, 300);
            btnCancel.Location = new Point(200, 300);
            btnAdd.Text = "Add customer";
            btnCancel.Text = "Cancel";

            form.Text = "Add Customer";
            form.ClientSize = new Size(500, 500);
            form.Controls.AddRange(new Control[] { lblName, lblSurname, lblPhone, lblEmail, txtName, txtSurname, txtPhone, txtEmail,btnAdd, btnCancel });

            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = btnAdd;
            form.CancelButton = btnCancel;

            DialogResult dialogResult = form.ShowDialog();
            
            name = txtName.Text;
            surname = txtSurname.Text;
            phone = txtPhone.Text;
            email = txtEmail.Text;

            if (dialogResult == DialogResult.Cancel)
            {
                form.Close();
            }
            else
            {              
                try
                {
                    Convert.ToInt32(txtPhone.Text);
                }
                catch
                {
                    if (dialogResult == DialogResult.Cancel)
                    {
                        form.Close();
                    }
                    MessageBox.Show("Phone number can only contain numbers");
                    txtName.Enabled = false;
                    txtSurname.Enabled = false;
                    txtEmail.Enabled = false;
                    txtPhone.Enabled = true;
                    txtPhone.Text = "";
                    form.ShowDialog();
                }
                if (txtPhone.Text.Length != 10)
                {
                    if (dialogResult == DialogResult.Cancel)
                    {
                        form.Close();
                    }
                    else
                    {
                        MessageBox.Show("Phone number should be 10 digits", "Invalid Phone Number");                       
                        txtName.Enabled = false;
                        txtSurname.Enabled = false;
                        txtEmail.Enabled = false;
                        txtPhone.Enabled = true;
                        txtPhone.Text = "";
                        form.ShowDialog();
                    }

                }
            }
            if (dialogResult == DialogResult.Cancel)
            {
                form.Close();
            }           
            return dialogResult;
        }

        private void DisplayCustomers()
        {
            Main cinema = new Main();
            connection = cinema.constr;
            cinema.conn = new SqlConnection(connection);
            cinema.conn.Open();
            string select_Customers = "SELECT * FROM CUSTOMER";
            cinema.com = new SqlCommand(select_Customers, cinema.conn);
            cinema.adap = new SqlDataAdapter();
            cinema.ds = new DataSet();
            cinema.adap = new SqlDataAdapter(select_Customers, cinema.conn);
            cinema.adap.Fill(cinema.ds, "Customers");
            dgCustomers.DataSource = cinema.ds;
            dgCustomers.DataMember = "Customers";
            cinema.conn.Close();
        }

        private void SearchCustomer(string detail)
        {
            Main cinema = new Main();
            connection = cinema.constr;
            cinema.conn = new SqlConnection(connection);
            cinema.conn.Open();
            string select_Customers = "SELECT * FROM CUSTOMER WHERE Customer_Name LIKE '%" + detail + "%' OR Customer_Surname LIKE '%"+ detail + "%' OR Customer_Phone LIKE '%" + detail + "%' OR Customer_Email LIKE '%" + detail + "%'" ;
            cinema.com = new SqlCommand(select_Customers, cinema.conn);
            cinema.adap = new SqlDataAdapter();
            cinema.ds = new DataSet();
            cinema.adap = new SqlDataAdapter(select_Customers, cinema.conn);
            cinema.adap.Fill(cinema.ds, "Customers");
            dgCustomers.DataSource = cinema.ds;
            dgCustomers.DataMember = "Customers";
            cinema.conn.Close();    
        }

        private void EditName(string newName)
        {
            Main cinema = new Main();
            connection = cinema.constr;
            cinema.conn = new SqlConnection(connection);
            cinema.conn.Open();
            string update_Name = "UPDATE CUSTOMER SET Customer_Name=@name";
            cinema.com = new SqlCommand(update_Name, cinema.conn);
            cinema.com.Parameters.AddWithValue("@Name", newName);
            cinema.com.ExecuteNonQuery();                     
            cinema.conn.Close();
            MessageBox.Show("Record Updated Successfully");
        }

        private void EditSurname(string newSurname)
        {
            Main cinema = new Main();
            connection = cinema.constr;
            cinema.conn = new SqlConnection(connection);
            cinema.conn.Open();
            string update_Surname = "UPDATE CUSTOMER SET Customer_Surname=@Surname";
            cinema.com = new SqlCommand(update_Surname, cinema.conn);
            cinema.com.Parameters.AddWithValue("@Surname", surname);
            cinema.com.ExecuteNonQuery();
            cinema.conn.Close();
            MessageBox.Show("Record Updated Successfully");
        }

        private void EditPhone(string newPhone)
        {
            Main cinema = new Main();
            connection = cinema.constr;
            cinema.conn = new SqlConnection(connection);
            cinema.conn.Open();
            string update_Phone = "UPDATE CUSTOMER SET Customer_Phone=@Phone";
            cinema.com = new SqlCommand(update_Phone, cinema.conn);
            cinema.com.Parameters.AddWithValue("@Phone", phone);
            cinema.com.ExecuteNonQuery();
            cinema.conn.Close();
            MessageBox.Show("Record Updated Successfully");
        }

        private void EditEmail(string newEmail)
        {
            Main cinema = new Main();
            connection = cinema.constr;
            cinema.conn = new SqlConnection(connection);
            cinema.conn.Open();
            string update_Email = "UPDATE CUSTOMER SET Customer_Email=@Email";
            cinema.com = new SqlCommand(update_Email, cinema.conn);
            cinema.com.Parameters.AddWithValue("@Email", email);
            cinema.com.ExecuteNonQuery();
            cinema.conn.Close();
            MessageBox.Show("Record Updated Successfully");
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomerFrom();
           
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            DisplayCustomers();
        }

        private void btnSearchCustomer_Click(object sender, EventArgs e)
        {
            string customerDetail = "";
            customerDetail = txtSearchCustomer.Text;

            try
            {
                SearchCustomer(customerDetail);
            }
            catch
            {              
                
            }                 
        }

        private void dgCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgCustomers_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void dgCustomers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            gbCustomerFields.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cbName.Checked == true)
            {
                EditName(txtEditName.Text);
                try
                {
                    
                }
                catch
                {

                }

            }
            if (cbSurname.Checked == true)
            {
                try
                {
                    EditSurname(txtEditSurname.Text);
                }
                catch
                {

                }
            }
            if (cbPhone.Checked == true)
            {
                try
                {
                    EditPhone(txtEditPhone.Text);
                }
                catch
                {
                    MessageBox.Show("Phone entered is invalid!", "Invalid Phone");
                }
            }
            if (cbEmail.Checked == true)
            {
                try
                {
                    EditEmail(txtEditEmail.Text);
                }
                catch
                {
                    MessageBox.Show("E-Mail Entered is invalid!", "Invalid Email");
                }
            }
        }

        private void cbName_CheckedChanged(object sender, EventArgs e)
        {
            txtEditName.Enabled = true;
        }

        private void cbSurname_CheckedChanged(object sender, EventArgs e)
        {
            txtEditSurname.Enabled = true;
        }

        private void cbPhone_CheckedChanged(object sender, EventArgs e)
        {
            txtEditPhone.Enabled = true;
        }

        private void cbEmail_CheckedChanged(object sender, EventArgs e)
        {
            txtEditEmail.Enabled = true;
        }
    }
}
