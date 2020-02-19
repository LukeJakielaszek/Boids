using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Flock/Behavior/Cohesion")]
public class Cohesion : FilteredFlockBehavior
{
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

            return cohesionMove;
        }
    }
}
