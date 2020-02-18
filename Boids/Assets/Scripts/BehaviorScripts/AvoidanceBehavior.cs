using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, do not adjust
        if(context.Count == 0){
            return Vector3.zero;
        }else{
            // set to average
            Vector3 avoidanceMove = Vector3.zero;
            int navoid = 0;
            foreach(Transform item in context)
            {
                if(Vector3.SqrMagnitude(item.position-agent.transform.position) < flock.SquareAvoidanceRadius)
                {
                    navoid++;
                    avoidanceMove += agent.transform.position - item.position;
                }
            }
    
            if(navoid > 0){
                avoidanceMove /= navoid;
            }

            return avoidanceMove;
        }
    }   
}
