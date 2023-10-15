using System;
using System.Collections.Generic;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    [DisallowMultipleComponent]
    public abstract class EvtTrigger : MonoBehaviour
    {
        [HideInInspector]
        [SerializeField] public float zoomLevel;

        public List<EvtNodeData> nodes = new();
        public List<EvtEdgeData> edges = new();

        private bool IsRunning => nodes?.FindAll(n => n is EvtReactionNode reaction && reaction.IsActive).Count > 0;

        protected void Trigger()
        {
            if (IsRunning) return;
            foreach (var node in GetRootNodes()) node.Execute(this);
        }

        private List<EvtNodeData> GetRootNodes()
        {
            var edgeData = edges.FindAll(e => e.OutputId == Guid.Empty);
            return nodes.FindAll(n => edgeData.Exists(e => e.InputId == n.ID));
        }

        public List<EvtNodeData> GetConnectedNodes(EvtNodeData node, string portName)
        {
            var edgeData = edges.FindAll(e => e.OutputId == node.ID && e.portName == portName);
            return nodes.FindAll(n => edgeData.Exists(e => e.InputId == n.ID));
        }
    }
}