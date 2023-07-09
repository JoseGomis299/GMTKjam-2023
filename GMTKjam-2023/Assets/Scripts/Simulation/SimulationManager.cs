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
    public Transform nextZone;

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
            SetClosestPlayerTarget(player);
        }
    }

    private void Update()
    {
        return;
        if(_paused) return;

        foreach (Character player in MapManager.instance.aliverCharacters)
        {
            CheckPlayerState(player);
            CharacterStateController.MovementStates currentState = GetPlayerState(player);
            
            if (currentState == CharacterStateController.MovementStates.random)
            {
                Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

                player.transform.position += new Vector3(
                    direction.x * Time.deltaTime * 0.006f,
                    direction.y * Time.deltaTime * 0.006f,
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
                    direction.x * Time.deltaTime * 0.006f,
                    direction.y * Time.deltaTime * 0.006f,
                    0);
            }
            else if (currentState == CharacterStateController.MovementStates.followPlayer)
            {
                SetClosestPlayerTarget(player);

                Transform playerToFollow = player.GetComponent<CharacterStateController>().GetPlayerToFollow();
                player.GetComponent<CharacterStateController>().SetDirectionMoving(-(player.transform.position - playerToFollow.position).normalized);
                print((player.transform.position - playerToFollow.position).normalized);

                Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

                player.transform.position += new Vector3(
                    direction.x * Time.deltaTime * 0.006f,
                    direction.y * Time.deltaTime * 0.006f,
                    0);
            }
            else if (currentState == CharacterStateController.MovementStates.runFromPlayer)
            {
                SetClosestPlayerTarget(player);

                Transform playerToFollow = player.GetComponent<CharacterStateController>().GetPlayerToFollow();
                player.GetComponent<CharacterStateController>().SetDirectionMoving((player.transform.position - playerToFollow.position).normalized);
                print((player.transform.position - playerToFollow.position).normalized);

                Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

                player.transform.position += new Vector3(
                    direction.x * Time.deltaTime * 0.006f,
                    direction.y * Time.deltaTime * 0.006f,
                    0);
            }
            else if (currentState == CharacterStateController.MovementStates.storm)
            {
                player.GetComponent<CharacterStateController>().SetDirectionMoving((nextZone.transform.position-player.transform.position).normalized);

                Vector2 direction = player.GetComponent<CharacterStateController>().GetDirectionMoving();

                player.transform.position += new Vector3(
                    direction.x * Time.deltaTime * 0.006f,
                    direction.y * Time.deltaTime * 0.006f,
                    0);
            }
        }
    }

    void CheckPlayerState(Character player)
    {
        float closestChest = Mathf.Infinity;
        foreach (Chest chest in MapManager.instance.chests)
        {
            float distance = Vector2.Distance(chest.transform.position, player.transform.position);
            if (distance < closestChest)
            {
                closestChest = distance;
            }
        }

        float closestPlayerDist = Mathf.Infinity;
        Character closestPlayer = null;
        foreach (Character currPlayer in MapManager.instance.aliverCharacters)
        {
            float distance = Vector2.Distance(currPlayer.transform.position, player.transform.position);
            if (distance < closestPlayerDist && currPlayer != player)
            {
                closestPlayer = currPlayer;
                closestPlayerDist = distance;
            }
        }

        float zoneRadius = FindObjectOfType<SafeZone>().zoneRadius / 2f;
        Vector2 zonePosition = FindObjectOfType<SafeZone>().transform.position;
        float distanceFromZoneBorder = zoneRadius - Vector2.Distance(zonePosition, player.transform.position);
        float percentageDistance = (distanceFromZoneBorder / zoneRadius)*100f;

        if (percentageDistance <= 25f)
        {
            //---------------------------------------------------------------------------CLOSE TO STORM
            player.GetComponent<CharacterStateController>().SetMoveState(CharacterStateController.MovementStates.storm);
        }
        else if (player.GetHealth() + player.GetShield() < 40f)
        {
            //---------------------------------------------------------------------------LOW HEALTH

            if (closestPlayerDist < 0.1f)
            {
                player.GetComponent<CharacterStateController>().SetMoveState(CharacterStateController.MovementStates.runFromPlayer);
            }
            else if (percentageDistance <= 35f)
            {
                player.GetComponent<CharacterStateController>().SetMoveState(CharacterStateController.MovementStates.storm);
            }
            else
            {
                player.GetComponent<CharacterStateController>().SetMoveState(CharacterStateController.MovementStates.followChest);
            }
        }
        else if (closestPlayerDist < 0.15f)
        {
            //---------------------------------------------------------------------------CLOSE PLAYER

            if (closestPlayer.GetWeapon().tier <= player.GetWeapon().tier + 1)
            {
                player.GetComponent<CharacterStateController>().SetMoveState(CharacterStateController.MovementStates.followPlayer);
            }

        }
    }

    void SetClosestPlayerTarget(Character player)
    {
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;
        foreach (Character currPlayer in MapManager.instance.aliverCharacters)
        {
            float distance = Vector2.Distance(currPlayer.transform.position, player.transform.position);
            if (distance < closestDistance && currPlayer != player)
            {
                closestDistance = distance;
                closestPlayer = currPlayer.gameObject;
            }
        }
        player.GetComponent<CharacterStateController>().SetPlayerToFollow(closestPlayer.transform);
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
