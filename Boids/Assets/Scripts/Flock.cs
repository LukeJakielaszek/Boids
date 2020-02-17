using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
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
        
    }
}
