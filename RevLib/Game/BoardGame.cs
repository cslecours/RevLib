using RevLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace RevLib
{
    public abstract class BoardGame
    {
        protected IPlayerQueue playerQueue;
        protected Board board;

        public event EventHandler GameOver;
        public bool IsGameOver { get { return board.IsGameOver; }}

        public ISnapshotContainer<Turn> SnapshotContainer { get; set; }

        public IDictionary<Player, PlayerScore> ScoreBoard { get { return board.scoreboard.Scores; } }

        public Player CurrentPlayer { get { return playerQueue.Current; } }

        public Player this[int x, int y]
        {
            get { return board[x, y]; }
        }

        protected abstract void CreateStartingBoard();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardGame(IPlayerQueue playerQueue)
        {
            this.playerQueue = playerQueue;
            board = new Board();
            CreateStartingBoard();
            SnapshotContainer = new SnapshotContainer<Turn>();
        }

        public void LoadBoard(Board b, Player p)
        {
            this.board = b;
            playerQueue.MakeCurrentTurnOf(p);
        }

        public abstract bool CanPlay(Player p);

        public Board CloneBoard()
        {
            return board.Clone();
        }

        protected void HandlePlayerChange()
        {
            if (CanPlay(playerQueue.Next))
            {
                playerQueue.ChangePlayer();
            }
            else if(CanPlay(playerQueue.Current))
            {
                playerQueue.SkipTurn();
            }
            else
            {
                OnGameOver();
            }
        }

        private void OnGameOver()
        {
            board.IsGameOver = true;
            var handler = GameOver;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        public void ChangePlayer()
        {
            playerQueue.ChangePlayer();
        }

        public void PutToken(Player p, int x, int y)
        {
            if (!IsValid(p,x,y))
            {
                throw new ArgumentException();
            }

            board[x, y] = p;
        }

        public bool IsValid(Player p, int x, int y)
        {
            return board.IsInBoard(x, y) && board[x,y] != p && playerQueue.IsPlayerInGame(p);
        }

        public int ScoreOf(Player p)
        {
            return board.ScoreOf(p);
        }
    }
}
