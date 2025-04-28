using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Catalog;

namespace Catalog.Tests
{
    [TestClass]
    public class BoardGameTest
    {
        [TestMethod]
        public void BoardGameConstructorTest()
        {
            string title = "Catan";
            string publisher = "Kosmos";
            bool availability = true;
            int minPlayers = 3;
            int maxPlayers = 4;
            int recommendedAge = 10;
            GameGenre genre = GameGenre.Strategy;

            var boardGame = new BoardGame(title, publisher, availability, minPlayers, maxPlayers, recommendedAge, genre);

            Assert.AreEqual(title, boardGame.title);
            Assert.AreEqual(publisher, boardGame.publisher);
            Assert.AreEqual(availability, boardGame.availbility);
            Assert.AreEqual(minPlayers, boardGame.minimumNumberOfPlayers);
            Assert.AreEqual(maxPlayers, boardGame.maximumNumberOfPlayers);
            Assert.AreEqual(recommendedAge, boardGame.recommendedAge);
            Assert.AreEqual(genre, boardGame.genre);
        }
    }
}
