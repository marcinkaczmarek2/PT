
namespace Data.Catalog
{
    public class BoardGame : Borrowable
    {
        public int minimumNumberOfPlayers { set; get; }
        public int maximumNumberOfPlayers { set; get; }
        public int recommendedAge { set; get; }
        public GameGenre genre { set; get; }

        public BoardGame(string title, string publisher, bool availbility, int minimumNumberOfPlayers
            ,int maximumNumberOfPlayers, int recommendedAge, GameGenre genre)
            : base(title, publisher, availbility)
        {
            this.minimumNumberOfPlayers = minimumNumberOfPlayers;
            this.maximumNumberOfPlayers = maximumNumberOfPlayers;
            this.recommendedAge = recommendedAge;
            this.genre = genre;
        }
    }

    public enum GameGenre
    {
        Strategy,
        Party,
        Cooperative,
        Competitive,
        Abstract,
        Thematic,
        DeckBuilding,
        RolePlaying,
        WarGame,
        Economic,
        Puzzle,
        Family,
        Trivia,
        Dexterity,
        Adventure,
        AreaControl,
        Bluffing,
        Storytelling,
        WorkerPlacement
    }
}
