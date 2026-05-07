namespace JobHunter.Models
{
    public class JobSession
    {
        private const string FavsKey = "myfavorites";
        private ISession session { get; set; }

        public JobSession(ISession session)
        {
            this.session = session;
        }

        public void SetFavoriteIds(List<int> ids)
        {
            session.SetObject(FavsKey, ids);
        }

        public List<int> GetFavoriteIds()
        {
            return session.GetObject<List<int>>(FavsKey) ?? new List<int>();
        }

        public void ClearFavorites()
        {
            session.Remove(FavsKey);
        }
    }
}
