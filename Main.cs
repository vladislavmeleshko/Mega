using Mega.classes;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Xml.Serialization;
using System.IO;
using Mega.megaAPI;

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
            button6.Enabled = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    api.auth();
                    this.Invoke(new System.Action(() =>
                        dataGridView1.Rows.Clear()
                    ));
                    dataGridView1.Rows.Clear();
                    SP_ListWayBillsResult[] sP_ListWayBillsResult = api.getAllInvoces(0);

                    for (int i = 0; i < sP_ListWayBillsResult.Length; i++)
                    {
                        API newAPI = new API();

                        newAPI.get_test(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeCityCode);

                        if (InvokeRequired)
                        {
                            if (newAPI.dateofreceipt != null)
                            {
                                this.Invoke(new System.Action(() =>
                                dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                                        sP_ListWayBillsResult[i].ConsigneeCity_Name, newAPI.dateofshipment, newAPI.dateofreceipt,
                                                        newAPI.dateofdaydeveliery, newAPI.namehistory, newAPI.comment, newAPI.datetimehistory, newAPI.adres)
                                ));
                            }
                            else
                                this.Invoke(new System.Action(() =>
                                dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                                sP_ListWayBillsResult[i].ConsigneeCity_Name, newAPI.dateofshipment,
                                                "", "", newAPI.namehistory, newAPI.comment, newAPI.datetimehistory, newAPI.adres)
                                ));
                            this.Invoke(new System.Action(() =>
                                label1.Text = string.Format("Обработано накладных {0} из {1}", (i + 1), sP_ListWayBillsResult.Length)
                            ));
                            this.Invoke(new System.Action(() =>
                                label1.Refresh()
                            ));
                            this.Invoke(new System.Action(() =>
                                dataGridView1.Refresh()
                            ));
                        }
                    }

                    MessageBox.Show("Проверка завершена!", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    api.auth();
                    this.Invoke(new System.Action(() =>
                        dataGridView1.Rows.Clear()
                    ));
                    SP_ListWayBillsResult[] sP_ListWayBillsResult = api.getAllInvoces(1);

                    API newAPI = new API();

                    for (int i = 0; i < sP_ListWayBillsResult.Length; i++)
                    {
                        newAPI.clear_data();
                        newAPI.get_test(sP_ListWayBillsResult[i].WBNumber, (int)sP_ListWayBillsResult[i].ShipperAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeAgentCode, (int)sP_ListWayBillsResult[i].ConsigneeCityCode);
                        if (InvokeRequired)
                        {
                            if (newAPI.dateofreceipt != null)
                            {
                                this.Invoke(new System.Action(() =>
                                    dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                                        sP_ListWayBillsResult[i].ConsigneeCity_Name, newAPI.dateofshipment, newAPI.dateofreceipt,
                                                        newAPI.dateofdaydeveliery, newAPI.namehistory, newAPI.comment, newAPI.datetimehistory, newAPI.adres)
                                ));
                            }
                            else
                                this.Invoke(new System.Action(() =>
                                    dataGridView1.Rows.Add(sP_ListWayBillsResult[i].WBNumber, sP_ListWayBillsResult[i].ShipperAgent_Name, sP_ListWayBillsResult[i].ConsigneeAgent_Name,
                                                sP_ListWayBillsResult[i].ConsigneeCity_Name, newAPI.dateofshipment,
                                                "", "", newAPI.namehistory, newAPI.comment, newAPI.datetimehistory, newAPI.adres)
                                ));
                            this.Invoke(new System.Action(() =>
                                label1.Text = string.Format("Обработано накладных {0} из {1}", (i + 1), sP_ListWayBillsResult.Length)
                            ));
                            this.Invoke(new System.Action(() =>
                                label1.Refresh()
                            ));
                            this.Invoke(new System.Action(() =>
                                dataGridView1.Refresh()
                            ));
                        }
                    }
                    MessageBox.Show("Проверка завершена!", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
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
                api.auth();

                string[] list_invoices = richTextBox1.Text.Split();

                Workbook xlWB;
                Worksheet xlSht;

                Excel.Application xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Open(System.Windows.Forms.Application.StartupPath + @"\dd.xls");
                xlSht = (Worksheet)xlApp.Worksheets.get_Item(1);

                int j = 2;

                for(int i = 0; i < list_invoices.Length; i++)
                {
                    if(api.get_info_for_delivery(list_invoices[i]) == 1)
                    {
                        xlSht.Cells[j, 1] = list_invoices[i];
                        xlSht.Cells[j, 2] = api.WBCloseDate;
                        xlSht.Cells[j, 3] = api.WBCloseTime;
                        xlSht.Cells[j, 4] = api.submiter;
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

        private async void button4_Click(object sender, EventArgs e)
        {
            try
            {
                api.auth();
                string[] invoices = richTextBox3.Text.Split();
                richTextBox3.Text = "";
                List<string> error_invoice = new List<string>();
                int manifest = Convert.ToInt32(textBox1.Text.Replace('M', ' ').Replace('М', ' '));
                int j = 1;
                int count = 0;

                await Task.Run(() =>
                {
                    for (int i = 0; i < invoices.Length; i++)
                    {
                        string response = api.addInvoiceToManifest(invoices[i], manifest);
                        if (response != null)
                        {
                            this.Invoke(new System.Action(() => richTextBox2.Text += j + ")\t" + response + "\n\n"));
                            j++;
                            error_invoice.Add(invoices[i]);
                            continue;
                        }
                        count++;
                    }
                    this.Invoke(new System.Action(() => richTextBox2.Text += "M" + manifest + ": накладные были добавлены в манифест! Количество добавленных накладных " + count + ". Количество накладных: " + invoices.Length + "\n\n"));
                    for (int i = 0; i < error_invoice.Count; i++)
                        this.Invoke(new System.Action(() => richTextBox3.Text += error_invoice[i] + "\n"));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string[] invoices = richTextBox5.Text.Split();
                int j = 1;
                string response = null;

                await Task.Run(() =>
                {
                    for (int i = 0; i < invoices.Length; i++)
                    {
                        for (int z = 0; z < sP_ListEventsResults.Length; z++)
                        {
                            if (comboBox1.Text == sP_ListEventsResults[z].EventName)
                            {
                                response = api.set_history_invoice(invoices[i], sP_ListEventsResults[z].EventNum, textBox2.Text, textBox3.Text);
                                if (response != null)
                                {
                                    this.Invoke(new System.Action(() => richTextBox4.Text += j + ")\t" + response + "\n\n"));
                                    j++;
                                }
                            }
                        }
                    }
                    this.Invoke(new System.Action(() => richTextBox4.Text += "Истории были добавлены в накладные!\n\n"));
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                api.auth();
                SP_ListManifestsResult[] sP_ListManifestsResults = api.get_manifest_for_date(dateTimePicker1.Value.Date);
                for (int i = 0; i < sP_ListManifestsResults.Length; i++)
                {
                    Report result = api.get_invoices_in_manifest(sP_ListManifestsResults[i].ManifestNum);
                    if (result != null)
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Report));
                        if (checkBox1.Checked == false || i > 0)
                        {
                            using (StreamWriter writer = new StreamWriter("Выгрузка МЭ.txt", true, Encoding.UTF8))
                            {
                                xmlSerializer.Serialize(writer, result);
                            }
                        }
                        else if (checkBox1.Checked == true && i == 0)
                        {
                            using (StreamWriter writer = new StreamWriter("Выгрузка МЭ.txt", false, Encoding.UTF8))
                            {
                                xmlSerializer.Serialize(writer, result);
                            }
                        }
                    }
                }
                MessageBox.Show("Накладные записаны в файл!", "Запись накладных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                api.auth();
                string[] number = richTextBox6.Text.Split();
                Report result = api.get_invoices_out_manifest(number, 0);
                if (result != null)
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(Report));
                    using (StreamWriter writer = new StreamWriter("Выгрузка МЭ.txt", false, Encoding.UTF8))
                    {
                        xmlSerializer.Serialize(writer, result);
                    }
                }
                MessageBox.Show("Накладные записаны в файл!", "Запись накладных", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button8_Click(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                try
                {
                    api.auth();
                    SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = null;
                    for (int i = dataGridView1.Rows.Count - 1; i >= 0; i--)
                    {
                        Invoke(new System.Action(() => label1.Text = "Осталось обработать накладных " + i));
                        Invoke(new System.Action(() => label1.Update()));
                        Invoke(new System.Action(() => dataGridView1.Update()));
                        int check = api.updateInvoice(dataGridView1.Rows[i].Cells[0].Value.ToString());
                        if (check == 1)
                            Invoke(new System.Action(() => dataGridView1.Rows.RemoveAt(i)));
                        else
                        {
                            if(dataGridView1.Rows[i].Cells[6].Value.ToString() != null)
                            {
                                sP_Invoice_HistoryResult = api.mekus.me_OneInvoiceHistory(api.login, dataGridView1.Rows[i].Cells[0].Value.ToString());
                                if (sP_Invoice_HistoryResult.Length > 0)
                                {
                                    Invoke(new System.Action(() => dataGridView1.Rows[i].Cells[7].Value = sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].Event_Name));
                                    Invoke(new System.Action(() => dataGridView1.Rows[i].Cells[8].Value = sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].Comments));
                                    Invoke(new System.Action(() => dataGridView1.Rows[i].Cells[9].Value = sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].EventDate +
                                                                    " " + sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].EventTime));
                                }
                            }
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.ShowDialog();
                openFileDialog1.Title = "Выберите файл для выгрузки в LIGA";
                
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(MEScheme));
                using (FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                {
                    MEScheme mEScheme = xmlSerializer.Deserialize(fs) as MEScheme;

                    if(mEScheme != null)
                    {
                        for(int i = 0; i < mEScheme.DeliveryUpdate.Length; i++)
                        {
                            if (api.inputdeliveryforliga(mEScheme.DeliveryUpdate[i]) == 0)
                            {
                                MessageBox.Show("Не удалось выгрузить накладную " + mEScheme.DeliveryUpdate[i].WBNumber);
                            }
                        }
                        MessageBox.Show("ДД были выгружены в LIGA!");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
