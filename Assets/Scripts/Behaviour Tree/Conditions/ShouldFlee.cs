public class ShouldFlee : Node
{
    private AgentData _agentData;
    private Sensing _agentSenses;

    public ShouldFlee(AgentData agentData, Sensing agentSenses)
    {
        _agentData = agentData;
        _agentSenses = agentSenses;
    }

    public override NodeState Evaluate()
    {
        if (_agentData.HasEnemyFlag || _agentData.HasFriendlyFlag)
        {
            return NodeState.SUCCESS;
        }

        if (_agentData.CurrentHitPoints <= _agentData.HealthThreshold)
        {
            return NodeState.SUCCESS;
        }

        // If there are more enemies than allies + AI
        if (_agentSenses.GetFriendliesInView().Count + 1 < _agentSenses.GetEnemiesInView().Count)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
