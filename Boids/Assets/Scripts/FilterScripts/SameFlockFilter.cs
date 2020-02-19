using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> filter(FlockAgent flockAgent, List<Transform> origional, List<FlockAgent> origionalDescription)
    {
        List<Transform> filtered = new List<Transform>();

        for(int i = 0; i < origional.Count; i++){
            FlockAgent itemAgent = origionalDescription[i];

            if(itemAgent != null && itemAgent.AgentFlock == flockAgent.AgentFlock){
                filtered.Add(origional[i]);
            }
        }

        return filtered;
    }
}
