using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, List<FlockAgent> contextDescription, Flock flock)
    {
        if(weights.Length != behaviors.Length){
            Debug.Log("Error: Data mismatch in " + name, this);
            return Vector3.forward;
        }else{
            Vector3 move = Vector3.zero;

            for(int i = 0; i < behaviors.Length; i++){
                Vector3 partialMove = behaviors[i].CalculateMove(agent, context, contextDescription, flock) * weights[i];

                if(partialMove != Vector3.zero){
                    if(partialMove.sqrMagnitude > weights[i] * weights[i]){
                        partialMove.Normalize();
                        partialMove*=weights[i];
                    }
                }

                move += partialMove;
            }

            return move;
        }
    }
}
