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
    public partial class Form1 : Form
    {
        pubsService pubsService;

        public Form1()
        {
            InitializeComponent();

            lvSales.View = View.Details;
            lvSales.GridLines = true;
            lvSales.FullRowSelect = true;
            lvSales.MultiSelect = false;

            string dbConnection = configFile.getSetting("pubsDBConnectionAzure");

            SalesRepoDB sRepoDB = new SalesRepoDB(dbConnection);
            storesRepoDB storesRepoDB = new storesRepoDB(dbConnection);
            BookRepoDB BkRepoDB = new BookRepoDB(dbConnection);
            orderRepoDB ordRepoDB = new orderRepoDB(dbConnection);

            pubsService = new pubsService(sRepoDB, storesRepoDB, ordRepoDB, BkRepoDB);

            //init columns
            // using statement: object is correctly disposed of
            using (SqlConnection connection = new SqlConnection(dbConnection))//StreamReader reader = File.OpenText(_connectionString)
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM sales", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            lvSales.Columns.Add(reader.GetName(i));
                        }
                    }// end using reader 
                }// end using command  
            }

            refreshSalesList();
        }

        //New order button
        private void button1_Click(object sender, EventArgs e)
        {
            OrderForm f = new OrderForm();
            f.parentForm = this;
            f.ShowDialog();
            
        }

        //Details button
        private void button2_Click(object sender, EventArgs e)
        {
            DetailsForm f = new DetailsForm(lvSales.SelectedItems[0].Text, lvSales.SelectedItems[0].SubItems[1].Text);
            f.ShowDialog();
        }

        public void refreshSalesList()
        {
            //Populate lv with sales
            List<saleViewModel> sales = getSalesNoDuplicates();

            foreach (saleViewModel sl in sales)
            {
                ListViewItem item = new ListViewItem(sl.storeID);
                item.SubItems.Add(sl.orderNum);
                item.SubItems.Add(sl.orderDate);
                item.SubItems.Add(sl.quantity);
                item.SubItems.Add(sl.payTerms);
                item.SubItems.Add(sl.titleID);

                item.Tag = sl.storeID; // remember for later  

                lvSales.Items.Add(item);
            }// end add authors

            foreach (ColumnHeader ch in lvSales.Columns)
            {
                ch.Width = -2; // fit for content 
            }
        }

        //Remove duplicates from list of sales
        public List<saleViewModel> getSalesNoDuplicates()
        {
            List<saleViewModel> sales = pubsService.getAllSales();

            for(int i = 0; i < sales.Count(); i++)
            {
                for(int j = 0; j < sales.Count(); j++)
                {
                    if (sales[j].titleID != sales[i].titleID && sales[j].orderNum == sales[i].orderNum )
                    {
                        sales.Remove(sales[j]);
                    }            
                }               
            }

            return sales;
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
