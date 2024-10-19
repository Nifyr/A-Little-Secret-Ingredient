using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ALittleSecretIngredient.Forms
{
    public partial class MessageForm : Form
    {
        #pragma warning disable IDE0052 // Remove unread private members
        private GlobalData GlobalData { get; }

        internal MessageForm(GlobalData globalData)
        {
            GlobalData = globalData;
            InitializeComponent();
            FormClosing += MainForm.CancelFormClosing;
        }
    }
}
