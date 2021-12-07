using UnityEngine;

namespace EvtGraph
{
    [CreateAssetMenu(fileName = "bool_", menuName = "Evt/BoolObject", order = 0)]
    public class EvtBoolObject : EvtObject<bool>
    {
        [SerializeField] private EvtBoolVariable boolVariable;
        protected override EvtVariable<bool> Variable => boolVariable;
    }
}