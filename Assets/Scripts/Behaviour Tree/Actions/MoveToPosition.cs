using UnityEngine;

public class MoveToPosition : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private GameObject _target;
    private TargetTypes _targetType;

    // Tolerance to target position the agent has to reach, so the agent does not need to be exactly at the target's position
    private float _tolerance;

    public MoveToPosition(AI agent, AgentActions actions, GameObject target = null, float tolerance = 1.0f, TargetTypes targetType = TargetTypes.FLAG)
    {
        _agent = agent;
        _target = target;
        _agentActions = actions;
        _tolerance = tolerance;
        _targetType = targetType;
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        GameObject tmp = Util.DetermineTarget(_agent, _target, _targetType);

        if (tmp == null) return NodeState.FAILURE;

        _agentActions.MoveTo(tmp);

        if (Vector3.Distance(_agent.transform.position, tmp.transform.position) <= _tolerance)
        {
            SetState(NodeState.SUCCESS);
        }

        return _nodeState;
    }
}