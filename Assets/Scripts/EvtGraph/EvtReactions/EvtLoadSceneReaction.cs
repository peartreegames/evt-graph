﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EvtGraph
{
    public class EvtLoadSceneReaction : EvtReaction
    {
        [SerializeField] private LoadSceneMode mode;
        [SerializeField] private string sceneName;
        public override IEnumerator React(EvtTrigger trigger)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, mode);
        }
    }
}