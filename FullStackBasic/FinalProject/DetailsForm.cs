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
    public partial class DetailsForm : Form
    {
        pubsService pubsService;
        store st;

        public DetailsForm(string saleId, string orderNum)
        {
            InitializeComponent();
            string dbConnection = configFile.getSetting("pubsDBConnectionAzure");

            SalesRepoDB sRepoDB = new SalesRepoDB(dbConnection);
            storesRepoDB storesRepoDB = new storesRepoDB(dbConnection);
            BookRepoDB BkRepoDB = new BookRepoDB(dbConnection);
            orderRepoDB ordRepoDB = new orderRepoDB(dbConnection);

            pubsService = new pubsService(sRepoDB, storesRepoDB, ordRepoDB, BkRepoDB);

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.MultiSelect = false;

            listView1.Columns.Add("Title");
            listView1.Columns.Add("Quantity");

            st = pubsService.findStoreByID(saleId);
            label4.Text = st.stor_name;

            GetOrderItems(orderNum);

            foreach (ColumnHeader ch in listView1.Columns)
            {
                ch.Width = -2; // fit for content 
            }
        }

        public List<orderItem> GetOrderItems(string ordNum)
        {
            List<saleViewModel> allSales = pubsService.getAllSales();
            List<orderItem> items = new List<orderItem>();          

            foreach (saleViewModel s in allSales)
            {
                if (s.orderNum == ordNum)
                {
                    book bk = pubsService.findBookByID(s.titleID);

                    ListViewItem item = new ListViewItem(bk.Title);
                    item.SubItems.Add(s.quantity);

                    listView1.Items.Add(item);
                }
            }

            return items;
        }
    }
}
