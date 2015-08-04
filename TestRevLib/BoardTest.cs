using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevLib;

namespace TestRevLib
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void TestIsInBoard()
        {
            var board = new Board();

            Assert.IsTrue(board.IsInBoard(0, 0));
            Assert.IsTrue(board.IsInBoard(0, 7));
            Assert.IsTrue(board.IsInBoard(7, 0));
            Assert.IsTrue(board.IsInBoard(7, 7));


            Assert.IsFalse(board.IsInBoard(-1, 0));
            Assert.IsFalse(board.IsInBoard(0, -1));
            Assert.IsFalse(board.IsInBoard(8, 0));
            Assert.IsFalse(board.IsInBoard(0, 8));
        }

        [TestMethod]
        public void TestPutTokenOnEmptySpace()
        {
            var board = new Board();
            Player b = new Player(Token.Black);
            Player w = new Player(Token.White);

            board[0, 0] = b;
            Assert.AreEqual(1, board.ScoreOf(b));
            Assert.AreEqual(0, board.ScoreOf(w));
        }

        [TestMethod]
        public void TestPutTokenOnOtherPlayerToken()
        {
            var board = new Board();
            Player b = new Player(Token.Black);
            Player w = new Player(Token.White);

            board[0, 0] = b;
            board[0, 0] = w;

            Assert.AreEqual(0, board.ScoreOf(b));
            Assert.AreEqual(1, board.ScoreOf(w));
        }

        [TestMethod]
        public void TestCloneBoard()
        {
            var board = new Board();
            var player = new Player(Token.Black);
            board[0, 0] = player;

            var clonedBoard = board.Clone();
            Assert.AreEqual(player, clonedBoard[0, 0]);

            var white = new Player(Token.White);
            clonedBoard[4, 7] = white;

            Assert.AreNotEqual(board[4, 7], clonedBoard[4, 7]);
        }

        [TestMethod]
        public void TestScore()
        {
            Player p = new Player(Token.Black);
            Board board = new Board();

            Assert.AreEqual(0, board.ScoreOf(p));
            board[0, 0] = p;

            int actual = board.ScoreOf(p);
            int expected = 1;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            Player p = new Player(Token.Black);

            Board board = new Board();
            Action<int, int, bool> test = (x, y, expected) =>
            {
                bool actual = board.IsEmpty(x, y);
                Assert.AreEqual(expected, actual);
            };

            test(0, 0, true);

            board[0, 0] = p;

            test(0, 0, false);

            board[0, 0] = null;

            test(0, 0, true);
        }
    }
}
