﻿using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtDebugReaction : EvtReaction
    {
        [SerializeField] private string message;
        public override IEnumerator React(EvtTrigger trigger)
        {
            Debug.Log(message);
            yield break;
        }
    }
}