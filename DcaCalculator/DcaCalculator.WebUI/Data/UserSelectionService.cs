namespace DcaCalculator.WebUI.Data
{
    public class UserSelectionService
    {
        private string? savedString;

        public string Property
        {
            get => savedString ?? string.Empty;
            set
            {
                savedString = string.IsNullOrEmpty(savedString) ? value.Replace(",", string.Empty) : $"{value}";
            }
        }
    }
}
