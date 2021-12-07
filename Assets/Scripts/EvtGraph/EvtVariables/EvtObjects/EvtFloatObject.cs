using UnityEngine;

namespace EvtGraph
{
    [CreateAssetMenu(fileName = "float_", menuName = "Evt/FloatObject", order = 0)]
    public class EvtFloatObject : EvtObject<float>
    {
        [SerializeField] private EvtFloatVariable floatVariable;
        protected override EvtVariable<float> Variable => floatVariable;
    }
}