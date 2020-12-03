using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mews.Fiscalization.Core.Model
{
    public sealed class EuropeanTaxpayerIdentificationNumberLimitation : ILimitation<(EuropeanUnionCountry country, string value)>
    {
        public bool IsValid((EuropeanUnionCountry country, string value) value)
        {
            return Regex.IsMatch(value.value, CountryInfo.EuropeanUnionTaxpayerNumberPatterns[value.country.Value]);
        }

        public void CheckValidity((EuropeanUnionCountry country, string value) value)
        {
            Check.IsNotNull(value.country, nameof(value.country));
        }
    }

    public sealed class EuropeanUnionTaxpayerIdentificationNumber : ValueWrapper<(EuropeanUnionCountry, string)>
    {
        private static readonly EuropeanTaxpayerIdentificationNumberLimitation Limitation = new EuropeanTaxpayerIdentificationNumberLimitation();

        public EuropeanUnionTaxpayerIdentificationNumber(EuropeanUnionCountry country, string value)
            : base((country, value), Limitation)
        {
        }

        public bool IsValid(EuropeanUnionCountry country, string value)
        {
            return IsValid((country, value), Limitation.ToEnumerable());
        }
    }
}
