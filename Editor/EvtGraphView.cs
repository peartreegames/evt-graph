using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace PeartreeGames.EvtGraph.Editor
{
    public class EvtGraphView : GraphView
    {
        private readonly EvtTrigger _evtTrigger;

        private static readonly Vector2 DefaultNodeSize = new(100, 200);

        private Edge[] Edges => edges.ToArray();
        private EvtNode[] Nodes => nodes.Cast<EvtNode>().ToArray();

        public EvtGraphView(EvtGraphEditor editorWindow, EvtTrigger evtTrigger)
        {
            styleSheets.Add(Resources.Load<StyleSheet>("EvtGraph"));
            _evtTrigger = evtTrigger;

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
            this.AddManipulator(new ContentZoomer());
            graphViewChanged = OnGraphChanged;
            var grid = new GridBackground();
            Insert(0, grid);

            var searchWindow = ScriptableObject.CreateInstance<EvtNodeSearchWindow>();
            searchWindow.Init(editorWindow, this, evtTrigger);
            nodeCreationRequest = ctx =>
                SearchWindow.Open(new SearchWindowContext(ctx.screenMousePosition), searchWindow);
            LoadGraph();

        }

        private GraphViewChange OnGraphChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.edgesToCreate != null)
            {
                foreach (var edge in graphViewChange.edgesToCreate)
                {
                    var outputNode = (EvtNode) edge.output.node;
                    var inputNode = (EvtNode) edge.input.node;
                    var portName = edge.output.portName;
                    if (_evtTrigger.edges.Exists(e =>
                        e.OutputId == outputNode.ID && e.InputId == inputNode.ID && e.portName == portName)) continue;
                    _evtTrigger.edges.Add(new EvtEdgeData()
                    {
                        OutputId = outputNode.ID,
                        portName = portName,
                        InputId = inputNode.ID
                    });
                }
            }

            if (graphViewChange.elementsToRemove != null)
            {
                foreach (var elem in graphViewChange.elementsToRemove)
                {
                    if (elem.GetType() == typeof(EvtNode))
                    {
                        var id = ((EvtNode) elem).ID;
                        _evtTrigger.nodes.RemoveAll(n => n.ID == id);
                        _evtTrigger.edges.RemoveAll(e => e.OutputId == id || e.InputId == id);
                    }
                    if (elem.GetType() != typeof(Edge)) continue;

                    var outputNode = (EvtNode) ((Edge) elem).output.node;
                    var inputNode = (EvtNode) ((Edge) elem).input.node;
                    var port = ((Edge) elem).output.portName;
                    _evtTrigger.edges.RemoveAll(e =>
                        e.OutputId == outputNode.ID && e.InputId == inputNode.ID && e.portName == port);
                }
            }

            if (graphViewChange.movedElements != null)
            {
                foreach (var elem in graphViewChange.movedElements)
                {
                    if (elem.GetType() != typeof(EvtNode)) continue;
                    var node = (EvtNode) elem;
                    if (node.ID == Guid.Empty) continue;
                    var referenceNode = _evtTrigger.nodes.FirstOrDefault(n => n.ID == node.ID);
                    if (referenceNode == null) continue;
                    referenceNode.position = node.GetPosition().position;
                }
            }
            
            EditorUtility.SetDirty(_evtTrigger);
            return graphViewChange;
        }

        private void LoadGraph()
        {
            if (_evtTrigger == null) return;
            ClearGraph();
            var root = CreateRootNode();
            AddElement(root);
            RecreateGraph();
        }

        private void RecreateGraph()
        {
            if (_evtTrigger == null || _evtTrigger.nodes == null) return;
            foreach (var nodeData in _evtTrigger.nodes)
            {
                var node = CreateNode(nodeData);
                if (node == null) continue;
                AddElement(node);
                node.SetPosition(new Rect(nodeData.position, EvtGraphView.DefaultNodeSize));
            }

            var cachedNodes = Nodes;
            foreach (var node in cachedNodes)
            {
                for (var i = 0; i < node.outputContainer.childCount; i++)
                {
                    var port = node.outputContainer[i].Q<Port>();
                    var edgeData = _evtTrigger.edges.Where(e => e.OutputId == node.ID && e.portName == port.portName).ToArray();
                    foreach (var edge in edgeData)
                    {
                        var targetId = edge.InputId;
                        var targetNode = cachedNodes.FirstOrDefault(n => n.ID == targetId);
                        if (targetNode == null) continue;
                        var nodeData = _evtTrigger.nodes.FirstOrDefault(n => n.ID == targetNode.ID);
                        if (nodeData == null) continue;
                        LinkNodes(port, (Port) targetNode.inputContainer[0]);
                    }
                }
            }
        }

        private void ClearGraph()
        {
            foreach(var edge in Edges) RemoveElement(edge);
            foreach(var node in Nodes) RemoveElement(node);
        }

        private EvtNode CreateRootNode()
        {
            var root = new EvtNode()
            {
                title = _evtTrigger.name,
                ID = Guid.Empty
            };
            root.AddToClassList("root");
            var port = CreatePort(root, Direction.Output, Port.Capacity.Multi);
            port.portName = "OnTrigger";
            root.outputContainer.Add(port);
            
            root.RefreshExpandedState();
            root.RefreshPorts();
            root.SetPosition(new Rect(Vector2.one * 100, DefaultNodeSize));
            return root;
        }

        private static Port CreatePort(EvtNode node, Direction direction, Port.Capacity capacity) =>
            node.InstantiatePort(Orientation.Horizontal, direction, capacity, typeof(float));

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter) =>
            ports.Where(port => startPort != port && startPort.node != port.node).ToList();

        private void LinkNodes(Port output, Port input)
        {
            var edge = new Edge()
            {
                output = output,
                input = input
            };
            edge.input.Connect(edge);
            edge.output.Connect(edge);
            Add(edge);
        }

        public EvtNode CreateNode(EvtNodeData data)
        {
            if (data == null) return null;
            var node = new EvtNode()
            {
                ID = data.ID
            };

            switch (data)
            {
                case EvtConditionNode condition:
                    CreateConditionNode(node, condition);
                    break;
                case EvtReactionNode reaction:
                    CreateReactionNode(node, reaction);
                    break;
            }

            var input = CreatePort(node, Direction.Input, Port.Capacity.Multi);
            input.portName = "Input";
            node.inputContainer.Add(input);
            node.RefreshExpandedState();
            node.RefreshPorts();
            node.SetPosition(new Rect(data.position, DefaultNodeSize));
            AddElement(node);
            return node;
        }

        private void CreateReactionNode(EvtNode node, EvtReactionNode reaction)
        {
            var choices = CreateDropDown<EvtReactionNode, EvtReaction>();
            choices.choices.Insert(0, "Select Reaction");
            choices.index = 0;
            node.title = "Reaction";
            node.extensionContainer.Add(choices);
            var output = CreatePort(node, Direction.Output, Port.Capacity.Multi);
            output.portName = EvtReactionNode.OnCompletePortName;
            node.outputContainer.Add(output);
            
            var serialized = new SerializedObject(reaction);
            var reactions = serialized.FindProperty("items");
            choices.RegisterValueChangedCallback(change =>
            {
                if (CreateInstanceFromDropdown<EvtReactionNode, EvtReaction>(change.newValue) is not EvtReaction react) return;
                react.name = change.newValue;
                choices.SetValueWithoutNotify("Select Reaction");
                reactions.arraySize++;
                reactions.GetArrayElementAtIndex(reactions.arraySize - 1).objectReferenceValue = react;
                serialized.ApplyModifiedProperties();
                var box = CreatePropertyBox(node, serialized, reactions, reactions.arraySize - 1);
                box.Q<Foldout>().value = true;
                node.extensionContainer.Add(box);
                node.RefreshExpandedState();
            });
            
            CreatePropertyBoxes(node, serialized, reactions);

            node.extensionContainer.style.borderBottomWidth = reaction.isActive ? 5 : 0;
            reaction.OnActive += enabled => node.extensionContainer.style.borderBottomWidth = enabled ? 5 : 0;
        }
        

        private void CreateConditionNode(EvtNode node, EvtConditionNode condition)
        {
            var choices = CreateDropDown<EvtConditionNode, EvtCondition>();
            choices.choices.Insert(0, "Select Condition");
            choices.index = 0;
            node.title = "Condition";
            node.extensionContainer.Add(choices);

            var truePort = CreatePort(node, Direction.Output, Port.Capacity.Multi);
            truePort.portName = EvtConditionNode.TruePortName;
            node.outputContainer.Add(truePort);
            var falsePort = CreatePort(node, Direction.Output, Port.Capacity.Multi);
            falsePort.portName = EvtConditionNode.FalsePortName;
            node.outputContainer.Add(falsePort);

            var serialized = new SerializedObject(condition);
            var conditions = serialized.FindProperty("items");
            choices.RegisterValueChangedCallback(change =>
            {
                if (CreateInstanceFromDropdown<EvtConditionNode, EvtCondition>(change.newValue) is not EvtCondition cond) return;
                cond.name = change.newValue;
                choices.SetValueWithoutNotify("Select Condition");
                conditions.arraySize++;
                conditions.GetArrayElementAtIndex(conditions.arraySize - 1).objectReferenceValue = cond;
                serialized.ApplyModifiedProperties();
                var box = CreatePropertyBox(node, serialized, conditions, conditions.arraySize - 1);
                box.Q<Foldout>().value = true;
                node.extensionContainer.Add(box);
                node.RefreshExpandedState();
            });

            CreatePropertyBoxes(node, serialized, conditions);
        }

        private void CreatePropertyBoxes(EvtNode node, SerializedObject serializedObject, SerializedProperty serializedProperty)
        {
            if (serializedProperty == null) return;
            for (int i = 0; i < serializedProperty.arraySize; i++)
            {
                var box = CreatePropertyBox(node, serializedObject, serializedProperty, i);
                if (box == null) continue;
                node.extensionContainer.Add(box);
            }
        }

        private static GroupBox CreatePropertyBox(EvtNode node, SerializedObject serializedObject, SerializedProperty serializedProperty, int i)
        {
            var obj = serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue;
            if (obj is null) return null;
            var box = new GroupBox();
            box.AddToClassList("property-box");
            var foldOut = new Foldout()
            {
                text = obj.GetType().GetProperty("DisplayName")?.GetValue(null).ToString() ?? obj.GetType().Name
            };
            foldOut.contentContainer.AddToClassList("property-foldout");
            var serializedProp = new SerializedObject(obj);
            var itr = serializedProp.GetIterator();
            if (itr.NextVisible(true))
            {
                do
                {
                    if (itr.name == "m_Script") continue;
                    var field = new PropertyField(itr);
                    field.Bind(serializedProp);
                    foldOut.contentContainer.Add(field);
                } while (itr.NextVisible(false));
            }
            foldOut.contentContainer.Add(new Button(() =>
            {
                serializedProperty.DeleteArrayElementAtIndex(i);
                serializedObject.ApplyModifiedProperties();
                node.extensionContainer.Remove(box);
            }) { text = "x" });
            box.Add(foldOut);
            foldOut.value = serializedProp.FindProperty("isExpanded").boolValue;
            foldOut.BindProperty(serializedProp.FindProperty("isExpanded"));
            return box;
        }

        private static DropdownField CreateDropDown<T, TU>() where T : EvtNodeData where TU : EvtNodeItemData =>
            new()
            {
                choices = typeof(T).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(TU)))
                    .Select(t => t.GetProperty("DisplayName")?.GetValue(null).ToString() ?? t.Name).ToList(),
                label = ""
                
            };

        private static ScriptableObject CreateInstanceFromDropdown<T, TU>(string str) where T : EvtNodeData where TU : EvtNodeItemData
        {
            var choices = typeof(T).Assembly.GetTypes().Where(t => !t.IsAbstract && t.IsSubclassOf(typeof(TU)))
                .Select(t => (display: t.GetProperty("DisplayName")?.GetValue(null).ToString() ?? t.Name, value: t.Name)).ToList();
            var choice = choices.Find(choice => choice.display == str);
            return ScriptableObject.CreateInstance(choice.value);
        }
    }
}























