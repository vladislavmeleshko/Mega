using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mega.classes
{
    public class API
    {
        public static megaAPI.МанифестыЗаказыНакладныеSoapClient mekus = new megaAPI.МанифестыЗаказыНакладныеSoapClient();
        public static string mlp;
        public megaAPI.TicketHeader login = null;

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

        public megaAPI.SP_ListWayBillsResult[] getAllInvoces(int type_invoice)
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
                megaAPI.SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
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
                megaAPI.SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
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
                megaAPI.SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
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
                megaAPI.SP_Invoice_HistoryResult[] sP_Invoice_HistoryResult = mekus.me_OneInvoiceHistory(login, WBNumber);
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

        public string getOriginalDeliveryDate(string WBNumber, int AgentCodeShipper, int ConsigneeAgentCode, int ConsigneeCityCode)
        {
            try
            {
                int? period = 0;
                megaAPI.SP_Invoice_GenerResult[] sP_Invoice = mekus.me_oneInvoice(login, WBNumber);
                megaAPI.SP_ListCitiesResult[] sP_ListCities = mekus.me_allCitys(login);
                megaAPI.SP_Tariffs_DirectZoneResult[] sP_Tariffs_DirectZones = mekus.me_CentralCitiesTariffs(login, (short)AgentCodeShipper, null);
                megaAPI.SP_Agent_ZoneResult[] sP_Agent_Zones = null;

                if (getDateOfShipment(WBNumber, AgentCodeShipper) == "")
                    return "";

                DateTime now = Convert.ToDateTime(getDateOfShipment(WBNumber, AgentCodeShipper));
                for (int i = 0; i < sP_ListCities.Length; i++)
                {
                    if (sP_ListCities[i].CityCode == ConsigneeCityCode && sP_ListCities[i].IsCentral == true)
                    {
                        for(int j = 0; j < sP_Tariffs_DirectZones.Length; j++)
                        {
                            if (sP_Tariffs_DirectZones[j].DestCityCode == sP_ListCities[i].CityCode)
                            {
                                period = sP_Tariffs_DirectZones[j].DeliveryPeriod;
                                while (true)
                                {
                                    if (period == 0)
                                        break;
                                    if(now.AddDays(1).DayOfWeek == DayOfWeek.Saturday)
                                    {
                                        now = now.AddDays(1);
                                        continue;
                                    }
                                    else if (now.AddDays(1).DayOfWeek == DayOfWeek.Sunday)
                                    {
                                        now = now.AddDays(1);
                                        continue;
                                    }
                                    now = now.AddDays(1);
                                    period--;
                                }
                                return now.ToString("dd.MM.yyyy");
                            }
                        }
                    }
                    else if(sP_ListCities[i].CityCode == ConsigneeCityCode && sP_ListCities[i].IsCentral == false)
                    {
                        for(int j = 0; j < sP_ListCities.Length; j++)
                        {
                            if (sP_ListCities[j].AgentCode == ConsigneeAgentCode && sP_ListCities[j].IsCentral == true)
                            {
                                for(int z = 0; z < sP_Tariffs_DirectZones.Length; z++)
                                {
                                    if (sP_Tariffs_DirectZones[z].DestCityCode == sP_ListCities[j].CityCode)
                                    {
                                        period = sP_Tariffs_DirectZones[z].DeliveryPeriod;
                                        sP_Agent_Zones = mekus.me_AgentZone(login, (short)ConsigneeAgentCode, null);
                                        for(int f = 0; f < sP_Agent_Zones.Length; f++)
                                        {
                                            if (sP_Agent_Zones[f].CityCode == ConsigneeCityCode)
                                            {
                                                period += sP_Agent_Zones[f].DeliveryTime;
                                                while (true)
                                                {
                                                    if (period == 0)
                                                        break;
                                                    if (now.AddDays(1).DayOfWeek == DayOfWeek.Saturday)
                                                    {
                                                        now = now.AddDays(1);
                                                        continue;
                                                    }
                                                    else if (now.AddDays(1).DayOfWeek == DayOfWeek.Sunday)
                                                    {
                                                        now = now.AddDays(1);
                                                        continue;
                                                    }
                                                    now = now.AddDays(1);
                                                    period--;
                                                }
                                                return now.ToString("dd.MM.yyyy");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return ""; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "";
            }
        }
    }
}
