using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{

    public float holdDuration = 2.0f; // Time in seconds to hold the button

    private bool isHolding = false;
    private float holdTimer = 0f;

    private bool gameStarted = false;

    public MeshRenderer[] renderers;

    public void OnSubmit(CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            if (GameManager.Instance != null && GameManager.Instance.recipeUnlockManager != null)
                GameManager.Instance.recipeUnlockManager.Continue();
        }
    }

    public void SwapMaterial(Material mat)
    {
        foreach (MeshRenderer renderer in renderers)
        {
            renderer.SetMaterials(new List<Material>() { mat });
        }
    }

    private void Update()
    {
        if (gameStarted)
            return;

        if (isHolding && PlayerJoinManager.Instance.slider != null)
        {
            holdTimer += Time.deltaTime;

            PlayerJoinManager.Instance.slider.value = holdTimer / holdDuration;
            if (holdTimer >= holdDuration)
            {
                StartGame();
            }
        }
    }

    public void OnStartButtonPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHolding = true;
        }
        else if (context.canceled)
        {
            isHolding = false;
            holdTimer = 0f;
            PlayerJoinManager.Instance.slider.value = 0;
        }
    }

    private void StartGame()
    {
        gameStarted = true;
        PlayerJoinManager.Instance.StartGame();
    }
}
