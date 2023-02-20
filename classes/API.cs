using System;
using System.Collections.Generic;
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
                    return mekus.Me_ListWayBills(login, DateTime.Now.AddDays(-89), DateTime.Now, null, 394, null, false, null);
                else return mekus.Me_ListWayBills(login, DateTime.Now.AddDays(-89), DateTime.Now, null, null, 394, false, null);
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
    }
}
