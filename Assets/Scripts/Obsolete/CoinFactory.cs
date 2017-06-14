using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

namespace CigBreak
{
    /// <summary>
    /// Old object pool for in-game coins stack
    /// </summary>
    [System.Obsolete]
    public class CoinFactory : MonoBehaviour
    {
        [SerializeField]
        private Coin[] coinPrefabs = null;

        private List<Coin>[] coins = null;
        

        public static CoinFactory coinFactory = null;

        void Awake()
        {
            // Ensure single instance
            Initialise(10);
            if (coinFactory == null)
            {
                coinFactory = this;
                
            }
            else
            {
                //Destroy(gameObject);
            }
        }

        public void Initialise(int objectsSpawnedPerType)
        {
            // Preinstantiate objects in the factory
            coins = new List<Coin>[coinPrefabs.Length];

            for (int i = 0; i < coinPrefabs.Length; i++)
            {
                coins[i] = new List<Coin>();

                for (int j = 0; j < objectsSpawnedPerType; j++)
                {
                    Coin coin = Instantiate(coinPrefabs[i], transform.position, transform.rotation) as Coin;
                    coin.gameObject.SetActive(false);
                    coin.gameObject.transform.parent = this.transform;
                    coin.gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    coins[i].Add(coin);
                }
            }
        }

        public GameObject GetNextCoin(Coin.CoinType coinType)
        {
            GameObject nextCoin = null;
            foreach(List<Coin> coinGroup in coins)
            {
                if(coinGroup[0].TypeOfCoin == coinType)
                {
                    nextCoin = coinGroup.Select(c => c.gameObject).FirstOrDefault(g => !g.activeSelf);
                }
            }

            return nextCoin;
        }

        public void ReturnCoin(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.position = transform.position;
            obj.transform.rotation = transform.rotation;
        }
    }

}