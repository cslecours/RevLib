using System;

namespace RevLib
{
	public class ChineseChessGame : BoardGame
	{
		public ChineseChessGame(): base(new PlayerQueue()){}

		public ChineseChessGame(IPlayerQueue queue) : base(queue)
		{

		}

		public bool CanSelectToPlay(Player p, int x, int y)
		{
			var offcalc = new OffsetCalculator(x, y);

			if(board[x,y] == p){    
				for (int i = 0; i < 9; i++)
				{
					offcalc.setDirection(i);
					for (int offset = 1; offset <= 2; offset++)
					{
						offcalc.MakeOffset(offset);
						if (board.IsInBoard(offcalc.ResultX, offcalc.ResultY) && board.IsEmpty(offcalc.ResultX, offcalc.ResultY))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		public bool CanPlayThere(Player p, Coord start, int x, int y)
		{
			if (board.IsInBoard(x,y) && board.IsEmpty(x,y))
			{
				Coord end = new Coord(x, y);
				var distance = end - start;
				int travelLength = Math.Abs(distance.x) + Math.Abs(distance.y);
				return travelLength == 1 || travelLength == 2;
			}
			else
			{
				return false;
			}
		}

		public void Play(Player p, Coord start, Coord end)
		{
			if (CanSelectToPlay(p, start.x, start.y) && board.IsEmpty(end.x, end.y))
			{
				var distance = end - start;
				int travelLength = Math.Abs(distance.x) + Math.Abs(distance.y);

				//Move -> Remove original
				if (travelLength == 2)
				{
					board[start.x, start.y] = null;
				}

				//Copy (or Move) -> Put token
				PutToken(p, end.x, end.y);
				var offsetCalc = new OffsetCalculator(end.x, end.y);
				for (int i = 0; i < 9; i++)
				{
					offsetCalc.setDirection(i);
					offsetCalc.MakeOffset(1);
					
					if (board.CanOvertakeToken(p, offsetCalc.ResultX, offsetCalc.ResultY))
					{
						PutToken(p, offsetCalc.ResultX, offsetCalc.ResultY);
					}
				}

				HandlePlayerChange();

                if (SnapshotContainer != null)
                {
                    SnapshotContainer.TakeSnapShot(new TwoPartTurn() { Start = start, End = end, PlayerThatPlayed = p, PlayerToPlay = CurrentPlayer, Board = board.Clone() });
                }
			}
		}

		public override bool CanPlay(Player p)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (CanSelectToPlay(p, i, j))
					{
						return true;
					}
				}
			}
			return false;
		}

		protected override void CreateStartingBoard()
		{
			var black = playerQueue.Current;
			var white = playerQueue.Next;

			PutToken(black, 0, 0);
			PutToken(black, 7, 7);
			PutToken(white, 0, 7);
			PutToken(white, 7, 0);
		}
	}
}
