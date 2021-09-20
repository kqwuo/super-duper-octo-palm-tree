using System;

namespace super_duper_octo_palm_tree.app.models
{
    public class Ticket
    {
        private uint _additionalLuggage;
        private uint _basePriceDiscount;

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

        public UserType UserType { get; set; }

        public double BasePrice { get; set; }

        public double AdditionalPrice { get; set; }

        public double PaidTotal { get { return DiscountedBasePrice + AdditionalPrice; } }

        public uint BasePriceDiscount {
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

        public double DiscountedBasePrice { get { return BasePrice * (100 - BasePriceDiscount) / 100; } }
    }
}
