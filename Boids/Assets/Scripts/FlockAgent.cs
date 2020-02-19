using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Flock agentFlock;
    public Flock AgentFlock{get{return agentFlock;}}

    Collider agentCollider;
    public Collider AgentCollider {get {return agentCollider;}}

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();    
    }

    public void initialize(Flock flock){
        this.agentFlock = flock;
    }

    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;

    }
}
