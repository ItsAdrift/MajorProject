using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] public GoalManager goalManager;
    [SerializeField] public RecipeUnlockManager recipeUnlockManager;
    [SerializeField] public OrderManager orderManager;
    [SerializeField] public PalletManager palletManager;

    

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

}
