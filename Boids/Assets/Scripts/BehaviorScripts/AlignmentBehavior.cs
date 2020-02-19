using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<FlockAgent> contextDescription, Flock flock)
    {
        List<Transform> filteredContext = (filter == null) ? context : filter.filter(agent, context, contextDescription);
        // if no neighbors, do not adjust
        if(context.Count == 0 || (filteredContext != null && filteredContext.Count == 0)){
            return agent.transform.forward;
        }else{
            // set to average
            Vector3 alignmentMove = Vector3.zero;
            foreach(Transform item in filteredContext){
                alignmentMove += item.forward;
            }
    
            alignmentMove /= filteredContext.Count;

            return alignmentMove;
        }
    }
}
