using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager Instance;

    [Header("Game Start")]
    public bool managePallet = false;
    public Pallet pallet;
    public ItemType[] startingDelivery;

    [Header("Unlocks")]
    public ItemType[] startItems;
    [ReadOnly] public List<ItemType> unlockedItems = new List<ItemType>();

    [Header("Scripts")]
    [SerializeField] Money money;
    [SerializeField] public GameTimer gameTimer;
    [SerializeField] public GoalManager goalManager;
    [SerializeField] public RecipeUnlockManager recipeUnlockManager;
    [SerializeField] public OrderManager orderManager;
    [SerializeField] public PalletManager palletManager;


    [Header("Game Over")]
    [SerializeField] public GameObject standardCanvas;
    [SerializeField] public GameObject gameOverCanvas;
    [SerializeField] TMP_Text scoreText;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (ItemType item in startItems)
        {
            unlockedItems.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateFirstDelivery()
    {
        pallet.AddItemTypes(startingDelivery);
    }

    public void ModifyFunds(int amount)
    {
        money.money += amount;
    }

    public bool SubtractFunds(int amount)
    {
        if (money.money >= amount)
        {
            money.money -= amount;
            return true;
        }
        return false;
    }

    public void PlayerJoined()
    {

    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        foreach(Item item in FindObjectsOfType<Item>())
        {
            Destroy(item.gameObject);
        }

        Destroy(FindObjectOfType<PlayerInputManager>().gameObject);
        foreach (PlayerInput playerInput in FindObjectsOfType<PlayerInput>())
        {
            Destroy(playerInput.gameObject);
        }

        standardCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);

        scoreText.text = ""+money.money;
    }

}
