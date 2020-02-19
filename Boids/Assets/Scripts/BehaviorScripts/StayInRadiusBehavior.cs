using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/StayInRadius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Vector3 center;
    public float radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<FlockAgent> contextDescription, Flock flock)
    {
        Vector3 centerOffset = center - agent.transform.position;

        float t = centerOffset.magnitude / radius;

        if(t < .9f){
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }
}