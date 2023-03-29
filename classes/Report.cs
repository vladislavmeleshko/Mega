using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.classes
{

    // Примечание. Для запуска созданного кода может потребоваться NET Framework версии 4.5 или более поздней версии и .NET Core или Standard версии 2.0 или более поздней.
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public class Report
    {

        private string titleField;

        private List<ReportManifest> manifestField;

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Manifest")]
        public List<ReportManifest> Manifest
        {
            get
            {
                return this.manifestField;
            }
            set
            {
                this.manifestField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ReportManifest
    {

        private string manifestNumberField;

        private List<ReportManifestInvoice> invoiceField;

        /// <remarks/>
        public string ManifestNumber
        {
            get
            {
                return this.manifestNumberField;
            }
            set
            {
                this.manifestNumberField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Invoice")]
        public List<ReportManifestInvoice> Invoice
        {
            get
            {
                return this.invoiceField;
            }
            set
            {
                this.invoiceField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ReportManifestInvoice
    {

        private uint wBNumberField;

        private string dateField;

        private string shipperCountryField;

        private string shipperCityField;

        private string shipperAgentField;

        private string shipperCompanyField;

        private string shipperFIOField;

        private string shipperPhoneField;

        private string shipperAddressField;

        private string consigneeCountryField;

        private string consigneeCityField;

        private string consigneeAgentField;

        private string consigneeCompanyField;

        private string consigneeFIOField;

        private string consigneePhoneField;

        private string consigneeAddressField;

        private decimal wBWeightField;

        private int placesField;

        private string whoWillPayField;

        private string statusField;

        private object wBOldNumberField;

        private object fedexNumberField;

        private decimal volumeWeightField;

        private decimal assessedCostField;

        private uint payerNumberField;

        private string paymentTypeField;

        private object specialDeliveryField;

        private string freightRURField;

        private string insuranceRURField;

        private string totalRURField;

        private string wBDescriptionField;

        /// <remarks/>
        public uint WBNumber
        {
            get
            {
                return this.wBNumberField;
            }
            set
            {
                this.wBNumberField = value;
            }
        }

        /// <remarks/>
        public string Date
        {
            get
            {
                return this.dateField;
            }
            set
            {
                this.dateField = value;
            }
        }

        /// <remarks/>
        public string ShipperCountry
        {
            get
            {
                return this.shipperCountryField;
            }
            set
            {
                this.shipperCountryField = value;
            }
        }

        /// <remarks/>
        public string ShipperCity
        {
            get
            {
                return this.shipperCityField;
            }
            set
            {
                this.shipperCityField = value;
            }
        }

        /// <remarks/>
        public string ShipperAgent
        {
            get
            {
                return this.shipperAgentField;
            }
            set
            {
                this.shipperAgentField = value;
            }
        }

        /// <remarks/>
        public string ShipperCompany
        {
            get
            {
                return this.shipperCompanyField;
            }
            set
            {
                this.shipperCompanyField = value;
            }
        }

        /// <remarks/>
        public string ShipperFIO
        {
            get
            {
                return this.shipperFIOField;
            }
            set
            {
                this.shipperFIOField = value;
            }
        }

        /// <remarks/>
        public string ShipperPhone
        {
            get
            {
                return this.shipperPhoneField;
            }
            set
            {
                this.shipperPhoneField = value;
            }
        }

        /// <remarks/>
        public string ShipperAddress
        {
            get
            {
                return this.shipperAddressField;
            }
            set
            {
                this.shipperAddressField = value;
            }
        }

        /// <remarks/>
        public string ConsigneeCountry
        {
            get
            {
                return this.consigneeCountryField;
            }
            set
            {
                this.consigneeCountryField = value;
            }
        }

        /// <remarks/>
        public string ConsigneeCity
        {
            get
            {
                return this.consigneeCityField;
            }
            set
            {
                this.consigneeCityField = value;
            }
        }

        /// <remarks/>
        public string ConsigneeAgent
        {
            get
            {
                return this.consigneeAgentField;
            }
            set
            {
                this.consigneeAgentField = value;
            }
        }

        /// <remarks/>
        public string ConsigneeCompany
        {
            get
            {
                return this.consigneeCompanyField;
            }
            set
            {
                this.consigneeCompanyField = value;
            }
        }

        /// <remarks/>
        public string ConsigneeFIO
        {
            get
            {
                return this.consigneeFIOField;
            }
            set
            {
                this.consigneeFIOField = value;
            }
        }

        /// <remarks/>
        public string ConsigneePhone
        {
            get
            {
                return this.consigneePhoneField;
            }
            set
            {
                this.consigneePhoneField = value;
            }
        }

        /// <remarks/>
        public string ConsigneeAddress
        {
            get
            {
                return this.consigneeAddressField;
            }
            set
            {
                this.consigneeAddressField = value;
            }
        }

        /// <remarks/>
        public decimal WBWeight
        {
            get
            {
                return this.wBWeightField;
            }
            set
            {
                this.wBWeightField = value;
            }
        }

        /// <remarks/>
        public int Places
        {
            get
            {
                return this.placesField;
            }
            set
            {
                this.placesField = value;
            }
        }

        /// <remarks/>
        public string WhoWillPay
        {
            get
            {
                return this.whoWillPayField;
            }
            set
            {
                this.whoWillPayField = value;
            }
        }

        /// <remarks/>
        public string Status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public object WBOldNumber
        {
            get
            {
                return this.wBOldNumberField;
            }
            set
            {
                this.wBOldNumberField = value;
            }
        }

        /// <remarks/>
        public object FedexNumber
        {
            get
            {
                return this.fedexNumberField;
            }
            set
            {
                this.fedexNumberField = value;
            }
        }

        /// <remarks/>
        public decimal VolumeWeight
        {
            get
            {
                return this.volumeWeightField;
            }
            set
            {
                this.volumeWeightField = value;
            }
        }

        /// <remarks/>
        public decimal AssessedCost
        {
            get
            {
                return this.assessedCostField;
            }
            set
            {
                this.assessedCostField = value;
            }
        }

        /// <remarks/>
        public uint PayerNumber
        {
            get
            {
                return this.payerNumberField;
            }
            set
            {
                this.payerNumberField = value;
            }
        }

        /// <remarks/>
        public string PaymentType
        {
            get
            {
                return this.paymentTypeField;
            }
            set
            {
                this.paymentTypeField = value;
            }
        }

        /// <remarks/>
        public object SpecialDelivery
        {
            get
            {
                return this.specialDeliveryField;
            }
            set
            {
                this.specialDeliveryField = value;
            }
        }

        /// <remarks/>
        public string FreightRUR
        {
            get
            {
                return this.freightRURField;
            }
            set
            {
                this.freightRURField = value;
            }
        }

        /// <remarks/>
        public string InsuranceRUR
        {
            get
            {
                return this.insuranceRURField;
            }
            set
            {
                this.insuranceRURField = value;
            }
        }

        /// <remarks/>
        public string TotalRUR
        {
            get
            {
                return this.totalRURField;
            }
            set
            {
                this.totalRURField = value;
            }
        }

        /// <remarks/>
        public string WBDescription
        {
            get
            {
                return this.wBDescriptionField;
            }
            set
            {
                this.wBDescriptionField = value;
            }
        }
    }
}
