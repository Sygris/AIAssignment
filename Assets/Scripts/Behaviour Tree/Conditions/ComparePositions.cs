using UnityEngine;

/// <summary>
/// Compares if two positions are on the same location, e.g. is the flag in base currently
/// </summary>
public class ComparePositions : Node
{
    private AI _agent;
    private GameObject _target;
    private float _tolerance;

    public ComparePositions(AI agent, GameObject targetPosition = null, float tolerance = 1.0f)
    {
        _agent = agent;
        _target = targetPosition;
        _tolerance = tolerance;
    }

    public override NodeState Evaluate()
    {
        GameObject tmp = Util.DetermineTarget(_agent, ref _target, TargetTypes.FLAG);

        if (Vector3.Distance(_agent.transform.position, tmp.transform.position) > _tolerance)
        {
            SetState(NodeState.FAILURE);
            return _nodeState;
        }

        SetState(NodeState.SUCCESS);
        return _nodeState;
    }
}
