namespace DcaCalculator.WebUI.Data
{
    public class UserSelectionService
    {
        private string? symbols;
        private string? identifiers;

        public string Symbols
        {
            get => symbols ?? string.Empty;
            set
            {
                symbols = string.IsNullOrEmpty(symbols) ? value.Replace(",", string.Empty) : $"{value}";
            }
        }

        public string Identifiers
        {
            get => identifiers ?? string.Empty;
            set
            {
                identifiers = string.IsNullOrEmpty(identifiers) ? value.Replace(",", string.Empty) : $"{value}";
            }
        }
    }
}
