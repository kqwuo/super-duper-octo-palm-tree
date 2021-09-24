using System;
using System.Collections.Generic;
using System.Text.Json;

namespace super_duper_octo_palm_tree.app.models
{
    public class Ticket
    {
        private uint _basePriceDiscount;

        public Ticket()
        {
        }


        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Nationality { get; set; }

        public UserType UserType { get; set; }

        public double BasePrice { get; set; }

        public double AdditionalPrice { 
            get
            {
                double result = 0;

                foreach(FlightOptions option in Options)
                {
                    if (option.ReturnType == "bool")
                    {
                        if (((JsonElement)option.Value).GetBoolean())
                            result += option.Price;
                    }
                    else if (option.ReturnType == "number")
                    {
                        if (option.Value is JsonElement)
                            result += ((JsonElement)option.Value).GetInt32() * option.Price;
                        else
                            result += (UInt32)option.Value * option.Price;
                    }
                }

                return result;
            }
        }

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

        public List<FlightOptions> Options { get; set; }

        public List<AdditionalField> AdditionalFields { get; set; }

        public uint NbAdditionalLuggage
        {
            get
            {
                return (uint)Options.Find(o => o.FieldName == "AdditionalLuggage").Value;
            }
        }
    }
}
