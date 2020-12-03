namespace Mews.Fiscalization.Core.Model
{
    public sealed class TaxpayerIdentificationNumberLimitation : ILimitation<(Country country, string value)>
    {
        public bool IsValid((Country country, string value) value)
        {
            if (value.country.IsNull())
            {
                return false;
            }
            if (value.country is EuropeanUnionCountry europeanCountry)
            {
                return new EuropeanTaxpayerIdentificationNumberLimitation().IsValid((europeanCountry, value.value));
            }

            return true;
        }

        public void CheckValidity((Country country, string value) value)
        {
            Check.IsNotNull(value.country, nameof(value.country));
        }
    }

    public class TaxpayerIdentificationNumber : ValueWrapper<(Country, string)>
    {
        private static readonly TaxpayerIdentificationNumberLimitation Limitation = new TaxpayerIdentificationNumberLimitation();

        public TaxpayerIdentificationNumber(Country country, string taxpayerNumber)
            : base((country, taxpayerNumber), Limitation)
        {
            Check.IsNotNull(country, nameof(country));
            Check.Condition(IsValid(country, taxpayerNumber), "Invalid taxpayer identification number.");
        }

        public static bool IsValid(Country country, string taxpayerNumber)
        {
            return IsValid((country, taxpayerNumber), Limitation.ToEnumerable());
        }
    }
}
