using UnityEngine;

public class ShouldFlee : Node
{
    private AI _agent;
    private AgentData _agentData;
    private Sensing _agentSenses;

    public ShouldFlee(AI agent, AgentData agentData, Sensing agentSenses)
    {
        _agent = agent;
        _agentData = agentData;
        _agentSenses = agentSenses;
    }

    public override NodeState Evaluate()
    {
        if (_agentSenses.GetFriendliesInView().Count + 1 < _agentSenses.GetEnemiesInView().Count)
        {
            return NodeState.SUCCESS;
        }
        if (_agentData.CurrentHitPoints <= _agentData.HealthThreshold)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
