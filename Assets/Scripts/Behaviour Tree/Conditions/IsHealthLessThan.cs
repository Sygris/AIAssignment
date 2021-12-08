using UnityEngine;

public class IsHealthLessThan : Node
{
    private float _value;
    private AgentData _agentData;

    public IsHealthLessThan(float value, AgentData agentData)
    {
        _value = value;
        _agentData = agentData;
    }

    public override NodeState Evaluate()
    {
        if (_agentData.CurrentHitPoints < _value)
        {
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }
}
