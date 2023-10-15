using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PeartreeGames.Evt.Graph
{
    public class EvtLoadSceneReaction : EvtReaction
    {
        public new static string DisplayName => "Scene/Load";
        [SerializeField] private LoadSceneMode mode;
        [SerializeField] private string sceneName;
        public override IEnumerator React(EvtTrigger trigger)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, mode);
        }
    }
}