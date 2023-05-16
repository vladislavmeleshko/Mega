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
    public class MEScheme
    {

        private MESchemeDeliveryUpdate[] deliveryUpdateField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("DeliveryUpdate")]
        public MESchemeDeliveryUpdate[] DeliveryUpdate
        {
            get
            {
                return this.deliveryUpdateField;
            }
            set
            {
                this.deliveryUpdateField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class MESchemeDeliveryUpdate
    {

        private object wBOpenDateField;

        private uint wBNumberField;

        private object wBOldNumberField;

        private object wBFedexNumberField;

        private object lastEventField;

        private object lastEventDateField;

        private object shipperCityField;

        private object shipperAgentField;

        private object consigneeCityField;

        private object consigneeAgentField;

        private object consigneeCompanyField;

        private object consigneeAddressField;

        private string deliveryDateField;

        private string deliveryTimeField;

        private string submitterField;

        private object courierField;

        /// <remarks/>
        public object WBOpenDate
        {
            get
            {
                return this.wBOpenDateField;
            }
            set
            {
                this.wBOpenDateField = value;
            }
        }

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
        public object WBFedexNumber
        {
            get
            {
                return this.wBFedexNumberField;
            }
            set
            {
                this.wBFedexNumberField = value;
            }
        }

        /// <remarks/>
        public object LastEvent
        {
            get
            {
                return this.lastEventField;
            }
            set
            {
                this.lastEventField = value;
            }
        }

        /// <remarks/>
        public object LastEventDate
        {
            get
            {
                return this.lastEventDateField;
            }
            set
            {
                this.lastEventDateField = value;
            }
        }

        /// <remarks/>
        public object ShipperCity
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
        public object ShipperAgent
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
        public object ConsigneeCity
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
        public object ConsigneeAgent
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
        public object ConsigneeCompany
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
        public object ConsigneeAddress
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
        public string DeliveryDate
        {
            get
            {
                return this.deliveryDateField;
            }
            set
            {
                this.deliveryDateField = value;
            }
        }

        /// <remarks/>
        public string DeliveryTime
        {
            get
            {
                return this.deliveryTimeField;
            }
            set
            {
                this.deliveryTimeField = value;
            }
        }

        /// <remarks/>
        public string Submitter
        {
            get
            {
                return this.submitterField;
            }
            set
            {
                this.submitterField = value;
            }
        }

        /// <remarks/>
        public object Courier
        {
            get
            {
                return this.courierField;
            }
            set
            {
                this.courierField = value;
            }
        }
    }
}
