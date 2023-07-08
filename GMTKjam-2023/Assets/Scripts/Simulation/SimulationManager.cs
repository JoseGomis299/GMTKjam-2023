using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SimulationManager : MonoBehaviour
{
    [SerializeField] private int playerCount;
    List<GameObject> playerVisualList;

    private bool _paused;
    public static SimulationManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        GeneratePlayers();    
    }

    void GeneratePlayers()
    {
        MapManager.instance.GenerateCharacters(playerCount);
        foreach (Character player in MapManager.instance.aliverCharacters)
        {
            player.GetComponent<CharacterStateController>().SetDirectionMoving(new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));
            player.GetComponent<CharacterStateController>().SetRandomTimeTarget(UnityEngine.Random.Range(0.5f, 1.0f));
        }
    }

    private void Update()
    {
        if(_paused) return;

        foreach (Character player in MapManager.instance.aliverCharacters)
        {
            CharacterStateController.MovementStates currentState = GetPlayerState(player);
            
            if (currentState == CharacterStateController.MovementStates.random)
            {
                Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

                player.transform.position += new Vector3(
                    direction.x * Time.deltaTime * 0.1f,
                    direction.y * Time.deltaTime * 0.1f,
                    0);

                if (player.GetComponent<CharacterStateController>().GetTimeInDirection() >= player.GetComponent<CharacterStateController>().GetRandomTimeTarget())
                {
                    player.GetComponent<CharacterStateController>().SetDirectionMoving(new Vector2(UnityEngine.Random.Range(-1.0f, 1.0f), UnityEngine.Random.Range(-1.0f, 1.0f)));
                    player.GetComponent<CharacterStateController>().SetRandomTimeTarget(UnityEngine.Random.Range(0.5f, 30.0f));
                    player.GetComponent<CharacterStateController>().ResetTimeInDirection();
                }
            }
            else if (currentState == CharacterStateController.MovementStates.followChest)
            {
                GameObject closestChest = null;
                float closestDistance  = Mathf.Infinity;
                foreach (Chest chest in MapManager.instance.chests)
                {
                    float distance = Vector2.Distance(chest.transform.position, player.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestChest = chest.gameObject;
                    }
                }
                player.GetComponent<CharacterStateController>().SetDirectionMoving((closestChest.transform.position - player.transform.position).normalized);


                Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

                player.transform.position += new Vector3(
                    direction.x * Time.deltaTime * 0.1f,
                    direction.y * Time.deltaTime * 0.1f,
                    0);
            }
            else if (currentState == CharacterStateController.MovementStates.followPlayer)
            {
                Transform playerToFollow = player.GetComponent<CharacterStateController>().GetPlayerToFollow();
                player.GetComponent<CharacterStateController>().SetDirectionMoving((playerToFollow.position - player.transform.position).normalized);

                Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

                player.transform.position += new Vector3(
                    direction.x * Time.deltaTime * 0.1f,
                    direction.y * Time.deltaTime * 0.1f,
                    0);

            }

        }
    }

    CharacterStateController.MovementStates GetPlayerState(Character _player)
    {
        return _player.GetComponent<CharacterStateController>().movState;
    }

    public void pauseSimulation()
    {
        _paused = true;
    }
    
    public void ResumeSimulation()
    {
        _paused = false;
    }
}
