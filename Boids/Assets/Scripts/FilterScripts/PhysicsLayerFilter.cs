using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/PhysicsLayer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;
    public override List<Transform> filter(FlockAgent flockAgent, List<Transform> origional, List<FlockAgent> origionalDescription)
    {
        List<Transform> filtered = new List<Transform>();

        foreach(Transform item in origional){
            if(mask == (mask | (1 << item.gameObject.layer))){
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
