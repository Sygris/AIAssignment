using UnityEngine;

/// <summary>
/// Compares if two positions are on the same location, e.g. is the flag in base currently
/// </summary>
public class ComparePositions : Node
{
    private GameObject _positionA;
    private GameObject _positionB;
    private float _tolerance;

    public ComparePositions(GameObject positionA, GameObject positionB, float tolerance = 1.0f)
    {
        _positionA = positionA;
        _positionB = positionB;
        _tolerance = tolerance;
    }

    public override NodeState Evaluate()
    {
        if (Vector3.Distance(_positionA.transform.position, _positionB.transform.position) > _tolerance)
        {
            SetState(NodeState.FAILURE);
            return _nodeState;
        }

        SetState(NodeState.SUCCESS);
        return _nodeState;
    }
}
