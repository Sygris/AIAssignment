using System.Collections.Generic;
using UnityEngine;

public class IsCarryingFlag : Node
{
    private InventoryController _agentInventory;
    private List<GameObject> _listOfFlags = new List<GameObject>();
    
    public IsCarryingFlag(InventoryController agentInventory)
    {
        _agentInventory = agentInventory;

        // Loops through every flag in the scene and adds it to the list
        foreach (var flag in Object.FindObjectsOfType<Flag>())
        {
            _listOfFlags.Add(flag.gameObject);
        }
    }

    public override NodeState Evaluate()
    {
        // Loops through the list of flags and if the player has a flag it returns true 
        foreach (var flag in _listOfFlags)
        {
            if (_agentInventory.HasItem(flag.name))
            {
                return NodeState.SUCCESS;
            }
        }

        return NodeState.FAILURE;
    }
}
