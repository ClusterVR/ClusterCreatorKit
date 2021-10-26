namespace ClusterVR.CreatorKit.World
{
    public sealed class Ranking
    {
        public int Rank;
        public User User;

        public Ranking(int rank, User user)
        {
            Rank = rank;
            User = user;
        }
    }
}
