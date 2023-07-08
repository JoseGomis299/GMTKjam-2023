using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateController : MonoBehaviour
{
    enum MovementStates
    {
        random,
        storm,
        followPlayer,
        runFromPlayer,
        followChest,
    }

    Vector2 directionMoving;
    float randomTimeTarget;
    float timeInDirection;

    private void Start()
    {
        timeInDirection = 0f;
    }
    private void Update()
    {
        timeInDirection += Time.deltaTime;
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
