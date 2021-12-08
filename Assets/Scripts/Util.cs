using UnityEngine;

public static class Util
{
    public static GameObject DetermineTarget(AI agent, ref GameObject gameObject, TargetTypes targetTypes)
    {
        GameObject tmp = null;

        if (gameObject == null)
        {
            switch (targetTypes)
            {
                case TargetTypes.FLAG:
                    tmp = agent.PriorityFlag;
                    break;
                case TargetTypes.ENEMY:
                    tmp = agent.ClosestEnemy;
                    break;
                default:
                    break;
            }
        }
        else
        {
            tmp = gameObject;
        }

        return tmp;
    }
}

public enum TargetTypes { FLAG, ENEMY }
