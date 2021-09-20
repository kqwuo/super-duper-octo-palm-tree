using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.models
{
    public class Ticket
    {
        private uint _additionalLuggage;

        public Ticket()
        {
            _additionalLuggage = 0;
        }

        public uint NbAdditionalLuggage {
            get { return _additionalLuggage; }
            set {
                if (value > 3)
                    throw new ArgumentException("Excessive additional luggages");
                _additionalLuggage = value;
            }
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public double PaidBasePrice { get; set; }

        public double PaidAdditionalPrice { get; set; }

        public double PaidTotal { get { return PaidBasePrice + PaidAdditionalPrice; } }
    }
}
