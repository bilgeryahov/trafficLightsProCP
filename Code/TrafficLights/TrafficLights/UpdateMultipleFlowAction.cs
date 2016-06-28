using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    public class UpdateMultipleFlowAction:UndoableAction
    {
        List<Lane> Lanes { get { return Crossing.Lanes.ToList(); } }
        private List<int> previousFlows;
        string OnString;
        public int Flow { get; set; }
        public Crossing Crossing { get {return grid[CrossingRow][CrossingColumn];}}
        int CrossingRow, CrossingColumn;
        Grid grid;

        public UpdateMultipleFlowAction(int flow,Crossing crossing)
        {
            previousFlows = new List<int>();
            this.Flow = flow;
            this.grid = crossing.Owner.Grid;
            this.CrossingColumn = crossing.Column;
            this.CrossingRow = crossing.Row;

            foreach (Lane lane in Lanes)
            {
                previousFlows.Add(lane.Flow);
                //Lane is of the crossing before it was deleted
                //lane  has to be using the ID that is in the crossing, not the direct reference
                
            }
        }
        protected override void OnUndo()
        {
            for (int i = 0; i < Lanes.Count; i++)
            {
                Lanes[i].UpdateFlow(previousFlows[i]);
            }
        }

        protected override void OnRedo()
        {
            foreach (Lane lane in this.Lanes)
            {
                if (lane.Flow == Flow) continue;
                if (!lane.IsFeeder) continue;
                lane.UpdateFlow(Flow);
            }
            OnString = "flow changes for crossing (" + Crossing.Row + "," + Crossing.Column + ")" + " to " + Flow;
        }

        protected override string AsString
        {
            get { return OnString; }
        }
    }
}
