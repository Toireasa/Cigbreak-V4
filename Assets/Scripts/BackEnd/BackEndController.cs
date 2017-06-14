using UnityEngine;
using System.Collections;
using Soomla.Store;
using System.Collections.Generic;
using Soomla;
using CigBreak;
using UnityEngine.UI;


public class BackEndController : MonoBehaviour
{
    //[SerializeField]
    //Text text = null;

    UnlockFullGame m_UnlockFullGame;
    public void SetUnlockFullGame(UnlockFullGame _state)
    {
        m_UnlockFullGame = _state;
    }

    public static BackEndController InstanceBackend;
    // Use this for initialization

    void Awake()
    {
        if (InstanceBackend)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            InstanceBackend = this;
        }
    }

    CigBreakStore store = null;
    // Use this for initialization
    void Start ()
    {        
        //if (!PlayerProfile.GetProfile().UnlockFullGame)
        {
            store = new CigBreakStore();
            SoomlaStore.Initialize(store);
            StoreEvents.OnMarketPurchase += onMarketPurchase;
            StoreEvents.OnItemPurchased += onItemPurchased;
            StoreEvents.OnUnexpectedStoreError += Error;

            //text.text = StoreInventory.GetItemBalance(CigBreakStore.UNLOCK_FULL_GAME).ToString();
            //StoreInventory.TakeItem(CigBreakStore.UNLOCK_FULL_GAME, 10);

            DontDestroyOnLoad(gameObject);
        }
       
    }


    // Update is called once per frame
    void Update ()
    {

    }

    public void UnlockFullGame()
    {
#if DEBUG
        StoreInventory.TakeItem(CigBreakStore.UNLOCK_FULL_GAME,1);
#endif
        StoreInventory.BuyItem(CigBreakStore.UNLOCK_FULL_GAME);
    }

    public void Error(int action)
    {
        SoomlaUtils.LogError("ExampleEventHandler", "error with code: " + action);
        m_UnlockFullGame.ErrorPage();
       // text.text = "Error";
    }
    public void onMarketPurchase(PurchasableVirtualItem pvi, string payload, Dictionary<string, string> extra)
    {
        //text.text = "onMarketPurchase";
        Debug.Log("onMarketPurchase");
    }

    public void onItemPurchased(PurchasableVirtualItem pvi, string payload)
    {
        if (pvi.ItemId == "full_game" && !PlayerProfile.GetProfile().UnlockFullGame)
        {
            PlayerProfile.GetProfile().UnlockFullGame = true;
            m_UnlockFullGame.GameUnlock();
        }
       // text.text = "onItemPurchased";
    }


}
