﻿using System.Collections;
using UnityEngine;
using PeartreeGames.EvtVariables;

namespace PeartreeGames.EvtGraph
{
    public class EvtBoolObjectReaction : EvtReaction
    {
        [SerializeField] private EvtBoolObject variable;
        [SerializeField] private bool value;
        public override IEnumerator React(EvtTrigger trigger)
        {
            variable.Value = value;
            yield break;
        }
    }
}