using super_duper_octo_palm_tree.app.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace super_duper_octo_palm_tree.app.services
{
    public class SharedCurrencyService
    {
        public double _currency = 0;
        
        public double GetCurrency(Currency currency)
        {
            switch (currency)
            {
                case Currency.EUR:
                    return 1d;
                case Currency.USD:
                    return _currency;
                default:
                    throw new ArgumentException("Unknonwn Currency");
            }
        }
    }
}
