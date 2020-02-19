using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{
    Vector3 currentVelocity;
    public float AgentSmoothTime = .5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<FlockAgent> contextDescription, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context, contextDescription);
        // if no neighbors, do not adjust
        if(context.Count == 0 || (filter != null && filteredContext.Count == 0)){
            return Vector3.zero;
        }else{
            // set to average
            Vector3 cohesionMove = Vector3.zero;
            foreach(Transform item in filteredContext){
                cohesionMove += item.position;
            }
    
            cohesionMove /= filteredContext.Count;

            cohesionMove -= agent.transform.position;

            if(float.IsNaN(currentVelocity.x) || float.IsNaN(currentVelocity.y) || float.IsNaN(currentVelocity.z)){
                currentVelocity = Vector3.zero;
            }
            
            cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, AgentSmoothTime);

            return cohesionMove;
        }
    }
}
