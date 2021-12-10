using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreBothFlagsInFriendlyBase : Node
{
    private GameObject _base;
    private AI _ai;
    private AgentData _agentData;
    private Blackboard _blackboard;

    private List<GameObject> _listOfFlagsInsideOfBase = new List<GameObject>();

    public AreBothFlagsInFriendlyBase(GameObject @base, AI ai, AgentData agentData, Blackboard blackboard)
    {
        _base = @base;
        _ai = ai;
        _agentData = agentData;
        _blackboard = blackboard;
    }

    public override NodeState Evaluate()
    {
        return AreBothFlagsInsideBase() ? NodeState.SUCCESS : NodeState.FAILURE;
    }

    private bool AreBothFlagsInsideBase()
    {
        // Empties the list
        _listOfFlagsInsideOfBase.Clear();

        int numOfFlags = GameObject.FindObjectsOfType<Flag>().Length;

        foreach (var flag in Object.FindObjectsOfType<Flag>())
        {
            bool isColliding = _base.GetComponent<BoxCollider>().bounds.Intersects(flag.GetComponent<BoxCollider>().bounds);

            if (isColliding)
            {
                // Adds the flag to the list if the flag is outside of the friendly base
                _listOfFlagsInsideOfBase.Add(flag.gameObject);
            }
        }

        Debug.Log(_listOfFlagsInsideOfBase.Count == numOfFlags);
        return _listOfFlagsInsideOfBase.Count == numOfFlags;
    }
}
