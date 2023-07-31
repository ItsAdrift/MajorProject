using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJoinManager : MonoBehaviour
{
    [SerializeField] Transform secondaryPosition;

    int count = 0;

    public void OnPlayerJoined(PlayerInput playerInput) { 
        count++;

        if (count == 2)
        {
            Debug.Log("Check - " + playerInput.gameObject.name);
            playerInput.gameObject.SetActive(false);
            playerInput.gameObject.transform.position = secondaryPosition.position;
        }
        
    }
}
