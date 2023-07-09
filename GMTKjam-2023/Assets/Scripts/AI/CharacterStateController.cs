using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    public enum MovementStates
    {
        random, //DONE
        storm, //DONE
        followPlayer, //DONE
        runFromPlayer, //DONE
        followChest, //DONE
    }

    public MovementStates movState = MovementStates.random;


    Vector2 directionMoving;
    float randomTimeTarget;
    float timeInDirection;
    Transform playerToFollow;

    Character target;

    private void Start()
    {
        timeInDirection = 0f;
        movState = MovementStates.random;
    }

    public void SetMoveState(MovementStates newMS)
    {
        movState = newMS;
    }

    private void Update()
    {
        timeInDirection += Time.deltaTime;
    }

    public void SetTarget(Character newTarget)
    {
        target = newTarget;
    }

    public Character GetTarget()
    {
        return target;
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
