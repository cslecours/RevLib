using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRevLib
{
    [TestClass]
    public class RevTest
    {
        public RevTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPutTokenWithNonValidPlayer()
        {
            var board = new RevGame();
            var player = new Player(0);

            board.PutToken(player, 5, 5);

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestPutTokenOnTokenOfSamePlayer()
        {
            var board = new RevGame();
            var player = board.CurrentPlayer;

            board.PutToken(player, 3, 4);

        }

        [TestMethod]
        public void TestCanPlayThere()
        {
            var board = new RevGame();

            bool actual = board.CanPlayThere(board.CurrentPlayer, 5, 4);
            bool expected = true;

            Assert.AreEqual(expected, actual);


            actual = board.CanPlayThere(board.CurrentPlayer, 5, 3);
            expected = false;

            Assert.AreEqual(expected, actual);


            actual = board.CanPlayThere(board.CurrentPlayer, 0, 0);
            expected = false;

            Assert.AreEqual(expected, actual);

            actual = board.CanPlayThere(board.CurrentPlayer, 4, 4);
            expected = false;

            Assert.AreEqual(expected, actual);
        }



        [TestMethod]
        public void TestPlay()
        {
            var board = new RevGame();
            var black = board.CurrentPlayer;

            /*
             * x0 1 2 3 4 5 6 7
             * 0
             * 1
             * 2
             * 3      o x
             * 4      x o
             * 5
             * 6
             * 7
             * */

            board.Play(black, 5, 4);
            var white = board.CurrentPlayer;
            Assert.AreEqual(4, board.ScoreOf(black));
            Assert.AreEqual(1, board.ScoreOf(white));
            Assert.AreNotSame(black, white);

            /*
             * x0 1 2 3 4 5 6 7
             * 0
             * 1
             * 2
             * 3      o x
             * 4      x x x
             * 5
             * 6
             * 7
             * */

            board.Play(white, 5, 5);
            Assert.AreEqual(3, board.ScoreOf(black));
            Assert.AreEqual(3, board.ScoreOf(white));
            /*
             * x0 1 2 3 4 5 6 7
             * 0
             * 1
             * 2
             * 3      o x
             * 4      x o x
             * 5          o
             * 6
             * 7
             * */

            board.Play(black, 4, 5);
            Assert.AreEqual(5, board.ScoreOf(black));
            Assert.AreEqual(2, board.ScoreOf(white));

            /*
             * x0 1 2 3 4 5 6 7
             * 0
             * 1
             * 2
             * 3      o x
             * 4      x x x
             * 5        x o
             * 6
             * 7
             * */

            board.Play(white, 5, 3);
            Assert.AreEqual(3, board.ScoreOf(black));
            Assert.AreEqual(5, board.ScoreOf(white));

            /*
             * x0 1 2 3 4 5 6 7
             * 0
             * 1
             * 2
             * 3      o o o
             * 4      x x o
             * 5        x o
             * 6
             * 7
             * */

            board.Play(black, 6, 5);
            Assert.AreEqual(5, board.ScoreOf(black));
            Assert.AreEqual(4, board.ScoreOf(white));

            /*
             * x0 1 2 3 4 5 6 7
             * 0
             * 1
             * 2
             * 3      o o o
             * 4      x x o
             * 5        x x x
             * 6
             * 7
             * */

            board.Play(white, 4, 6);
            Assert.AreEqual(7, board.ScoreOf(white));
            Assert.AreEqual(3, board.ScoreOf(black));

            /*
             * x0 1 2 3 4 5 6 7
             * 0
             * 1
             * 2
             * 3      o o o
             * 4      x o o
             * 5        o x x
             * 6        o
             * 7
             * */
        }
    }
}
