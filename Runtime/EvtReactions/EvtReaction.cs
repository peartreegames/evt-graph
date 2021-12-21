using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public abstract class EvtReaction : EvtNodeItemData
    {
#pragma warning disable 0414
        [HideInInspector] [SerializeField] private bool isExpanded = true;
#pragma warning restore 0414
        public abstract IEnumerator React(EvtTrigger trigger);
    }
}