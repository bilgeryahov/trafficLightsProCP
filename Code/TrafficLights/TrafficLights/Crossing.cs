using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>Holds the component on the grid</summary>
    public abstract class Crossing : Renderable
    {
        public bool HasPedestriansCrossing { get { return this.Crosswalks.Any(x => x.HasPedestriansCrossing); } }

        public abstract Crosswalk[] Crosswalks { get; }

        public Crosswalk CrosswalkOnLeft { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Left)); } }
        public Crosswalk CrosswalkOnRight { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Right)); } }
        public Crosswalk CrosswalkAbove { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Up)); } }
        public Crosswalk CrosswalkBelow { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Down)); } }

        public TrafficManager Owner { get; private set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        public bool IsOnTheGrid { get { return Row != -1 && Column != -1; } }

        public Crosswalk NextCrosswalkBelow
        {
            get
            {
                Crossing below = this.NextCrossingBelow;
                if (below == null) return null;
                return below.CrosswalkAbove;
            }
        }

        public Crosswalk NextCrosswalkAbove
        {
            get
            {
                Crossing above = this.NextCrossingAbove;
                if (above == null) return null;
                return above.CrosswalkBelow;
            }
        }

        public Crosswalk NextCrosswalkLeft
        {
            get
            {
                Crossing left = this.NextCrossingOnLeft;
                if (left == null) return null;
                return left.CrosswalkOnRight;
            }
        }

        public Crosswalk NextCrosswalkRight
        {
            get
            {
                Crossing right = this.NextCrossingOnRight;
                if (right == null) return null;
                return right.CrosswalkOnLeft;
            }
        }

        public Crossing NextCrossingBelow
        {
            get
            {
                Crossing[] nextRow = Owner.Grid[this.Row + 1];
                if (nextRow == null) return null;
                if (nextRow.Length < this.Column) return nextRow[this.Column];
                return null;
            }
        }

        public Crossing NextCrossingAbove
        {
            get
            {
                Crossing[] previousRow = Owner.Grid[this.Row - 1];
                if (previousRow == null) return null;
                if (previousRow.Length < this.Column) return previousRow[this.Column];
                return null;
            }
        }

        public Crossing NextCrossingOnRight
        {
            get
            {
                Crossing[] currentRow = Owner.Grid[this.Row];
                if (this.Column + 1 >= currentRow.Length) return null;
                return currentRow[this.Column + 1];
            }
        }

        public Crossing NextCrossingOnLeft
        {
            get
            {
                Crossing[] currentRow = Owner.Grid[this.Row];
                if (this.Column - 1 < 0) return null;
                return currentRow[this.Column - 1];
            }
        }

        public IEnumerable<Road> Roads
        {
            get
            {
                List<Road> roads = new List<Road>();

                foreach (Crosswalk crosswalk in Crosswalks)
                {
                    roads.AddRange(crosswalk.Lanes);
                }

                return roads;
            }
        }

        public IEnumerable<Road> Feeders
        {
            get
            {
                return Roads.Where(x => x.IsFeeder);
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> TopFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Up);
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> BotFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Down);
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> LeftFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Left);
            }
        }

        public System.Collections.Generic.IEnumerable<TrafficLights.Road> RightFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Right);
            }
        }

        public Crossing(TrafficManager owner, int row, int column)
        {
            this.Owner = owner;
        }

        public void ActivatePedestrianSensor(Crosswalk crosswalk)
        {
            throw new System.NotImplementedException();
        }

        public Crossing CreateCopy()
        {
            //using serialization create Full copy
            Crossing copy = null;
            throw new System.NotImplementedException();
        }

        public void RemoveFromGrid()
        {
            AssignGridLocation(-1, -1);
        }

        public void AssignGridLocation(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }
    }
}
