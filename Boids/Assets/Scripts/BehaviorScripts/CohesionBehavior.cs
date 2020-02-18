using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Flock/Behavior/Cohesion")]
public class Cohesion : FlockBehavior
{
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, do not adjust
        if(context.Count == 0){
            return Vector3.zero;
        }else{
            // set to average
            Vector3 cohesionMove = Vector3.zero;
            foreach(Transform item in context){
                cohesionMove += item.position;
            }
    
            cohesionMove /= context.Count;

            cohesionMove -= agent.transform.position;

            return cohesionMove;
        }
    }
}
