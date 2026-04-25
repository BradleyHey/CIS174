using System.Collections.Generic;

namespace FirstResponsiveWebAppHey.Models.Olympics
{
    public class OlympicsViewModel
    {
        public string ActiveGame { get; set; } = "all";
        public string ActiveCat { get; set; } = "all";
        
        public Country Country { get; set; } = new Country();
        public List<Game> Games { get; set; } = new List<Game>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Country> Countries { get; set; } = new List<Country>();

        // methods to help view determine active link
        public string CheckActiveGame(string g) =>
            g.ToLower() == ActiveGame.ToLower() ? "active" : "";

        public string CheckActiveCat(string c) =>
            c.ToLower() == ActiveCat.ToLower() ? "active" : "";
    }
}