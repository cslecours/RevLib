using System.Collections.Generic;
using System.Linq;

namespace RevLib
{
    public class Scoreboard
    {
        public IDictionary<Player, PlayerScore> Scores { get; set; }

        public Scoreboard()
        {
            Scores = new Dictionary<Player,PlayerScore>();
        }

        public void AddPointTo(Player p)
        {
            if (Scores.ContainsKey(p))
            {
                Scores[p].Score++;
            }
            else
            {
                Scores.Add(p, new PlayerScore() { Score = 1 });
            }
        }

        public void RemovePointFrom(Player p)
        {
            if (Scores.ContainsKey(p) && Scores[p].Score > 0)
            {
                Scores[p].Score--;
            }
        }

        public int ScoreOf(Player p)
        {
            if (Scores.ContainsKey(p))
            {
                return Scores[p].Score;
            }
            else
            {
                return 0;
            }
        }

        public Scoreboard Clone()
        {
            return new Scoreboard() { Scores = Scores.ToDictionary((x) => x.Key, x => (PlayerScore)x.Value.Clone())};
        }
    }
}
