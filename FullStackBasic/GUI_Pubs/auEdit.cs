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

namespace GUI_Pubs
{
    public partial class auEdit : Form
    {
        private author _author; 
        public auEdit(author au)
        {
            InitializeComponent();

            _author = au; 
        }

        private void auEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
