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

namespace FinalProject
{
    public partial class OrderForm : Form
    {
        List<bookViewModel> books;
        List<storeViewModel> stores;

        pubsService pubsService;

        public Form1 parentForm;

        public OrderForm()
        {
            InitializeComponent();

            string dbConnection = configFile.getSetting("pubsDBConnectionAzure");

            lvOrder.View = View.Details;
            lvOrder.GridLines = true;
            lvOrder.FullRowSelect = true;
            lvOrder.MultiSelect = false;

            lvBooks.View = View.Details;
            lvBooks.GridLines = true;
            lvBooks.FullRowSelect = true;
            lvBooks.MultiSelect = false;

            SalesRepoDB sRepoDB = new SalesRepoDB(dbConnection);
            storesRepoDB storesRepoDB = new storesRepoDB(dbConnection);
            BookRepoDB BkRepoDB = new BookRepoDB(dbConnection);
            orderRepoDB ordRepoDB = new orderRepoDB(dbConnection);

            pubsService = new pubsService(sRepoDB, storesRepoDB, ordRepoDB, BkRepoDB);

            lvOrder.Columns.Add("Title");
            lvOrder.Columns.Add("Quantity");

            lvBooks.Columns.Add("Title");
            lvBooks.Columns.Add("Publisher");
            lvBooks.Columns.Add("Retail");


            //Populate dropdown with store names and get available stores
            stores = pubsService.getAllStores();

            foreach (storeViewModel st in stores)
            {
                comboBox1.Items.Add(st.storeName);               
            }

            comboBox2.Items.Add("Net 60");
            comboBox2.Items.Add("Net 30");
            comboBox2.Items.Add("ON invoice");

            //Populate lv with books
            books = pubsService.getAllBooks();

            foreach (bookViewModel bk in books)
            {
                ListViewItem item = new ListViewItem(bk.Title);
                item.SubItems.Add(bk.PubName);
                item.SubItems.Add(bk.Price);

                item.Tag = bk.Id; // remember for later  

                lvBooks.Items.Add(item);
            }// end add authors

            foreach (ColumnHeader ch in lvBooks.Columns)
            {
                ch.Width = -2; // fit for content 
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Cancel button
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //Add book to order button
        private void button3_Click(object sender, EventArgs e)
        {

            ListViewItem match = null;

            //Go through each order item in list
            foreach (ListViewItem itm in lvOrder.Items)
            {
                //Check whether or overwrite quantity
                if (itm.SubItems[0].Text == lvBooks.SelectedItems[0].Text)
                {
                    //Match found
                    match = itm;
                }
            }

            //Selection was already in order, overwrite quantity
            if (match != null)
            {
                match.SubItems[1].Text = textBox1.Text;
            }
            else
            {
                //Make new item listing with quantity
                ListViewItem item = new ListViewItem(lvBooks.SelectedItems[0].Text);
                item.SubItems.Add(textBox1.Text);

                lvOrder.Items.Add(item);  
            }

            foreach (ColumnHeader ch in lvOrder.Columns)
            {
                ch.Width = -2; // fit for content 
            }
        }

        //Place order button
        private void button2_Click(object sender, EventArgs e)
        {
            //Order object to set up
            order ord = null;

            foreach (storeViewModel str in stores)
            {
                //Match selected store to get details
                if (comboBox1.Text == str.storeName)
                {
                    //Create new order object (pass in store id and pay terms)
                    ord = pubsService.CreateNewOrder(str.storeID, comboBox2.Text);
                }
            }

            //Fills up the orderItmes list in ord
            foreach (ListViewItem itm in lvOrder.Items)
            {
                //Get the quantity and book id numbers from this listViewItem
                string quant = itm.SubItems[1].Text;
                string id = "none";

                //Get titles ID from its matching name
                foreach (bookViewModel bk in books)
                {
                    if (itm.SubItems[0].Text == bk.Title)
                    {
                        id = bk.Id;
                    }
                }

                ord.Items = new List<orderItem>();

                //Add an orderItem(titleID, qty) to order objects list for each in listView
                ord.Items.Add(new orderItem(id, quant));
            }

            //Order object has all order info needed
            pubsService.submitOrder(ord);
            parentForm.refreshSalesList();
            this.Close();
        }
    }
}
