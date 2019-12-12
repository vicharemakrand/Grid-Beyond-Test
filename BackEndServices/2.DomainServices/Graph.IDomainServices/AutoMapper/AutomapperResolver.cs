using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graph.IDomainServices.AutoMapper
{
    public class MarketPriceConvertor : IValueConverter<string, double>
    {
        public double Convert(string sourceMember, ResolutionContext context)
        {
            if (double.TryParse(sourceMember, out double result))
            {
                return result;
            }

            return 0;
        }
    }

    public class DateConvertor : IValueConverter<string, DateTime>
    {
        public DateTime Convert(string sourceMember, ResolutionContext context)
        {
            var formats = new string[] { "dd/MM/yyyy", "dd/MM/yyyy HH:mm" };
            if (DateTime.TryParseExact(sourceMember, formats, System.Globalization.CultureInfo.InvariantCulture,
                                  System.Globalization.DateTimeStyles.None, out DateTime result))
            {
                return result;
            }

            return default(DateTime);
        }
    }
}
