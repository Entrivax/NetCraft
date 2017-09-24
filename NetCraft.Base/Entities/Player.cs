namespace NetCraft.Base.Entities
{
    public class Player
    {
        public string Username { get; private set; }

        public Player(string username)
        {
            Username = username;
        }
    }
}
