using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficLights
{
    public class UpdateMultipleFlowAction:UndoableAction
    {
        List<Lane> Lanes = new List<Lane>();
        private List<int> previousFlows;
        string OnString;
        public int Flow { get; set; }
        public Crossing Crossing { get; set; }

        public UpdateMultipleFlowAction(int flow,Crossing crossing)
        {
            previousFlows = new List<int>();
            this.Flow = flow;
            this.Crossing = crossing;
            this.Lanes = crossing.Lanes.ToList();
            foreach (Lane lane in Lanes)
            {
                previousFlows.Add(lane.Flow);
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
