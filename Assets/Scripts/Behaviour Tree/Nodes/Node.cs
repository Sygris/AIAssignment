/// <summary>
/// Enum that contains the three different states that a node can be
/// </summary>
public enum NodeState { SUCCESS, FAILURE, RUNNING };

/// <summary>
/// Blueprint for all the other nodes
/// </summary>
public abstract class Node
{
    protected NodeState _nodeState;

    /// <summary>
    /// Returns the current state of the node
    /// </summary>
    public NodeState GetState() => _nodeState;

    /// <summary>
    /// Sets the node's state to the one passed in the arguments
    /// </summary>
    /// <param name="newState">New state</param>
    public void SetState(NodeState newState) => _nodeState = newState;

    /// <summary>
    /// Method that determines the state of the node.
    /// </summary>
    /// <returns></returns>
    public abstract NodeState Evaluate();
}
