using map_encounter_system.map_system.UI.line;
using UnityEngine;

namespace map_encounter_system.map_system.UI
{
    [System.Serializable]
    public class LineConnection
    {
        private LineRenderer lr;
        private UILineRenderer uilr;
        public NodeView from;
        public NodeView to;

        public LineConnection(UILineRenderer uilr,LineRenderer lr, NodeView from, NodeView to)
        {
            this.uilr = uilr;
            this.lr = lr;
            this.from = from;
            this.to = to;
        }
    }
}