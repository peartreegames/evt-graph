namespace EvtGraph
{
    public class EvtConditionNode : EvtNodeData<EvtCondition>
    {
        public const string TruePortName = "True";
        public const string FalsePortName = "False";
        private bool CheckIsSatisfied(EvtTrigger trigger) =>
            items.TrueForAll(condition => condition.CheckIsSatisfied(trigger));
        public override void Execute(EvtTrigger trigger)
        {
            var connections = trigger.GetConnectedNodes(this, CheckIsSatisfied(trigger) ? TruePortName : FalsePortName);
            foreach(var connection in connections) connection.Execute(trigger);
        }
    }
}