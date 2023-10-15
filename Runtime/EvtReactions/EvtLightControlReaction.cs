using System.Collections;
using UnityEngine;

namespace PeartreeGames.Evt.Graph
{
    public class EvtLightControlReaction : EvtReaction
    {
        public new static string DisplayName => "Light/Control";
        [SerializeField] private Light light;
        [SerializeField] private Color color;
        [SerializeField] private float intensity;
        public override IEnumerator React(EvtTrigger trigger)
        {
            light.color = color;
            light.intensity = intensity;
            yield break;
        }
    }
}