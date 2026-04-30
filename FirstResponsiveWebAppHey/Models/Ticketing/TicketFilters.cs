namespace FirstResponsiveWebAppHey.Models.Ticketing
{
    public class TicketFilters
    {
        public TicketFilters(string filterstring)
        {
            FilterString = filterstring ?? "all";
            string[] filters = FilterString.Split('-');
            StatusId = filters[0];
        }

        public string FilterString { get; }
        public string StatusId { get; }

        public bool HasStatus => StatusId.ToLower() != "all";
    }
}
