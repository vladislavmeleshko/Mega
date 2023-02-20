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
    public partial class Form1 : Form
    {
        API api = null;
        public Form1()
        {
            InitializeComponent();
            api = new API();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                megaAPI.SP_ListWayBillsResult[] sP_ListWayBillsResult = api.getAllInvoces(0);

                for (int i = 0; i < sP_ListWayBillsResult.Length; i++)
                    dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                            sP_ListWayBillsResult[i].ConsigneeCity_Name, api.getDateOfShipment(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode),
                                            api.getDateOfReceipt(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode),
                                            api.getOriginalDeliveryDate(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeCityCode),
                                            api.getLastEvent(sP_ListWayBillsResult[i].WBNumber), api.getDateLastEvent(sP_ListWayBillsResult[i].WBNumber));
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                megaAPI.SP_ListWayBillsResult[] sP_ListWayBillsResult = api.getAllInvoces(1);

                for (int i = 0; i < sP_ListWayBillsResult.Length; i++)
                    dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                            sP_ListWayBillsResult[i].ConsigneeCity_Name, api.getDateOfShipment(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode),
                                            api.getDateOfReceipt(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode),
                                            api.getOriginalDeliveryDate(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeCityCode),
                                            api.getLastEvent(sP_ListWayBillsResult[i].WBNumber), api.getDateLastEvent(sP_ListWayBillsResult[i].WBNumber));
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
