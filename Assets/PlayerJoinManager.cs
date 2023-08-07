using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerJoinManager : MonoBehaviour
{
    public static PlayerJoinManager Instance;

    [SerializeField] GameObject playerPrefab;

    [Header("Materials")]
    public Material player1Mat;
    public Material player2Mat;

    [Header("UI")]
    public UIImageSwapper player1;
    public UIImageSwapper player2;

    public Slider slider;

    [ReadOnly] List<GameObject> playerList = new List<GameObject>();

    private void Start()
    {
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void OnPlayerJoined(PlayerInput playerInput) { 
        playerList.Add(playerInput.gameObject);
        DontDestroyOnLoad(playerInput.gameObject);

        playerInput.gameObject.GetComponent<PlayerController>().enabled = false;
        playerInput.gameObject.GetComponent<CharacterController>().enabled = false;

        if (playerList.Count == 1 )
        {
            player1.SwapImagesWithFade();
            playerInput.gameObject.GetComponent<Player>().SwapMaterial(player1Mat);
            Debug.Log("Swapping Player 1 Mat");
        }
        if (playerList.Count >= 2 )
        {
            player2.SwapImagesWithFade();
            playerInput.gameObject.GetComponent<Player>().SwapMaterial(player2Mat);
            Debug.Log("Swapping Player 2 Mat");
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

        foreach (GameObject player in playerList)
        {
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<CharacterController>().enabled = true;
        }
    }
}
