using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevLib;

namespace TestRevLib
{
    [TestClass]
    public class PlayerQueueTest
    {
        [TestMethod]
        public void TestPlayerChange()
        {

            var playerQueue = new PlayerQueue();
            var player = playerQueue.Current;

            //Has the player changed?
            playerQueue.ChangePlayer();
            var newPlayer = playerQueue.Current;
            Assert.AreNotSame(newPlayer, player);

            //Are we back to the first player?
            playerQueue.ChangePlayer();
            newPlayer = playerQueue.Current;
            Assert.AreSame(newPlayer, player);

        }
    }
}
