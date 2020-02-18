using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, do not adjust
        if(context.Count == 0){
            return agent.transform.forward;
        }else{
            // set to average
            Vector3 alignmentMove = Vector3.zero;
            foreach(Transform item in context){
                alignmentMove += item.forward;
            }
    
            alignmentMove /= context.Count;

            return alignmentMove;
        }
    }
}
