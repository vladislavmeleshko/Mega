using Mega.megaAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mega.classes
{
    public class API
    {
        public МанифестыЗаказыНакладныеSoapClient mekus = new megaAPI.МанифестыЗаказыНакладныеSoapClient();
        public string mlp;
        public TicketHeader login = null;

        public API()
        {
            try
            {
                login = mekus.me_Login("webminsk", "XvR2qTM9", out mlp);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public SP_ListWayBillsResult[] getAllInvoces(int type_invoice)
        {
            try
            {   if(type_invoice == 0)
                    return mekus.Me_ListWayBills(login, DateTime.Now.AddDays(-90), DateTime.Now.AddDays(-1), null, 394, null, false, null);
                else return mekus.Me_ListWayBills(login, DateTime.Now.AddDays(-90), DateTime.Now.AddDays(-1), null, null, 394, false, null);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public string getDateOfShipment(string WBNumber, int AgentCode)
        {
            try
            {
                SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
                for(int i = 0; i < sP_Invoice_HistoryResult.Length; i++)
                {
                    if (sP_Invoice_HistoryResult[i].EventNum == 23 && sP_Invoice_HistoryResult[i].AgentCode == AgentCode)
                        return sP_Invoice_HistoryResult[i].EventDate + " " + sP_Invoice_HistoryResult[i].EventTime;
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public string getDateOfReceipt(string WBNumber, int AgentCode)
        {
            try
            {
                SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
                for (int i = 0; i < sP_Invoice_HistoryResult.Length; i++)
                {
                    if (sP_Invoice_HistoryResult[i].EventNum == 47 && sP_Invoice_HistoryResult[i].AgentCode == AgentCode)
                        return sP_Invoice_HistoryResult[i].EventDate + " " + sP_Invoice_HistoryResult[i].EventTime;
                }
                return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public string getLastEvent(string WBNumber)
        {
            try
            {
                SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
                if (sP_Invoice_HistoryResult.Length > 0)
                    return sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].Event_Name;
                else return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public string getDateLastEvent(string WBNumber)
        {
            try
            {
                SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
                if (sP_Invoice_HistoryResult.Length > 0)
                    return sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].EventDate + " " + sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].EventTime;
                else return "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }

        public string get_info_for_delivery(string invoice)
        {
            try
            {
                SP_Invoice_HistoryResult[] sP_Invoice_HistoryResults = mekus.me_OneInvoiceHistory(login, invoice);
                for (int i = 0; i < sP_Invoice_HistoryResults.Length; i++)
                    if (sP_Invoice_HistoryResults[i].EventNum == 24)
                        return invoice + " " + sP_Invoice_HistoryResults[i].EventDate + " " + sP_Invoice_HistoryResults[i].EventTime + " " + sP_Invoice_HistoryResults[i].Comments;
                return null;
            }
            catch(Exception)
            {
                return null;
            }
        }

        public string addInvoiceToManifest(string invoice, int manifest)
        {
            SP_Manifest_NaklAddResult[] sP_Manifest_NaklAddResults = null;
            try
            {
                sP_Manifest_NaklAddResults = mekus.me_Manifest_NaklAdd(login, manifest, invoice);
                if (sP_Manifest_NaklAddResults[0].ResCode != 0)
                    return invoice + "\t" + sP_Manifest_NaklAddResults[0].ResText + "\tM" + manifest;
                return null;
            }
            catch(Exception)
            {
                return invoice + "\t" + sP_Manifest_NaklAddResults[0].ResText + "\tM" + manifest;
            }
        }

        public SP_ListEventsResult[] get_history()
        {
            try
            {
                return mekus.me_ListEvents(login);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string set_history_invoice(string WBNumber, int id_history, string comment, string time)
        {
            SP_InvoiceHistAddResult[] sP_InvoiceHistAddResults = null;
            try
            {
                if(id_history == 32)
                    sP_InvoiceHistAddResults = mekus.me_InvoiceHistoryAdd(login, WBNumber, 1367, (byte)id_history, DateTime.Now.Date, time, comment);
                else sP_InvoiceHistAddResults = mekus.me_InvoiceHistoryAdd(login, WBNumber, 1367, (byte)id_history, DateTime.Now.Date, time, comment);
                if (sP_InvoiceHistAddResults[0].ResCode != 0)
                    return WBNumber + "\t" + sP_InvoiceHistAddResults[0].ResText;
                return null;
            }
            catch (Exception)
            {
                return WBNumber + "\t" + sP_InvoiceHistAddResults[0].ResText;
            }
        }

        public string get_date_delivery(DateTime date, int ConsigneeAgentConde, int ConsigneeCityCode)
        {
            SP_Agent_ZoneResult[] sP_Agent_ZoneResults = null;
            try
            {
                if(date != DateTime.MinValue)
                {
                    sP_Agent_ZoneResults = mekus.me_AgentZone(login, (short)ConsigneeAgentConde, DateTime.Today);
                    for (int i = 0; i < sP_Agent_ZoneResults.Length; i++)
                    {
                        if (sP_Agent_ZoneResults[i].CityCode == ConsigneeCityCode)
                        {
                            if (date.DayOfWeek == DayOfWeek.Saturday || date.AddDays((double)sP_Agent_ZoneResults[i].DeliveryTime - 1).DayOfWeek == DayOfWeek.Saturday)
                            {
                                date = date.AddDays(2 + (double)sP_Agent_ZoneResults[i].DeliveryTime - 1);
                                return date.ToString("dd.MM.yyyy");
                            }
                            else if (date.DayOfWeek == DayOfWeek.Sunday || date.AddDays((double)sP_Agent_ZoneResults[i].DeliveryTime - 1).DayOfWeek == DayOfWeek.Sunday)
                            {
                                date = date.AddDays(1 + (double)sP_Agent_ZoneResults[i].DeliveryTime - 1);
                                return date.ToString("dd.MM.yyyy");
                            }
                            else
                            {
                                date = date.AddDays((double)sP_Agent_ZoneResults[i].DeliveryTime - 1);
                                return date.ToString("dd.MM.yyyy");
                            }
                        }
                    }
                    if (date.DayOfWeek == DayOfWeek.Saturday)
                    {
                        date = date.AddDays(2);
                        return date.ToString("dd.MM.yyyy");
                    }
                    else if (date.DayOfWeek == DayOfWeek.Sunday)
                    {
                        date = date.AddDays(1);
                        return date.ToString("dd.MM.yyyy");
                    }
                    return date.ToString("dd.MM.yyyy");
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
