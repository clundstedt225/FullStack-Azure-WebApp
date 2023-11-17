using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using Model;
using ServiceBus;
using Repositories;

using System.Configuration;
using System.Text.RegularExpressions;

namespace GUI_Pubs
{
    public partial class frmMain : Form
    {
        pubsService pubsService; 

        public frmMain()
        {
            InitializeComponent();

            lvAuthors.View = View.Details;
            lvAuthors.GridLines = true;
            lvAuthors.FullRowSelect = true;
            lvAuthors.MultiSelect = false;

            lvBooks.View = View.Details;
            lvBooks.GridLines = true;
            lvBooks.FullRowSelect = true;
            lvBooks.MultiSelect = false;

            lvAllBooks.View = View.Details;
            lvAllBooks.GridLines = true;
            lvAllBooks.FullRowSelect = true;
            lvAllBooks.MultiSelect = false;

            //lvAuthors.Columns.Add("Last");
            //lvAuthors.Columns.Add("First");

            string dbConnection = configFile.getSetting("pubsDBConnectionString");

            //AuthorRepoFile AuRepo = new AuthorRepoFile("D:\\authors-1.csv");
            AuthorRepoDB AuRepoDB = new AuthorRepoDB(dbConnection);
            BookRepoDB BkRepoDB = new BookRepoDB(dbConnection);

            //Give the service layer the repos being used
            pubsService = new pubsService(AuRepoDB, BkRepoDB);

            //init columns
            // using statement: object is correctly disposed of
            using (SqlConnection connection = new SqlConnection(dbConnection))//StreamReader reader = File.OpenText(_connectionString)
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM authors", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            lvAuthors.Columns.Add(reader.GetName(i));
                        }
                    }// end using reader 
                }// end using command  

                using (SqlCommand command = new SqlCommand("SELECT * FROM titles", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            lvBooks.Columns.Add(reader.GetName(i));
                            lvAllBooks.Columns.Add(reader.GetName(i));
                        }
                    }// end using reader 
                }// end using command
            }
        }

        //List All Authors button
        private void button1_Click(object sender, EventArgs e)
        {
            refreshAuthorList();
            refreshAllBooks();
        }

        //Edit Author button
        private void button2_Click(object sender, EventArgs e)
        {
            //string selected = listBox1.SelectedItem.ToString();
            //author au = pubsService.getAuthorByID(selected); 

            //Create author with field values
            author au = new author();
            au.au_fname = txtFirst.Text;
            au.au_lname = txtLast.Text;
            au.au_phone = phoneBox.Text;
            au.au_address = addrBox.Text;
            au.au_city = cityBox.Text;
            au.au_state = stateBox.Text;
            au.au_zip = zipBox.Text;
            au.au_id = txtID.Text;

            if (radioYes.Checked) {
                au.au_contract = true;
            } else {
                au.au_contract = false;
            }

            //Pass author to editAuthor
            pubsService.editAuthor(au);

            //Refresh to reflect changes
            refreshAuthorList();
        }

        private void lvAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvAuthors.SelectedItems.Count <= 0) return; // nothing selected 

            ListViewItem item = lvAuthors.SelectedItems[0];

            string id = item.Tag as string; // it is later 
            string last = item.SubItems[1].Text;
            string first = item.SubItems[2].Text;
            string pNum = item.SubItems[3].Text;
            string address = item.SubItems[4].Text;
            string city = item.SubItems[5].Text;
            string state = item.SubItems[6].Text;
            string zip = item.SubItems[7].Text;
            
            //Radio buttons
            if(item.SubItems[8].Text == "Yes") {
                radioYes.Checked = true;
            } else {
                radioNo.Checked = true;
            }

            //Text fields
            txtID.Text = id;
            txtLast.Text = last;
            txtFirst.Text = first;
            phoneBox.Text = pNum;
            addrBox.Text = address;
            cityBox.Text = city;
            stateBox.Text = state;
            zipBox.Text = zip;

            //populate books by other lv
            refreshBooksByAuthor(id);
        }

        //Delete Author button
        private void deleteAuthorButton_Click(object sender, EventArgs e)
        {
            if (lvAuthors.SelectedItems.Count <= 0)
                return;

            string selected = lvAuthors.SelectedItems[0].SubItems[0].Text;

            //Author to pass to delete function
            author au = pubsService.getAuthorByID(selected);

            pubsService.deleteAuthor(au);

            //Refresh to reflect changes
            refreshAuthorList();
        }

        //Add Author button
        private void addAuthorButton_Click(object sender, EventArgs e)
        {
            //Create author with field values
            author au = new author();
            au.au_fname = txtFirst.Text;
            au.au_lname = txtLast.Text;
            au.au_phone = phoneBox.Text;
            au.au_address = addrBox.Text;
            au.au_city = cityBox.Text;
            au.au_state = stateBox.Text;
            au.au_zip = zipBox.Text;
            au.au_id = txtID.Text;

            if (radioYes.Checked)
            {
                au.au_contract = true;
            }
            else
            {
                au.au_contract = false;
            }

            pubsService.addAuthor(au);

            //Refresh to reflect changes
            refreshAuthorList();
        }

        private void refreshAuthorList()
        {
            //Clear old items
            lvAuthors.Items.Clear();

            //View Models
            List<authorViewModel> authors = pubsService.getAllAuthors();

            foreach (authorViewModel au in authors)
            {
                //string output = au.ID + " " + au.LastName + ", " + au.FirstName; 
                //listBox1.Items.Add(output); 

                ListViewItem item = new ListViewItem(au.ID);
                item.SubItems.Add(au.LastName);
                item.SubItems.Add(au.FirstName);
                item.SubItems.Add(au.Phone);
                item.SubItems.Add(au.Address);
                item.SubItems.Add(au.City);
                item.SubItems.Add(au.State);
                item.SubItems.Add(au.Zip);
                item.SubItems.Add(au.Contract);

                item.Tag = au.ID; // remember for later  

                lvAuthors.Items.Add(item);
            }// end add authors

            foreach (ColumnHeader ch in lvAuthors.Columns)
            {
                ch.Width = -2; // fit for content 
            }
        }

        private void refreshBooksByAuthor(string id)
        {
            lvBooks.Items.Clear();

            //View Models
            List<bookViewModel> books = pubsService.getBooksByAuthorID(id);

            foreach (bookViewModel bk in books)
            {
                ListViewItem item = new ListViewItem(bk.Title);
                item.SubItems.Add(bk.Type);
                item.SubItems.Add(bk.PubID);
                item.SubItems.Add(bk.PubName);
                item.SubItems.Add(bk.Price);
                item.SubItems.Add(bk.PubDate);

                item.Tag = bk.Id; // remember for later  

                lvBooks.Items.Add(item);
            }// end add authors

            foreach (ColumnHeader ch in lvBooks.Columns)
            {
                ch.Width = -2; // fit for content 
            }
        }

        private void refreshAllBooks()
        {
            lvAllBooks.Items.Clear();

            //View Models
            List<bookViewModel> books = pubsService.getAllBooks();

            foreach (bookViewModel bk in books)
            {
                ListViewItem item = new ListViewItem(bk.Title);
                item.SubItems.Add(bk.Type);
                item.SubItems.Add(bk.PubID);
                item.SubItems.Add(bk.PubName);
                item.SubItems.Add(bk.Price);
                item.SubItems.Add(bk.PubDate);

                item.Tag = bk.Id; // remember for later  

                lvAllBooks.Items.Add(item);
            }// end add authors

            foreach (ColumnHeader ch in lvAllBooks.Columns)
            {
                ch.Width = -2; // fit for content 
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    class configFile
    {
        public static string getSetting(string key)
        {
            // adapted from: 
            // https://msdn.microsoft.com/en-us/library/system.configuration.configurationmanager.appsettings(v=vs.110).aspx
            // see app.config file for how to add the setting

            string setting = "";

            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                // if appSettings[key] != null, then appSettings[key], else exception
                setting = appSettings[key] ?? throw new Exception("Key not found");
            }
            catch (ConfigurationErrorsException)
            {
                throw new Exception("Key not found");
            }

            return setting;
        }
    }
}
