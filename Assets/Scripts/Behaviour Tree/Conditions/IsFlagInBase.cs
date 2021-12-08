using UnityEngine;

public class IsFlagInBase : Node
{
    private GameObject _base;
    private GameObject _flag;

    public IsFlagInBase(GameObject @base, GameObject flag)
    {
        _base = @base;
        _flag = flag;
    }

    public override NodeState Evaluate()
    {
        // Checks if the flag collider is colliding with the base's collider
        bool isColliding = _base.GetComponent<BoxCollider>().bounds.Intersects(_flag.GetComponent<BoxCollider>().bounds);

        return isColliding ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}
