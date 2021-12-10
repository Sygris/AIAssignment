using System.Collections.Generic;

public class Selector : Node
{
    /// <summary>
    /// Children node that bnelongs to this sequence
    /// </summary>
    protected List<Node> _nodes = new List<Node>();

    /// <summary>
    /// Default Constractor
    /// </summary>
    public Selector() { }

    /// <summary>
    /// Constractor that recveives a list of child nodes
    /// </summary>
    /// <param name="nodes">List with all the child nodes</param>
    public Selector(List<Node> nodes) => _nodes = nodes;

    /// <summary>
    /// Adds a child to the list of child nodes
    /// </summary>
    /// <param name="child">Chiold node to be added to the list</param>
    public void AddChild(Node child) => _nodes.Add(child);

    public override NodeState Evaluate()
    {
        foreach (var node in _nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.FAILURE:
                    continue;
                case NodeState.SUCCESS:
                    _nodeState = NodeState.SUCCESS;
                    return _nodeState;
                case NodeState.RUNNING:
                    _nodeState = NodeState.RUNNING;
                    return _nodeState;
                default:
                    continue;
            }
        }

        _nodeState = NodeState.FAILURE;
        return _nodeState;
    }
}
