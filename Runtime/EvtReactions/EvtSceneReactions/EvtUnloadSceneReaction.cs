using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeartreeGames.EvtGraph
{
    public class EvtUnloadSceneReaction : EvtReaction
    {
        public new static string DisplayName => "Scene/Unload";
        [SerializeField] private string scene;
        public override IEnumerator React(EvtTrigger trigger)
        {
            yield return SceneManager.UnloadSceneAsync(scene);
        }
    }
}