using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mega.classes
{
    public class Agent
    {
        public Agent(int id_city, int delivery_period)
        {
            this.id_city = id_city;
            this.delivery_period = delivery_period;
        }
        public int id_city { get; set; }
        public int delivery_period { get; set; }
    }
}
