using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{

    public void OnSubmit(CallbackContext context)
    {
        if (context.phase == UnityEngine.InputSystem.InputActionPhase.Performed)
        {
            Debug.Log("Continue_UI");
            GameManager.Instance.recipeUnlockManager.Continue();
        }
    }
}
