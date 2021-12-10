using System.Collections.Generic;
using UnityEngine;

public class IsAnyFlagOutsideFriendlyBase : Node
{
    private GameObject _base;
    private AI _ai;
    private AgentData _agentData;
    private Blackboard _blackboard;

    private List<GameObject> _listOfFlagsOutsideBase = new List<GameObject>();

    public IsAnyFlagOutsideFriendlyBase(GameObject @base, AI ai, AgentData agentData, Blackboard blackboard)
    {
        _base = @base;
        _ai = ai;
        _agentData = agentData;
        _blackboard = blackboard;
    }

    public override NodeState Evaluate()
    {
        if (AreFlagsOutsideBase())
        {
            // Checks if the friendly flag exists in the list and if so make it the priority flag
            // If not make the enemy flag the priority
            if (_ai.Blackboard.GetData("PriorityFlag") == null)
            {
                if (_listOfFlagsOutsideBase.Contains((GameObject)_blackboard.GetData(_agentData.FriendlyFlagName)))
                    _ai.Blackboard.ModifyData("PriorityFlag", _blackboard.GetData(_agentData.FriendlyFlagName));
                else
                    _ai.Blackboard.ModifyData("PriorityFlag", (GameObject)_blackboard.GetData(_agentData.EnemyFlagName));
                //_ai.Blackboard.GetData("PriorityFLag") = (GameObject)_blackboard.GetData(_agentData.EnemyFlagName);
            }
            return NodeState.SUCCESS;
        }

        return NodeState.FAILURE;
    }

    private bool AreFlagsOutsideBase()
    {
        // Empties the list
        _listOfFlagsOutsideBase.Clear();

        foreach (var flag in Object.FindObjectsOfType<Flag>())
        {
            bool isColliding = _base.GetComponent<BoxCollider>().bounds.Intersects(flag.GetComponent<BoxCollider>().bounds);

            if (!isColliding)
            {
                // Adds the flag to the list if the flag is outside of the friendly base
                _listOfFlagsOutsideBase.Add(flag.gameObject);
            }
        }

        return _listOfFlagsOutsideBase.Count > 0;
    }
}
