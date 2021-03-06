﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    /// <summary>
    /// abstract class defining the basic properties and methods for the crossings 
    /// </summary>
    /// <seealso cref="TrafficLights.Renderable" />
    [Serializable]
    public abstract class Crossing : Renderable
    {
        /// <summary>
        /// Gets a value indicating whether this instance has pedestrians crossing.
        /// </summary>
        /// <value><c>true</c> if this instance has pedestrians crossing; otherwise, <c>false</c>.</value>
        public bool HasPedestriansCrossing { get { return this.Crosswalks.Any(x => x.HasPedestriansCrossing); } }

        /// <summary>
        /// When a car crosses inside a crossing, this value gets increased.
        /// </summary>
        public int XTimesCrossed { get; set; }

        /// <summary>
        /// Gets the crosswalks.
        /// </summary>
        /// <value>The crosswalks.</value>
        private Crosswalk[] crosswalks = null;

        public Crosswalk[] Crosswalks
        {
            get
            {
                if (crosswalks == null)
                {
                    crosswalks = GenerateCrosswalks;
                    foreach (Crosswalk crosswalk in crosswalks)
                    {
                        crosswalk.Owner = this;
                    }
                    foreach (Trafficlight light in this.Lights)
                    {
                        light.SetOwner(this);
                    }
                }
                return crosswalks;
            }
        }
        protected abstract Crosswalk[] GenerateCrosswalks { get; }

        /// <summary>
        /// Gets the crosswalk on left.
        /// </summary>
        /// <value>The crosswalk on left.</value>
        public Crosswalk CrosswalkOnLeft { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Left)); } }
        /// <summary>
        /// Gets the crosswalk on right.
        /// </summary>
        /// <value>The crosswalk on right.</value>
        public Crosswalk CrosswalkOnRight { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Right)); } }
        /// <summary>
        /// Gets the crosswalk above.
        /// </summary>
        /// <value>The crosswalk above.</value>
        public Crosswalk CrosswalkAbove { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Up)); } }
        /// <summary>
        /// Gets the crosswalk below.
        /// </summary>
        /// <value>The crosswalk below.</value>
        public Crosswalk CrosswalkBelow { get { return this.Crosswalks.FirstOrDefault(x => x.Entrylanes.All(y => y.From == Direction.Down)); } }


        /// <summary>
        /// Gets the owner.
        /// </summary>
        /// <value>The owner.</value>
        [NonSerialized]
        public TrafficManager Owner;
        /// <summary>
        /// Gets the row.
        /// </summary>
        /// <value>The row.</value>
        public int Row { get; private set; }
        /// <summary>
        /// Gets the column.
        /// </summary>
        /// <value>The column.</value>
        public int Column { get; private set; }

        /// <summary>
        /// Used to restore in its previous place at the grid.
        /// Replaces the original row and column since after deletion they get -1 -1.
        /// </summary>
        public int RowRecycleManager { get; private set; }
        public int ColumnRecycleManager { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is on the grid.
        /// </summary>
        /// <value><c>true</c> if this instance is on the grid; otherwise, <c>false</c>.</value>
        public bool IsOnTheGrid { get { return Row != -1 && Column != -1; } }

        /// <summary>
        /// Gets the next crosswalk below.
        /// </summary>
        /// <value>The next crosswalk below.</value>
        public Crosswalk NextCrosswalkBelow
        {
            get
            {
                Crossing below = this.NextCrossingBelow;
                if (below == null) return null;
                return below.CrosswalkAbove;
            }
        }

        /// <summary>
        /// Gets the next crosswalk above.
        /// </summary>
        /// <value>The next crosswalk above.</value>
        public Crosswalk NextCrosswalkAbove
        {
            get
            {
                Crossing above = this.NextCrossingAbove;
                if (above == null) return null;
                return above.CrosswalkBelow;
            }
        }

        /// <summary>
        /// Gets the next crosswalk left.
        /// </summary>
        /// <value>The next crosswalk left.</value>
        public Crosswalk NextCrosswalkLeft
        {
            get
            {
                Crossing left = this.NextCrossingOnLeft;
                if (left == null) return null;
                return left.CrosswalkOnRight;
            }
        }

        /// <summary>
        /// Gets the next crosswalk right.
        /// </summary>
        /// <value>The next crosswalk right.</value>
        public Crosswalk NextCrosswalkRight
        {
            get
            {
                Crossing right = this.NextCrossingOnRight;
                if (right == null) return null;
                return right.CrosswalkOnLeft;
            }
        }

        /// <summary>
        /// Gets the next crossing below.
        /// </summary>
        /// <value>The next crossing below.</value>
        public Crossing NextCrossingBelow
        {
            get
            {
                Crossing[] nextRow = Owner.Grid[this.Row + 1];
                if (nextRow == null) return null;
                if (nextRow.Length > this.Column) return nextRow[this.Column];
                return null;
            }
        }

        /// <summary>
        /// Gets the next crossing above.
        /// </summary>
        /// <value>The next crossing above.</value>
        public Crossing NextCrossingAbove
        {
            get
            {
                Crossing[] previousRow = Owner.Grid[this.Row - 1];
                if (previousRow == null) return null;
                if (previousRow.Length > this.Column) return previousRow[this.Column];
                return null;
            }
        }

        /// <summary>
        /// Gets the next crossing on right.
        /// </summary>
        /// <value>The next crossing on right.</value>
        public Crossing NextCrossingOnRight
        {
            get
            {
                Crossing[] currentRow = Owner.Grid[this.Row];
                if (this.Column + 1 >= currentRow.Length) return null;
                return currentRow[this.Column + 1];
            }
        }

        /// <summary>
        /// Gets the next crossing on left.
        /// </summary>
        /// <value>The next crossing on left.</value>
        public Crossing NextCrossingOnLeft
        {
            get
            {
                Crossing[] currentRow = Owner.Grid[this.Row];
                if (this.Column - 1 < 0) return null;
                return currentRow[this.Column - 1];
            }
        }

        public IEnumerable<Renderable> ChildElements
        {
            get
            {
                List<Renderable> children = new List<Renderable>();
                children.AddRange(this.Crosswalks);
                children.AddRange(Lights);
                //children.AddRange(Lanes);

                return children;
            }
        }

        public abstract System.Drawing.Image Image { get; }

        /// <summary>
        /// Gets the lanes.
        /// </summary>
        /// <value>The lanes.</value>
        public IEnumerable<Lane> Lanes
        {
            get
            {
                List<Lane> roads = new List<Lane>();

                foreach (Crosswalk crosswalk in Crosswalks)
                {
                    roads.AddRange(crosswalk.Lanes);
                }

                return roads;
            }
        }

        /// <summary>
        /// Gets the lanes.
        /// </summary>
        /// <value>The lanes.</value>
        public IEnumerable<Trafficlight> Lights
        {
            get
            {
                List<Trafficlight> lights = new List<Trafficlight>();

                foreach (Crosswalk crosswalk in Crosswalks)
                {
                    lights.Add(crosswalk.Light);
                }

                return lights;
            }
        }

        /// <summary>
        /// Gets the feeders.
        /// </summary>
        /// <value>The feeders.</value>
        public IEnumerable<Lane> Feeders
        {
            get
            {
                return Lanes.Where(x => x.IsFeeder);
            }
        }

        /// <summary>
        /// Gets the top feeders.
        /// </summary>
        /// <value>The top feeders.</value>
        public IEnumerable<Lane> TopFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Up);
            }
        }

        /// <summary>
        /// Gets the bot feeders.
        /// </summary>
        /// <value>The bot feeders.</value>
        public IEnumerable<Lane> BotFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Down);
            }
        }

        /// <summary>
        /// Gets the left feeders.
        /// </summary>
        /// <value>The left feeders.</value>
        public IEnumerable<Lane> LeftFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Left);
            }
        }

        /// <summary>
        /// Gets the right feeders.
        /// </summary>
        /// <value>The right feeders.</value>
        public IEnumerable<Lane> RightFeeders
        {
            get
            {
                return Feeders.Where(x => x.From == Direction.Right);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Crossing"/> class.
        /// </summary>
        /// <param name="owner">The owner.</param>
        public Crossing(TrafficManager owner)
        {
            this.Owner = owner;
        }

        /// <summary>
        /// Activates the pedestrian sensor.
        /// </summary>
        /// <param name="crosswalk">The crosswalk.</param>
        public void ActivatePedestrianSensor(Crosswalk crosswalk)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Creates the copy.
        /// </summary>
        /// <returns>Crossing.</returns>
        public Crossing CreateCopy()
        {
            var stream = new System.IO.MemoryStream();
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            Crossing copy = formatter.Deserialize(stream) as Crossing;
            copy.Owner = this.Owner;
            return copy;
        }

        /// <summary>
        /// Removes from grid.
        /// </summary>
        public void RemoveFromGrid()
        {
            AssignGridLocation(-1, -1);
        }

        /// <summary>
        /// Assigns the grid location.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="column">The column.</param>
        public void AssignGridLocation(int row, int column)
        {
            this.Row = row;
            this.Column = column;

            // Set these values so they can be used from the Recycle Manager.
            if(row!=-1 && column != -1)
            {
                this.RowRecycleManager = row;
                this.ColumnRecycleManager = column;
            }
        }
        public override void Update(float seconds)
        {
            //set interval, but not timeout if first pass
            foreach (Renderable child in this.ChildElements)
                child.Update(seconds);
        }
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            
            foreach (Renderable child in this.ChildElements)
            {
                child.Draw(image);
            }  
        }

        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            foreach (Renderable child in this.ChildElements)
                child.Draw(image);
        }
        public bool IntervalsSet = false;
        public void SetStartIntervals()
        {
            foreach (Trafficlight light in this.Lights)
            {
                light.RedSeconds = this.Lights.Where(x=>x != light).Sum(x=>x.GreenSeconds+x.YellowSeconds);
                light.TimeoutSeconds = this.Lights.Where(x => x.Position < light.Position).Sum(x => x.YellowSeconds+x.GreenSeconds);
            }
            IntervalsSet = true;
        }
        public void Reset()
        {
            // Reset the crossed times in order to start from 0 after stopping a simulation and starting over again.
            this.XTimesCrossed = 0;

            SetStartIntervals();
            foreach (Crosswalk crosswalk in this.Crosswalks)
            {
                crosswalk.Reset();
            }
        }
    }
}
