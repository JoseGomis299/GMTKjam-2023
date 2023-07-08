using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    public enum MovementStates
    {
        random, //DONE
        storm,
        followPlayer, 
        runFromPlayer,
        followChest, //DONE
    }

    public MovementStates movState = MovementStates.followPlayer;


    Vector2 directionMoving;
    float randomTimeTarget;
    float timeInDirection;
    Transform playerToFollow;

    private void Start()
    {
        timeInDirection = 0f;
        movState = MovementStates.followPlayer;
    }
    private void Update()
    {
        timeInDirection += Time.deltaTime;
    }

    public void SetPlayerToFollow(Transform player)
    {
        playerToFollow = player;
    }

    public Transform GetPlayerToFollow()
    {
        return playerToFollow;
    }

    public void SetDirectionMoving(Vector2 newDirection)
    {
        directionMoving = newDirection;
    }

    public Vector2 GetDirectionMoving()
    {
        return directionMoving;
    }

    public void SetRandomTimeTarget(float randomTime)
    {
        randomTimeTarget = randomTime;
    }

    public void ResetTimeInDirection()
    {
        timeInDirection = 0f;
    }

    public float GetRandomTimeTarget()
    {
        return randomTimeTarget;
    }

    public float GetTimeInDirection()
    {
        return timeInDirection;
    }
}
