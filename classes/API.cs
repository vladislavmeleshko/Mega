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
        public List<Agent> agents = new List<Agent>();

        public string dateofshipment { get; set; }
        public string dateofreceipt { get; set; }
        public string dateofdaydeveliery { get; set; }
        public string namehistory { get; set; }
        public string datetimehistory { get; set; }
        public string comment { get; set; }
        public string adres { get; set; }

        public void clear_data()
        {
            dateofshipment = "";
            dateofreceipt = "";
            dateofdaydeveliery = "";
            namehistory = "";
            datetimehistory = "";
            comment = "";
            adres = "";
        }

        public API()
        {
            try
            {
                login = mekus.me_Login("webminsk", "XvR2qTM970a04d43", out mlp);
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

        public void get_test(string WBNumber, int AgentCode, int AgentCode2, int CityCode)
        {
            SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
            SP_Invoice_GenerResult[] sP_Invoice_GenerResults = mekus.me_oneInvoice(login, WBNumber);
            for (int i = 0; i < sP_Invoice_HistoryResult.Length; i++)
            {
                if (sP_Invoice_HistoryResult[i].EventNum == 23 && sP_Invoice_HistoryResult[i].AgentCode == AgentCode)
                {
                    dateofshipment = sP_Invoice_HistoryResult[i].EventDate + " " + sP_Invoice_HistoryResult[i].EventTime;
                    for(int j = i; j < sP_Invoice_HistoryResult.Length; j++)
                    {
                        if (sP_Invoice_HistoryResult[j].EventNum == 47 && sP_Invoice_HistoryResult[j].AgentCode == AgentCode2)
                        {
                            dateofreceipt = sP_Invoice_HistoryResult[j].EventDate + " " + sP_Invoice_HistoryResult[j].EventTime;
                            dateofdaydeveliery = get_date_delivery(Convert.ToDateTime(dateofreceipt), AgentCode2, CityCode);
                            adres = sP_Invoice_GenerResults[0].ConsigneeAdres;
                            namehistory = sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].Event_Name;
                            datetimehistory = sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].EventDate + " "
                                                + sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].EventTime;
                            comment = sP_Invoice_HistoryResult[sP_Invoice_HistoryResult.Length - 1].Comments;
                        }
                    }
                }        
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
                if(time != "")
                    sP_InvoiceHistAddResults = mekus.me_InvoiceHistoryAdd(login, WBNumber, 1367, (byte)id_history, DateTime.Now.Date, time, comment);
                else sP_InvoiceHistAddResults = mekus.me_InvoiceHistoryAdd(login, WBNumber, 1367, (byte)id_history, DateTime.Now.Date, DateTime.Now.ToString("hh:mm"), comment);
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
            SP_ListAgentsResult[] sP_ListAgentsResults = mekus.me_ListAgents(login);
            try
            {
                if (date != DateTime.MinValue)
                {
                    Agent agent = agents.Find(x => x.id_city == ConsigneeCityCode);
                    if(agent != null)
                    {
                        if (date.DayOfWeek == DayOfWeek.Saturday || date.AddDays((double)agent.delivery_period - 1).DayOfWeek == DayOfWeek.Saturday)
                        {
                            date = date.AddDays(2 + (double)agent.delivery_period - 1);
                            return date.ToString("dd.MM.yyyy");
                        }
                        else if (date.DayOfWeek == DayOfWeek.Sunday || date.AddDays((double)agent.delivery_period - 1).DayOfWeek == DayOfWeek.Sunday)
                        {
                            date = date.AddDays(1 + (double)agent.delivery_period - 1);
                            return date.ToString("dd.MM.yyyy");
                        }
                        else
                        {
                            date = date.AddDays((double)agent.delivery_period);
                            return date.ToString("dd.MM.yyyy");
                        }
                    }
                    for (int i = 0; i < sP_ListAgentsResults.Length; i++)
                    { 
                        if (sP_ListAgentsResults[i].AgentCode == ConsigneeAgentConde && sP_ListAgentsResults[i].AgentCityCode == ConsigneeCityCode)
                        {
                            agents.Add(new Agent(ConsigneeCityCode, 0));
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
                    }
                    sP_Agent_ZoneResults = mekus.me_AgentZone(login, (short)ConsigneeAgentConde, DateTime.Today);
                    for (int i = 0; i < sP_Agent_ZoneResults.Length; i++)
                    {
                        if (sP_Agent_ZoneResults[i].CityCode == ConsigneeCityCode)
                        {
                            agents.Add(new Agent(ConsigneeCityCode, (int)sP_Agent_ZoneResults[i].DeliveryTime));
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
                }
                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

        public int updateInvoice(string number)
        {
            SP_Invoice_GenerResult [] sP_Invoice_GenerResult = mekus.me_oneInvoice(login, number);
            if (sP_Invoice_GenerResult[0].WBCloseDate != null)
                return 1;
            else return 0;
        }

        public Report get_invoices_in_manifest(string manifest)
        {
            WBInManifest[] wBInManifests = null;
            Report report = new Report();
            try
            {
                wBInManifests = mekus.me_allInvocesManifest_new(login, Convert.ToInt32(manifest.Replace("М", "")));
                report.Manifest = new List<ReportManifest>
                {
                    new ReportManifest()
                };
                report.Manifest[0].Invoice = new List<ReportManifestInvoice>();

                for (int i = 0; i < wBInManifests.Length; i++)
                {
                    ReportManifestInvoice invoice = new ReportManifestInvoice();
                    invoice.WBNumber = Convert.ToUInt32(wBInManifests[i].WBNumber);
                    invoice.Date = Convert.ToString(wBInManifests[i].WBOpenDate);
                    invoice.ShipperCountry = wBInManifests[i].ShipperCountry;
                    invoice.ShipperCity = wBInManifests[i].ShipperCity_Name;
                    invoice.ShipperAgent = wBInManifests[i].ShipperAgent;
                    invoice.ShipperCompany = wBInManifests[i].ShipperCompany;
                    invoice.ShipperFIO = wBInManifests[i].ShipperLastName;
                    invoice.ShipperPhone = wBInManifests[i].ShipperPhone;
                    invoice.ShipperAddress = wBInManifests[i].ShipperAdres;
                    invoice.ConsigneeCountry = wBInManifests[i].ConsigneeCountry;
                    invoice.ConsigneeCity = wBInManifests[i].ConsigneeCity_Name;
                    invoice.ConsigneeAgent = wBInManifests[i].ConsigneeAgent;
                    invoice.ConsigneeCompany = wBInManifests[i].ConsigneeCompany;
                    invoice.ConsigneeFIO = wBInManifests[i].ConsigneeLastName;
                    invoice.ConsigneePhone = wBInManifests[i].ConsigneePhone;
                    invoice.ConsigneeAddress = wBInManifests[i].ConsigneeAdres;
                    invoice.WBWeight = Convert.ToDecimal(wBInManifests[i].WBWeight);
                    invoice.Places = Convert.ToInt32(wBInManifests[i].Places.Length);
                    invoice.Status = wBInManifests[i].NaklStatus;
                    invoice.WBOldNumber = wBInManifests[i].WBOldNumber;
                    invoice.FedexNumber = wBInManifests[i].FedexNum;
                    invoice.VolumeWeight = Convert.ToDecimal(wBInManifests[i].WBVolumeWeight);
                    invoice.WBDescription = wBInManifests[i].WBDescription;
                    invoice.FreightRUR = Convert.ToString(wBInManifests[i].Freight_RUR).Replace(".", ",");
                    if (wBInManifests[i].WhoWillPay == 1)
                        invoice.WhoWillPay = "Отправитель";
                    else if (wBInManifests[i].WhoWillPay == 2)
                        invoice.WhoWillPay = "Получатель";
                    else if (wBInManifests[i].WhoWillPay == 3)
                        invoice.WhoWillPay = "3-я сторона";
                    if (wBInManifests[i].PaymentType == 1 && wBInManifests[i].WhoWillPay != 3)
                        invoice.PaymentType = "Нал";
                    else if (wBInManifests[i].PaymentType == 2 && wBInManifests[i].WhoWillPay != 3)
                        invoice.PaymentType = "Б/нал";
                    report.Manifest[0].Invoice.Add(invoice);
                }
                return report;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public Report get_invoices_out_manifest(string[] number, int i)
        {
            SP_Invoice_GenerResult[] sP_Invoice_GenerResults = null;
            Report report = new Report();
            int j = i;
            try
            {
                report.Manifest = new List<ReportManifest>
                {
                    new ReportManifest()
                };
                report.Manifest[0].Invoice = new List<ReportManifestInvoice>();
                for (j = i; j < number.Length; j++)
                {
                    sP_Invoice_GenerResults = mekus.me_oneInvoice(login, number[j]);
                    if (sP_Invoice_GenerResults[0].WBNumber != null)
                    {
                        ReportManifestInvoice invoice = new ReportManifestInvoice();
                        invoice.WBNumber = Convert.ToUInt32(sP_Invoice_GenerResults[0].WBNumber);
                        invoice.Date = Convert.ToString(sP_Invoice_GenerResults[0].WBOpenDate);
                        invoice.ShipperCity = sP_Invoice_GenerResults[0].ShipperCity_Name;
                        invoice.ShipperAgent = sP_Invoice_GenerResults[0].ShipperAgent_Name;
                        invoice.ShipperCompany = sP_Invoice_GenerResults[0].ShipperCompany;
                        invoice.ShipperFIO = sP_Invoice_GenerResults[0].ShipperLastName;
                        invoice.ShipperPhone = sP_Invoice_GenerResults[0].ShipperPhone;
                        invoice.ShipperAddress = sP_Invoice_GenerResults[0].ShipperAdres;
                        invoice.ConsigneeCity = sP_Invoice_GenerResults[0].ConsigneeCity_Name;
                        invoice.ConsigneeAgent = sP_Invoice_GenerResults[0].ConsigneeAgent_Name;
                        invoice.ConsigneeCompany = sP_Invoice_GenerResults[0].ConsigneeCompany;
                        invoice.ConsigneeFIO = sP_Invoice_GenerResults[0].ConsigneeLastName;
                        invoice.ConsigneePhone = sP_Invoice_GenerResults[0].ConsigneePhone;
                        invoice.ConsigneeAddress = sP_Invoice_GenerResults[0].ConsigneeAdres;
                        if(sP_Invoice_GenerResults[0].WBWeight != null)
                            invoice.WBWeight = Convert.ToDecimal(sP_Invoice_GenerResults[0].WBWeight.Replace(".", ","));
                        invoice.Places = Convert.ToInt32(sP_Invoice_GenerResults[0].WBPackage);
                        invoice.Status = sP_Invoice_GenerResults[0].NaklStatus;
                        invoice.WBOldNumber = sP_Invoice_GenerResults[0].WBOldNumber;
                        invoice.FedexNumber = sP_Invoice_GenerResults[0].FedexNum;
                        invoice.VolumeWeight = Convert.ToDecimal(sP_Invoice_GenerResults[0].WBVolumeWeight);
                        invoice.WBDescription = sP_Invoice_GenerResults[0].WBDescription;
                        invoice.FreightRUR = Convert.ToString(sP_Invoice_GenerResults[0].Freight_RUR).Replace(".", ",");
                        if (sP_Invoice_GenerResults[0].WhoWillPay == 1)
                            invoice.WhoWillPay = "Отправитель";
                        else if (sP_Invoice_GenerResults[0].WhoWillPay == 2)
                            invoice.WhoWillPay = "Получатель";
                        else if (sP_Invoice_GenerResults[0].WhoWillPay == 3)
                            invoice.WhoWillPay = "3-я сторона";
                        if (sP_Invoice_GenerResults[0].PaymentType == 1)
                            invoice.PaymentType = "Нал";
                        else if (sP_Invoice_GenerResults[0].PaymentType == 2)
                            invoice.PaymentType = "Б/нал";
                        report.Manifest[0].Invoice.Add(invoice);
                    }
                }
                return report;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return get_invoices_out_manifest(number, j + 1);
            }
        }

        public SP_ListManifestsResult[] get_manifest_for_date(DateTime date)
        {
            try
            {
                return mekus.me_ListManifests(login, date);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        } 
    }
}