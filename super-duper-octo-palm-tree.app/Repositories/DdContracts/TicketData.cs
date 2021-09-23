using System;

namespace super_duper_octo_palm_tree.app.Repositories.DdContracts
{
    public class TicketData
    {
        public string IdOrder { get; set; }

        private uint _additionalLuggage;
        private uint _basePriceDiscount;

        public TicketData()
        {
            _additionalLuggage = 0;
        }

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

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public double BasePrice { get; set; }
        public double AdditionalPrice { get; set; }
        public uint BasePriceDiscount
        {
            get
            {
                return _basePriceDiscount;
            }
            set
            {
                if (value > 100)
                    throw new ArgumentException("Discount cannot be superior to 100%");
                _basePriceDiscount = value;
            }
        }

    }
}
