using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] Transform secondaryPosition;

    [SerializeField] GameObject playerPrefab;

    [Header("UI")]
    public Image player1;
    public Image player2;

    [ReadOnly] List<GameObject> playerList = new List<GameObject>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void OnPlayerJoined(PlayerInput playerInput) { 
        playerList.Add(playerInput.gameObject);
        DontDestroyOnLoad(playerInput.gameObject);
        if (playerList.Count >= 1 )
        {
            player1.color = Color.green;
        }
        if (playerList.Count >= 2 )
        {
            player2.color = Color.green;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex != 1)
            return;

        Debug.Log(FindObjectsByType<SpawnPoint>(FindObjectsSortMode.None).Length.ToString());

        int i = 0;
        foreach (SpawnPoint spawnPoint in FindObjectsByType<SpawnPoint>(FindObjectsSortMode.InstanceID))
        {
            playerList[i].transform.position = spawnPoint.transform.position;
            Debug.Log("Moved Player " + i + " to " + spawnPoint.transform.position);
            i++;
        }
    }
}
