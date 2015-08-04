using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

namespace RevLib
{
    public class Board
    {
        public const int Width = 8;

        protected Player[,] board;
        public Scoreboard scoreboard;
        public bool IsGameOver { get; set; }

        public Board()
        {
            IsGameOver = false;
            board = new Player[Width, Width];
            scoreboard = new Scoreboard();
        }

        public Player this[int x, int y]
        {
            get{return board[x,y];}
            set { PutToken(value, x, y); }
        }
    
        private void PutToken(Player p, int x, int y)
        {
            Player other = board[x, y];

            if (other != null)
            {
                scoreboard.RemovePointFrom(other);
            }

            if (p != null)
            {
                scoreboard.AddPointTo(p);
            }

            board[x, y] = p;
        }

        [DebuggerStepThrough]
        public bool IsInBoard(int x, int y)
        {
            return !(x < 0 | x > Width - 1 | y < 0 | y > Width - 1);
        }

        [DebuggerStepThrough]
        public bool IsEmpty(int x, int y)
        {
            return board[x, y] == null;
        }

        [DebuggerStepThrough]
        public bool CanPutToken(Player p, int x, int y)
        {
            return board[x, y] != p;
        }

        public bool CanOvertakeToken(Player p, int x, int y)
        {
            return IsInBoard(x, y) && !IsEmpty(x, y) && CanPutToken(p, x, y);
        }

        public Board Clone()
        {
            return new Board()
            {
                board = (Player[,])board.Clone(),
                scoreboard = scoreboard.Clone(),
                IsGameOver = this.IsGameOver,
            };
        }

        public int ScoreOf(Player p)
        {
            return scoreboard.ScoreOf(p);
        }
    }
}