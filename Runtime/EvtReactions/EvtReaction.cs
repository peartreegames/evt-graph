using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public abstract class EvtReaction : EvtNodeItemData
    {
        [HideInInspector] [SerializeField] private bool isExpanded = true;
        public abstract IEnumerator React(EvtTrigger trigger);
    }
}