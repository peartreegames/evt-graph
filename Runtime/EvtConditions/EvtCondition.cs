using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public abstract class EvtCondition : EvtNodeItemData
    {
        [HideInInspector] [SerializeField] private bool isExpanded = true;
        public abstract bool CheckIsSatisfied(EvtTrigger trigger);
    }
}