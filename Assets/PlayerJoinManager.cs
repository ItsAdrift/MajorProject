using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] Transform secondaryPosition;

    int count = 0;

    [SerializeField] GameObject playerPrefab;

    public void OnPlayerJoined(PlayerInput playerInput) { 
        count++;

        if (count == 2)
        {
            Debug.Log("Check - " + playerInput.gameObject.name);
            playerInput.gameObject.SetActive(false);
            playerInput.gameObject.transform.position = secondaryPosition.position;
        }
    }

    int pCount = 0;

    public void CreateArrowsPlayer()
    {
        pCount++;

        if (pCount == 1)
        {
            PlayerInput p = PlayerInput.Instantiate(playerPrefab, controlScheme: "arrows", pairWithDevice: Keyboard.current);
            //defaultActionMap = "arrows";
            p.defaultControlScheme = "arrows";
            p.ActivateInput();
        }
        
        
    }
}
