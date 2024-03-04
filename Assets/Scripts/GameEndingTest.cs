using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndingTest : MonoBehaviour
{
    
    public void EndGame()
    {
        Debug.Log("Ending Game");
        GameManager.Instance.GameOver();
        
    }

}
