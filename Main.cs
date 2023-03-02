using Mega.classes;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Mega
{
    public partial class Main : Form
    {
        API api = null;
        public Main()
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
                                            api.getLastEvent(sP_ListWayBillsResult[i].WBNumber), api.getDateLastEvent(sP_ListWayBillsResult[i].WBNumber));
             }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string WBNumber = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                DetailHistoryInvoice form = new DetailHistoryInvoice(WBNumber, api);
                form.Show();
                Clipboard.SetText(WBNumber);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string[] list_invoices = richTextBox1.Text.Split();

                Workbook xlWB;
                Worksheet xlSht;

                Excel.Application xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Open(System.Windows.Forms.Application.StartupPath + @"\dd.xls");
                xlSht = (Worksheet)xlApp.Worksheets.get_Item(1);

                int j = 2;

                for(int i = 0; i < list_invoices.Length; i++)
                {
                    string info = api.get_info_for_delivery(list_invoices[i]);
                    if (info != null)
                    {
                        string[] info_two = info.Split();
                        xlSht.Cells[j, 1] = info_two[0];
                        xlSht.Cells[j, 2] = info_two[1];
                        xlSht.Cells[j, 3] = info_two[2];
                        xlSht.Cells[j, 4] = info_two[3];
                        j++;
                    }
                }
                MessageBox.Show("Функция получения данных о доставке выполнена!", "Получение данных о доставке", MessageBoxButtons.OK, MessageBoxIcon.Information);

                xlApp.Application.ActiveWorkbook.Save();
                xlApp.Application.ActiveWorkbook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string[] invoices = richTextBox3.Text.Split();
                int manifest = Convert.ToInt32(textBox1.Text.Replace('M', ' ').Replace('М', ' '));
                int j = 1;
                for (int i = 0; i < invoices.Length; i++)
                {
                    string response = api.addInvoiceToManifest(invoices[i], manifest);
                    if (response != null)
                    {
                        richTextBox2.Text += j + ")\t" + response + "\n\n";
                        j++;
                    }
                }
                richTextBox2.Text += "M" + manifest + ": накладные были добавлены в манифест!\n\n";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
