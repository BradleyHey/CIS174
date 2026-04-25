using Microsoft.AspNetCore.Http;

namespace FirstResponsiveWebAppHey.Models.Olympics
{
    public class OlympicsSession
    {
        private const string GameKey = "game";
        private const string CatKey = "cat";

        private ISession session { get; set; }
        public OlympicsSession(ISession session) => this.session = session;

        public void SetActiveGame(string activeGame) =>
            session.SetString(GameKey, activeGame);
        public string GetActiveGame() => 
            session.GetString(GameKey) ?? "all";

        public void SetActiveCat(string activeCat) =>
            session.SetString(CatKey, activeCat);
        public string GetActiveCat() => 
            session.GetString(CatKey) ?? "all";
    }
}