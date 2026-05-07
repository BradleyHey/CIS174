namespace FirstResponsiveWebAppHey.Models.Olympics
{
    public class Country
    {
        public string CountryID { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Game Game { get; set; } = null!;
        public string GameID { get; set; } = string.Empty;
        public Category Category { get; set; } = null!;
        public string CategoryID { get; set; } = string.Empty;
        public string Sport { get; set; } = string.Empty;
        public string LogoImage { get; set; } = string.Empty;
    }
}