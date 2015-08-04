using System;
using System.Collections.Generic;

namespace RevLib
{
    /// <summary>
    /// Enumerate through 2d coord [size,size] in a spiral way (counterclockwise). Starts at the low right corner of the spiral defined at distanceFromCenter
    /// </summary>
    public sealed class SpiralCoordEnumerator : IEnumerator<Coord>
    {

        private int x;
        private int y;
        int distanceFromCenter;
        int size;

        public SpiralCoordEnumerator(int size, int distanceFromCenter)
        {
            this.size = size;

            x = int.MinValue;
            y = int.MinValue;

            this.distanceFromCenter = distanceFromCenter;
        }


        public Coord Current
        {
            get { return new Coord(x,y); }
        }

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if (x == int.MinValue)
            {
                x = (size / 2) - distanceFromCenter;
                y = (size / 2) - distanceFromCenter;
            }
            else
            {
                DoTheMove();
            }


            return x >= 0;
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        private void DoTheMove()
        {
            if (x + y < size - 1)
            {
                if (x < y)
                {
                    y--;
                }
                else
                {
                    x++;
                }
            }
            else
            {
                if (x > y)
                {
                    y++;
                }
                else
                {
                    x--;
                }
            }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return this; }
        }
    }
}
