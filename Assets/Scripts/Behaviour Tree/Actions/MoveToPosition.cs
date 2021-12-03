using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : Node
{
    private AI _agent;
    private AgentActions _agentActions;
    private GameObject _target;

    // Tolerance to target position the agent has to reach, so the agent does not need to be exactly at the target's position
    private float _tolerance;

    public MoveToPosition(AI agent, AgentActions actions, GameObject target, float tolerance)
    {
        _agent = agent;
        _agentActions = actions;
        _target = target;
        _tolerance = tolerance;
    }

    public override NodeState Evaluate()
    {
        SetState(NodeState.RUNNING);

        _agentActions.MoveTo(_target);

        if (Vector3.Distance(_agent.transform.position, _target.transform.position) > _tolerance)
        {
            Debug.Log("Far away from my G");
        }
        else
        {
            SetState(NodeState.SUCCESS);
            Debug.Log("<color=green>Reached </color>" + _target.name);
        }

        return _nodeState;
    }
}
