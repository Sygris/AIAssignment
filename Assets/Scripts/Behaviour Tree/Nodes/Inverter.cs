/// <summary>
/// This decorator will invert the result of a note
/// </summary>
public class Inverter : Node
{
    private Node _node;

    public Inverter(Node node)
    {
        _node = node;
    }

    public override NodeState Evaluate()
    {
        switch (_node.Evaluate())
        {
            case NodeState.SUCCESS:
                _nodeState = NodeState.FAILURE;
                break;
            case NodeState.FAILURE:
                _nodeState = NodeState.SUCCESS;
                return _nodeState;
            case NodeState.RUNNING: //Does not invert if the node is running
                _nodeState = NodeState.RUNNING;
                break;
            default:
                break;
        }

        return _nodeState;
    }
}
