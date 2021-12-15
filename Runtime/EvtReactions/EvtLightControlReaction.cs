using System.Collections;
using UnityEngine;

namespace PeartreeGames.EvtGraph
{
    public class EvtLightControlReaction : EvtReaction
    {
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