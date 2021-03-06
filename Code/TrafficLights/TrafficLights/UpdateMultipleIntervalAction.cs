﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    public class UpdateMultipleIntervalAction : UndoableAction
    {
        private List<Trafficlight> Lights { get { return Crossing.Lights.ToList(); } }
        private List<float> previousIntervals;
        public Crossing Crossing { get { return grid[CrossingRow][CrossingColumn]; } }
        int CrossingRow, CrossingColumn;
        string OnString;
        Grid grid;
        public float Interval { get; private set; }
        public UpdateMultipleIntervalAction(float interval, Crossing crossing)
        {
            previousIntervals = new List<float>();
            this.grid = crossing.Owner.Grid;
            this.Interval = interval;
            this.CrossingColumn = crossing.Column;
            this.CrossingRow = crossing.Row;
            foreach (Trafficlight light in Lights)
            {
                previousIntervals.Add(light.GreenSeconds);
            }
        }
        protected override string AsString
        {
            get { return OnString; }
        }

        protected override void OnUndo()
        {
            for (int i = 0; i < Lights.Count; i++)
            {
                Lights[i].GreenSeconds = previousIntervals[i];
            }
        }

        protected override void OnRedo()
        {
            foreach (Trafficlight light in Lights)
            {
                if (light.GreenSeconds == Interval) continue;
                light.GreenSeconds = Interval;
            }
            OnString = "interval changes for crossing (" + Crossing.Row + "," + Crossing.Column + ")" + " to " + Interval;
        }
    }
    public static class E
    {
        public static List<float> Variate(this List<Trafficlight> l)
        {
            List<float> variations = new List<float>(); 
            int index=0;
            for (int i = 0; i < l.Count; i++)
            {
                if(i==0)
                {
                    variations.Add(l[i].GreenSeconds);
                    index++;
                    continue;
                }
                for (int a = 0; a < index; a++)
                {
                    if(variations[a]!=l[i].GreenSeconds)
                    {
                        variations.Add(l[i].GreenSeconds);
                        index++;
                    }
                }
            }
            return variations;
        }
    }
}
