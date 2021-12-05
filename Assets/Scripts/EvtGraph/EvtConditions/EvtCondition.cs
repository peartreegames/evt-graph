namespace EvtGraph
{
    public abstract class EvtCondition : EvtNodeItemData
    {
        public abstract bool CheckIsSatisfied(EvtTrigger trigger);
    }
}