using Data.Implementations.Enums;

namespace Data.Implementations.Catalog
{
    internal sealed class BoardGame : Borrowable
    {
        internal int minimumNumberOfPlayers { private set; get; }
        internal int maximumNumberOfPlayers { private set; get; }
        internal int recommendedAge { private set; get; }
        internal GameGenre genre { private set; get; }

        internal BoardGame(string title, string publisher, bool availbility, int minimumNumberOfPlayers
            ,int maximumNumberOfPlayers, int recommendedAge, GameGenre genre)
            : base(title, publisher, availbility)
        {
            this.minimumNumberOfPlayers = minimumNumberOfPlayers;
            this.maximumNumberOfPlayers = maximumNumberOfPlayers;
            this.recommendedAge = recommendedAge;
            this.genre = genre;
        }
    }
}
