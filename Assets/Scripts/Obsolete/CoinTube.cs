using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace CigBreak
{
    /// <summary>
    /// Old coin tube for in game coin stack
    /// </summary>
    [System.Obsolete]
    public class CoinTube : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPoint = null;
        [SerializeField]
        private Transform tubeBotton = null;

        [SerializeField]
        private int upgradeCount = 10;

        private List<Coin> coins = null;

//        private Vector3 coinOffset = Vector3.zero;
      // private Vector3 lastCoinPosition = Vector3.zero;

        private Gameplay gameplay = null;
        private CoinFactory coinFactory = null;

        void Start()
        {
            gameplay = Gameplay.gameplay;
            coinFactory = CoinFactory.coinFactory;

            gameplay.OnPointEarned += AddCoin;

            coins = new List<Coin>();
        }

        private void AddCoin()
        {
            GameObject coin = coinFactory.GetNextCoin(Coin.CoinType.Copper);
            if (coin != null)
            {
                Coin cCoin = coin.GetComponent<Coin>();
                coin.transform.position = spawnPoint.position;
                cCoin.OnCoinLanded += CoinLanded;
            }
        }

        private void RemoveCoin()
        {

        }

        private void CoinLanded(Coin coin)
        {
            coins.Add(coin);
            if (coins.Count >= 10)
            {
                StartCoroutine(CollateTopCoins());
            }
        }

        private IEnumerator CollateTopCoins()
        {
            int startIndex = coins.Count - upgradeCount;

            var top = coins.Skip(startIndex).Take(upgradeCount);
            yield return StartCoroutine(Collate(startIndex, top));

            if (coins.Count >= 10)
            {
                StartCoroutine(CollateBottomCoins());
            }

            FixCoinPositions();
        }

        private IEnumerator CollateBottomCoins()
        {
            var bottom = coins.Take(upgradeCount);
            yield return StartCoroutine(Collate(0, bottom));
        }

        private IEnumerator Collate(int startIndex, IEnumerable<Coin> top)
        {
            Coin.CoinType topType = top.Last().TypeOfCoin;
            if (topType != Coin.CoinType.Gold)
            {
                if (top.All(c => c.TypeOfCoin == topType))
                {
                    gameplay.UpdateHealth(1);

                    Vector3 bottomPos = coins[startIndex].transform.position;

                    GameObject upgradedCoin = coinFactory.GetNextCoin(topType + 1);
                    upgradedCoin.transform.position = bottomPos;
                    //lastCoinPosition = bottomPos;

                    yield return StartCoroutine(AnimateCoinCollapse(top, bottomPos));

                    foreach (Coin c in top)
                    {
                        coinFactory.ReturnCoin(c.gameObject);
                    }

                    coins.RemoveRange(startIndex, upgradeCount);
                    coins.Insert(startIndex, upgradedCoin.GetComponent<Coin>());


                }
            }
        }

        private IEnumerator AnimateCoinCollapse(IEnumerable<Coin> coinGroup, Vector3 collapsePoint)
        {
            Coin furthest = coinGroup.Last();
            while (furthest.transform.position != collapsePoint)
            {
                foreach (Coin c in coinGroup)
                {
                    c.transform.position = Vector3.MoveTowards(c.transform.position, collapsePoint, Coin.coinFallSpeed * Time.deltaTime);
                }
                yield return null;
            }
        }

        public void FixCoinPositions()
        {
            if (coins.Count > 0)
            {
                Vector3 lastPos = tubeBotton.transform.position;
                Vector3 offset = Coin.CoinOffset;

                for (int i = 0; i < coins.Count; i++)
                {
                    coins[i].transform.position = lastPos;
                    lastPos = lastPos + offset;
                }
            }
        }
    }
}
