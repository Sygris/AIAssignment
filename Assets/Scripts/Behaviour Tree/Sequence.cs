using System.Collections.Generic;

/// <summary>
/// If any child node returns a failure, the entire node fails. Whence all nodes return a success, the node reports a success.
/// </summary>
public class Sequence : Node
{
    /// <summary>
    /// Children node that bnelongs to this sequence
    /// </summary>
    protected List<Node> _nodes = new List<Node>();
    
    /// <summary>
    /// Default Constractor
    /// </summary>
    public Sequence() { }

    /// <summary>
    /// Constractor that recveives a list of child nodes
    /// </summary>
    /// <param name="nodes">List with all the child nodes</param>
    public Sequence(List<Node> nodes) => _nodes = nodes;

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
                    SetState(NodeState.FAILURE);
                    return _nodeState;
                case NodeState.RUNNING:
                    SetState(NodeState.RUNNING);
                    return _nodeState;
            }
        }

        SetState(NodeState.SUCCESS);
        return _nodeState;
    }
}
