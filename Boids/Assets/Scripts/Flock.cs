using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public Material otherMaterial;
    public Material startMaterial;

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior flockBehavior;
    
    [Range(10,500)]
    public int agentCount = 250;
    const float agentDensity = 0.08f;

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
               Random.insideUnitSphere*agentCount*agentDensity, 
               Quaternion.Euler(Vector3.forward * Random.Range(0,360)),
               transform
               );

            newAgent.name = "Agent_" + i;
            agents.Add(newAgent);
       }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents){
            List<Transform> context = GetNearbyObjects(agent);
            //Debug.Log(context.Count);
            //agent.GetComponentInChildren<MeshRenderer>().material.Lerp(startMaterial, otherMaterial, context.Count / 6f);
            
            Vector3 move = flockBehavior.CalculateMove(agent, context, this);
            move*=driveFactor;

            if(move.sqrMagnitude > squareMaxSpeed){
                move = move.normalized*maximumSpeed;
            }

            agent.Move(move);
            
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent){
        List<Transform> context = new List<Transform>();

        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);

        foreach(Collider c in contextColliders){
            if(c != agent.AgentCollider){
                context.Add(c.transform);
            }
        }

        return context;
    }
}
