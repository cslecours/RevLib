using System.Collections.Generic;
using System.Linq;

namespace RevLib
{
    public class ComputerPlayer : Player
    {
        public ComputerPlayer(Token t) : base(t) { }

        public void Play(RevGame game)
        {
            var plays = MakeListOfPossiblePlays(this, game);
            var bestPlay = plays.First(x => x.Value == plays.Max(y => y.Value)).Key;
            game.Play(this, bestPlay.x, bestPlay.y);
        }

        protected Dictionary<Coord,int> MakeListOfPossiblePlays(Player p, RevGame game)
        {
            int col = 8;
            int row = 8;
            Dictionary<Coord,int> d = new Dictionary<Coord, int>();

            for (int i = 0; i < col; i++)
            {
                for (int j = 0; j < row; j++)
                {
                    if (game.CanPlayThere(p, i, j))
                    {
                        var tokens = game.MakeListOfConvertedTokens(p, i, j);
                        d[new Coord(i,j)] = tokens.Count();
                    }
                }
            }
            return d;
        }
    }
}
