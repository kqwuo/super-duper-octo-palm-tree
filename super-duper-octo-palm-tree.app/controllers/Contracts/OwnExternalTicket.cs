using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.controllers.Contracts
{
    public class OwnExternalTicket
    {
        private uint _additionalLuggage;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public uint NbAdditionalLuggage
        {
            get { return _additionalLuggage; }
            set
            {
                if (value > 3)
                    throw new ArgumentException("Excessive additional luggages");
                _additionalLuggage = value;
            }
        }
    }
}
