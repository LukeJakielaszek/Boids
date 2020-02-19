using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flock : MonoBehaviour
{
    public Material otherMaterial;
    public Material startMaterial;

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior flockBehavior;
    
    [Range(10,10000)]
    public int agentCount = 250;

    [Range(.001f, .1f)]
    public float agentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maximumSpeed = 5f;

    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius{get{return squareAvoidanceRadius;} set{squareAvoidanceRadius = value;}}


    // Start is called before the first frame update
    void Start()
    {
       squareMaxSpeed = maximumSpeed * maximumSpeed;
       squareNeighborRadius = neighborRadius * neighborRadius;
       SquareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

       for(int i = 0; i < agentCount; i++)
       {
           FlockAgent newAgent = Instantiate(
               agentPrefab,
               UnityEngine.Random.insideUnitSphere*agentCount*agentDensity, 
               Quaternion.Euler(Vector3.forward * UnityEngine.Random.Range(0,360)),
               transform
               );

            newAgent.name = "Agent_" + i;
            newAgent.initialize(this);
            agents.Add(newAgent);
       }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents){
            Tuple<List<Transform>, List<FlockAgent>> contexts = GetNearbyObjects(agent);
            List<Transform> context = contexts.Item1;
            List<FlockAgent> contextDescription = contexts.Item2;
            
            Vector3 move = flockBehavior.CalculateMove(agent, context, contextDescription, this);
            move*=driveFactor;

            if(move.sqrMagnitude > squareMaxSpeed){
                move = move.normalized*maximumSpeed;
            }

            agent.Move(move);
        }
    }

    Tuple<List<Transform>, List<FlockAgent>> GetNearbyObjects(FlockAgent agent){
        List<Transform> context = new List<Transform>();
        List<FlockAgent> contextDescription = new List<FlockAgent>();

        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

        foreach(Collider c in contextColliders){
            if(c != agent.AgentCollider){
                context.Add(c.transform);
                contextDescription.Add(c.GetComponent<FlockAgent>());
            }
        }
    
        return new Tuple<List<Transform>, List<FlockAgent>>(context, contextDescription);
    }
}
