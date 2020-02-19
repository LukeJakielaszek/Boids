using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior
{
    Vector3 currentVelocity;
    public float AgentSmoothTime = .5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<FlockAgent> contextDescription, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context, contextDescription);
        // if no neighbors, do not adjust
        if(context.Count == 0 || (filteredContext != null && filteredContext.Count == 0)){
            return Vector3.zero;
        }else{
            // set to average
            Vector3 avoidanceMove = Vector3.zero;
            int navoid = 0;
            foreach(Transform item in filteredContext)
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

            if(float.IsNaN(currentVelocity.x) || float.IsNaN(currentVelocity.y) || float.IsNaN(currentVelocity.z)){
                currentVelocity = Vector3.zero;
            }

            avoidanceMove = Vector3.SmoothDamp(agent.transform.forward, avoidanceMove, ref currentVelocity, AgentSmoothTime);

            return avoidanceMove;
        }
    }   
}
