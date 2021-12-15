using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeartreeGames.EvtGraph
{
    public class EvtUnloadSceneReaction : EvtReaction
    {
        [SerializeField] private string scene;
        public override IEnumerator React(EvtTrigger trigger)
        {
            yield return SceneManager.UnloadSceneAsync(scene);
        }
    }
}