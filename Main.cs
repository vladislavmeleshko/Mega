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
        megaAPI.SP_ListEventsResult[] sP_ListEventsResults;
        public Main()
        {
            InitializeComponent();
            api = new API();
            sP_ListEventsResults = api.get_history();
            for (int i = 0; i < sP_ListEventsResults.Length; i++)
                comboBox1.Items.Add(sP_ListEventsResults[i].EventName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                megaAPI.SP_ListWayBillsResult[] sP_ListWayBillsResult = api.getAllInvoces(0);

                for (int i = 0; i < sP_ListWayBillsResult.Length; i++)
                {
                    DateTime date = new DateTime();
                    if (api.getDateOfReceipt(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode) != "")
                        date = Convert.ToDateTime(api.getDateOfReceipt(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode));
                    if (date.Date != DateTime.MinValue)
                        dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                            sP_ListWayBillsResult[i].ConsigneeCity_Name, api.getDateOfShipment(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode),
                                            date.ToString("dd.MM.yyyy HH:mm"),
                                            api.get_date_delivery(date, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeCityCode),
                                            api.getLastEvent(sP_ListWayBillsResult[i].WBNumber), api.getDateLastEvent(sP_ListWayBillsResult[i].WBNumber));
                    else
                        dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                            sP_ListWayBillsResult[i].ConsigneeCity_Name, api.getDateOfShipment(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode),
                                            "",
                                            "",
                                            api.getLastEvent(sP_ListWayBillsResult[i].WBNumber), api.getDateLastEvent(sP_ListWayBillsResult[i].WBNumber));
                }

                MessageBox.Show("Проверка завершена!", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                {
                    DateTime date = new DateTime();
                    if (api.getDateOfReceipt(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode) != "")
                        date = Convert.ToDateTime(api.getDateOfReceipt(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode));
                    if(date.Date != DateTime.MinValue)
                        dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                                sP_ListWayBillsResult[i].ConsigneeCity_Name, api.getDateOfShipment(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode),
                                                date.ToString("dd.MM.yyyy HH:mm"),
                                                api.get_date_delivery(date, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeCityCode),
                                                api.getLastEvent(sP_ListWayBillsResult[i].WBNumber), api.getDateLastEvent(sP_ListWayBillsResult[i].WBNumber));
                    else
                        dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                                sP_ListWayBillsResult[i].ConsigneeCity_Name, api.getDateOfShipment(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode),
                                                "",
                                                "",
                                                api.getLastEvent(sP_ListWayBillsResult[i].WBNumber), api.getDateLastEvent(sP_ListWayBillsResult[i].WBNumber));
                }

                MessageBox.Show("Проверка завершена!", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
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
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string[] invoices = richTextBox5.Text.Split();
                int j = 1;
                string response = null;

                for(int i = 0; i < invoices.Length; i++)
                {
                    for(int z = 0; z < sP_ListEventsResults.Length; z++)
                    {
                        if(comboBox1.Text == sP_ListEventsResults[z].EventName)
                        {
                            response = api.set_history_invoice(invoices[i], sP_ListEventsResults[z].EventNum, textBox2.Text, textBox3.Text);
                            if (response != null)
                            {
                                richTextBox4.Text += j + ")\t" + response + "\n\n";
                                j++;
                            }
                        }
                    }
                }
                richTextBox4.Text += "Истории были добавлены в накладные!\n\n";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
