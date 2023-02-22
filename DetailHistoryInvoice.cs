using Mega.classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mega
{
    public partial class DetailHistoryInvoice : Form
    {
        public string WBNumber = null;
        public API api = null;

        public DetailHistoryInvoice(string WBNumber, API api)
        {
            InitializeComponent();
            this.WBNumber = WBNumber;
            this.api = api;
            textBox1.Text += string.Format("{0}", WBNumber);
        }

        private void DetailHistoryInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                megaAPI.SP_Invoice_HistoryResult[] sP_Invoice_Histories = api.mekus.me_OneInvoiceHistory(api.login, WBNumber);
                for (int i = 0; i < sP_Invoice_Histories.Length; i++)
                    dataGridView1.Rows.Add(sP_Invoice_Histories[i].Event_Name, sP_Invoice_Histories[i].City_Name, sP_Invoice_Histories[i].EventDate + " " + sP_Invoice_Histories[i].EventTime,
                                            sP_Invoice_Histories[i].Comments, sP_Invoice_Histories[i].Agent_Name, sP_Invoice_Histories[i].DateCreate);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
