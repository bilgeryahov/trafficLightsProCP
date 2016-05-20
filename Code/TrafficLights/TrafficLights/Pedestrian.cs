using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrafficLights
{
    [Serializable]
    /// <summary>
    /// a component that moves on the sidewalks and crosswalks of the roads
    /// </summary>
    /// <seealso cref="TrafficLights.Moveable" />
    public class Pedestrian : Moveable
    {

        /// <summary>
        /// Gets or sets the current crosswalk.
        /// </summary>
        /// <value>The current crosswalk.</value>
        public Crosswalk CurrentCrosswalk
        {
            get
            {
                return CurrentCrosswalk;
            }
            set
            {
                if (value.CanHavePedestrians)
                { CurrentCrosswalk = value; }
            }
        }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <value>The direction.</value>
        public Direction Direction { get; private set; }

        //todo go in crosswalk, check direction, return from simulation the next crossing available's croswalk
        /// <summary>
        /// Gets the next crosswalk.
        /// </summary>
        /// <value>The next crosswalk.</value>
        public Crosswalk NextCrosswalk
        {
            get
            {
                
                    Crossing crossing = CurrentCrosswalk.Owner;
                    if (CurrentCrosswalk.To == Direction.Up && crossing.NextCrosswalkAbove.CanHavePedestrians)
                    {
                        return crossing.NextCrosswalkAbove;
                    }
                    else if (CurrentCrosswalk.To == Direction.Down && crossing.NextCrosswalkBelow.CanHavePedestrians)
                    {
                        return crossing.NextCrosswalkBelow;
                    }
                    else if (CurrentCrosswalk.To == Direction.Left && crossing.NextCrosswalkLeft.CanHavePedestrians)
                    {
                        return crossing.NextCrosswalkLeft;
                    }
                    else if (CurrentCrosswalk.To == Direction.Right && crossing.NextCrosswalkRight.CanHavePedestrians)
                    {
                        return crossing.NextCrosswalkRight;
                    }
                
                return null;
            }
        }

        public Pedestrian(int startX, int startY, params System.Drawing.Point[] path) : base(startX, startY, path) { }

        /// <summary>
        /// Updates the specified seconds.
        /// </summary>
        /// <param name="seconds">The seconds.</param>
        public override void Update(float seconds)
        {
            //moves the location of the pedestrians based on the elapsed time
            base.Update(seconds);
            if (CurrentPointIndex > 0)
            {
                int index = Convert.ToInt32(Math.Round(Convert.ToDecimal(seconds) * Path.Count));
                this.X = Path[index+CurrentPointIndex].X;
                this.Y = Path[index + CurrentPointIndex].Y;
            }
            else 
            {
                int index = Convert.ToInt32(Math.Round(Convert.ToDecimal(seconds) * Path.Count));
                this.X = Path[index].X;
                this.Y = Path[index].Y;
            }
        }

        /// <summary>
        /// Draws the when normal.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenNormal(System.Drawing.Graphics image)
        {
            //draws a dot on the crosswalk
            image.DrawEllipse(System.Drawing.Pens.Red, this.X, this.Y, 2, 2);
        }

        /// <summary>
        /// Draws the when active.
        /// </summary>
        /// <param name="image">The image.</param>
        protected override void DrawWhenActive(System.Drawing.Graphics image)
        {
            DrawWhenNormal(image);
        }
    }
}
