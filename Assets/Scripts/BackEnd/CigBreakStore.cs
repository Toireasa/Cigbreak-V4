using UnityEngine;
using System.Collections;
using Soomla.Store;
using System;
namespace Soomla
{
    public class CigBreakStore : IStoreAssets
    {
        public const string UNLOCK_FULL_GAME = "full_game";


        public static VirtualGood FULL_GAME = new LifetimeVG(
                    "unlock full game",                    // name
                    "unlock the full game",               // description
                    "full_game",                         // item id
            new PurchaseWithMarket(UNLOCK_FULL_GAME, 1.49));


        public VirtualCategory[] GetCategories()
        {
            return new VirtualCategory[] { };
        }

        public VirtualCurrency[] GetCurrencies()
        {
            return new VirtualCurrency[] { };
        }

        public VirtualCurrencyPack[] GetCurrencyPacks()
        {
            return new VirtualCurrencyPack[] { };
        }

        public VirtualGood[] GetGoods()
        {
            return new VirtualGood[] { FULL_GAME };
        }

        public int GetVersion()
        {
            return 3;
        }
    }
}