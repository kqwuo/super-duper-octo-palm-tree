﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.Repositories.DdContracts
{
    public class TicketData
    {
        private uint _additionalLuggage;
        private uint _basePriceDiscount;

        public TicketData()
        {
            _additionalLuggage = 0;
        }

        public string IdOrder { get; set; }
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
        public double PaidTotal { get { return DiscountedBasePrice + AdditionalPrice; } }
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

        public double DiscountedBasePrice { get { return BasePrice * (100 - BasePriceDiscount) / 100; } }
    }
}
