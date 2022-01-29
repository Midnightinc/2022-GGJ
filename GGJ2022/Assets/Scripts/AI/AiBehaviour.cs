using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Purpose: Controls the AI and how it moves
/// Created Date: 29th of January 2022
/// Author/s: Jonathan Nemec
/// Major Revision History: 
///     - 29th of January 2022 - The AI will move within given range and shoot function is called after delay (just needs shooting algrothim)
/// </summary>
[RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(NavMeshObstacle))]
public class AiBehaviour : MonoBehaviour
{
    private const float SmallestInteriorRadius = 0.1f;
    private const float SmallestExteriorRadius = 0.15f;
    private const string MagicNumber = "Just for you Blake ;)";

    //Player Information
    private GameObject player = null;
    private Transform playerPosition = null;

    //AI Information
    private Transform aiTransform = null;
    private CharacterController aiController = null;
    private NavMeshAgent aiNavAgent = null;
    private CapsuleCollider aiCollider = null;
    [SerializeField, Tooltip("Variables containing to movement")] private Movement movement = new Movement();
    [SerializeField, Tooltip("Variables containing to shooting")] private Shooting shooting = new Shooting();

    /// <summary>
    /// Contains all the information about how the AI moves
    /// </summary>
    [System.Serializable] private struct Movement
    {
        [Tooltip("The speed the AI will move at")] public float MoveSpeed;
        [Tooltip("The amount of time before the AI finds a new place to walk to")] public float DestinationUpdateDelay;
        [Tooltip("The size of the are inside the exterior done that is a no-go zone")] public float InteriorCircleRadius;
        [Tooltip("The size of the zone the AI's destination will be in")] public float ExteriorCircleRadius;
        [Tooltip("The layers it will check for the player on")] public LayerMask SeightLayers;
    }

    /// <summary>
    /// Contains all the information about how the AI shoots
    /// </summary>
    [System.Serializable] private struct Shooting
    {
        [Tooltip("The amount of time between shots")] public float ShootingCooldown;
        [Tooltip("The max amount of time that will be randomly added ontop of the existing cooldown")] public float ShootingCooldownMaxAddition;
        [Tooltip("The layers the bullet will collide with")] public LayerMask BulletCollideLayers;
        [Tooltip("The amount of damage each bullet will do")] public float BulletDamage;
        [Tooltip("The speed at which each bullet moves")] public float BulletSpeed;
    }

    /// <summary>
    /// Initalizes variables
    /// </summary>
    private void Start()
    {
        //Initalization
        player = GameObject.FindGameObjectWithTag("Player");
        playerPosition = player.transform;

        aiTransform = gameObject.transform;
        aiController = GetComponent<CharacterController>();
        aiNavAgent = GetComponent<NavMeshAgent>();
        aiNavAgent.speed = movement.MoveSpeed;

        //Makes sure that the radius' never get too small
        if (movement.InteriorCircleRadius < SmallestInteriorRadius)
        {
            movement.InteriorCircleRadius = SmallestInteriorRadius;
        }
        if (movement.ExteriorCircleRadius < SmallestExteriorRadius)
        {
            movement.ExteriorCircleRadius = SmallestExteriorRadius;
        }

        //Keeps interior radius smaller
        if (movement.InteriorCircleRadius >= movement.ExteriorCircleRadius)
        {
            movement.InteriorCircleRadius = movement.ExteriorCircleRadius - 0.1f;
        }
        UpdateDestination();

        //Starts shooting
        this.CallWithDelay(Shoot, FindShootCooldown());
    }

    /// <summary>
    /// The AI will fire it's weapon
    /// </summary>
    private void Shoot()
    {
        //If player not in LOS exits function
        if (!PlayerInLOS())
        {
            //Starts the function delay
            this.CallWithDelay(Shoot, FindShootCooldown());
            return;
        }

        //Blake do your stuff here

        //Starts the delay again
        this.CallWithDelay(Shoot, FindShootCooldown());
    }

    /// <summary>
    /// Finds the delay before the next shot
    /// </summary>
    /// <returns>The amount of time before the next shot</returns>
    private float FindShootCooldown()
    {
        return shooting.ShootingCooldown + Random.Range(0, shooting.ShootingCooldownMaxAddition);
    }

    /// <summary>
    /// Finds a new point for the AI to move to
    /// </summary>
    /// <remarks>Must be within the movement 'donut' and on navmesh</remarks>
    private void UpdateDestination()
    {
        //Finds a new destination
        bool posFound = false;
        Vector3 newPosition = Vector3.zero;
        do
        {
            //Creates a new position
            newPosition.x = Random.Range(playerPosition.position.x - movement.ExteriorCircleRadius, playerPosition.position.x + movement.ExteriorCircleRadius);
            newPosition.z = Random.Range(playerPosition.position.z - movement.ExteriorCircleRadius, playerPosition.position.z + movement.ExteriorCircleRadius);

            if (Vector2.Distance(new Vector2(newPosition.x, newPosition.z), new Vector2(playerPosition.position.x, playerPosition.position.z)) 
                > movement.InteriorCircleRadius)
            {
                //Makes sure it's on navmesh
                NavMeshHit hit;
                if (NavMesh.SamplePosition(newPosition, out hit, 1f, NavMesh.AllAreas))
                {
                    posFound = true;
                }
            }            
        } while (!posFound);

        //Sets the destination
        aiNavAgent.SetDestination(newPosition);

        //Starts the next position update delay
        this.CallWithDelay(UpdateDestination, movement.DestinationUpdateDelay);
    }

    /// <summary>
    /// Checks if the player is in Line of Sight (LOS)
    /// </summary>
    /// <returns>Returns if the AI can see the player or not</returns>
    private bool PlayerInLOS()
    {
        RaycastHit hit = new RaycastHit();
        Vector3 rayDirection = playerPosition.position - aiTransform.position;
        if (Physics.Raycast(aiTransform.position, rayDirection, out hit, Mathf.Infinity, movement.SeightLayers, QueryTriggerInteraction.Ignore))
        {
            return (hit.collider.gameObject == player);
        }
        else
        {
            return false;
        }
    }
}
