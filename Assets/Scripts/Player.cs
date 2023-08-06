using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public void Continue_UI()
    {
        GameManager.Instance.recipeUnlockManager.Continue();
    }
}
