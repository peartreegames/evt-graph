using System;
using System.Collections;

namespace PeartreeGames.EvtGraph
{
    public class EvtReactionNode : EvtNodeData<EvtReaction>
    {
        public bool isActive;
#if UNITY_EDITOR
        public event Action<bool> OnActive;
#endif
        
        public const string OnCompletePortName = "OnComplete";

        public override void Execute(EvtTrigger trigger)
        {
            trigger.StartCoroutine(React(trigger));
        }

        private IEnumerator React(EvtTrigger trigger)
        {
            isActive = true;
#if UNITY_EDITOR
            OnActive?.Invoke(isActive);
#endif
            var coroutines = items.ConvertAll(reaction => trigger.StartCoroutine(reaction.React(trigger)));
            foreach (var coroutine in coroutines) yield return coroutine;
            var connections = trigger.GetConnectedNodes(this, OnCompletePortName);
            foreach (var connection in connections) connection.Execute(trigger);
            isActive = false;
#if UNITY_EDITOR
            OnActive?.Invoke(isActive);
#endif
        }
        
    }
}