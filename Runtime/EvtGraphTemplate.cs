using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtGraphTemplate : ScriptableObject
    {
        [SerializeField]
        private List<EvtNodeData> nodes;

        [Serializable]
        private class EvtTemplateEdge
        {
            public int outputIndex;
            public int inputIndex;
            public string portName;
        }
        
        [SerializeField]
        private List<EvtTemplateEdge> edges;

        public void Save(EvtTrigger evtTrigger)
        {
            nodes = new List<EvtNodeData>();
            var previous = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(this));
            foreach (var prev in previous)
            {
                if (AssetDatabase.IsSubAsset(prev)) AssetDatabase.RemoveObjectFromAsset(prev);
            }
            
            foreach (var node in evtTrigger.nodes)
            {
                var obj = CreateInstance(node.GetType().Name) as EvtNodeData;
                obj.position = node.position;
                obj.ID = Guid.Empty;
                AssetDatabase.AddObjectToAsset(obj, this);
                switch (node)
                {
                    case EvtConditionNode cond:
                    {
                        var condObj = (EvtConditionNode) obj;
                        condObj.items = new List<EvtCondition>();
                        foreach (var item in cond.items)
                        {
                            var cName = item.GetType().Name;
                            var c = CreateInstance(cName) as EvtCondition;
                            c.name = cName;
                            AssetDatabase.AddObjectToAsset(c, this);
                            condObj.items.Add(c);
                            
                        }
                        break;
                    }
                    case EvtReactionNode react:
                    {
                        var reactObj = (EvtReactionNode) obj;
                        reactObj.items = new List<EvtReaction>();
                        foreach (var item in react.items)
                        {
                            var rName = item.GetType().Name;
                            var r = CreateInstance(item.GetType().Name) as EvtReaction;
                            r.name = rName;
                            AssetDatabase.AddObjectToAsset(r, this);
                            reactObj.items.Add(r);
                        }
                        break;
                    }
                }
                nodes.Add(obj);
            }
            edges = evtTrigger.edges.Select(edge => new EvtTemplateEdge()
            {
                outputIndex = evtTrigger.nodes.FindIndex(n => n.ID == edge.OutputId),
                inputIndex = evtTrigger.nodes.FindIndex(n => n.ID == edge.InputId),
                portName = edge.portName
            }).ToList();
        }

        public void Load(EvtTrigger evtTrigger)
        {
            evtTrigger.nodes.Clear();
            foreach (var node in nodes)
            {
                var obj = CreateInstance(node.GetType().Name) as EvtNodeData;
                obj.position = node.position;
                obj.ID = Guid.NewGuid();
                switch (node)
                {
                    case EvtConditionNode cond:
                    {
                        var condObj = (EvtConditionNode) obj;
                        condObj.items = new List<EvtCondition>();
                        foreach (var item in cond.items.Where(item => item != null))
                        {
                            condObj.items.Add(CreateInstance(item.GetType().Name) as EvtCondition);
                        }
                        break;
                    }
                    case EvtReactionNode react:
                    {
                        var reactObj = (EvtReactionNode) obj;
                        reactObj.items = new List<EvtReaction>();
                        foreach (var item in react.items.Where(item => item != null))
                            reactObj.items.Add(CreateInstance(item.GetType().Name) as EvtReaction);
                        break;
                    }
                }
                evtTrigger.nodes.Add(obj);
            }
            evtTrigger.edges = edges.Select(edge => new EvtEdgeData()
            {
                OutputId = edge.outputIndex < 0 ? Guid.Empty : evtTrigger.nodes[edge.outputIndex].ID,
                InputId = evtTrigger.nodes[edge.inputIndex].ID,
                portName = edge.portName
            }).ToList();
        }
    }
}