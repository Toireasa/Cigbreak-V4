using UnityEngine;
using System.Collections;

namespace CigBreak
{
    [System.Obsolete]
    public class Chest : MonoBehaviour
    {
        [SerializeField]
        private BoxCollider spawnArea = null;

        [SerializeField]
        private float timeBetweenCoins = 0.2f;

        private CoinFactory factory = null;

        // Use this for initialization
        void Start()
        {
            factory = CoinFactory.coinFactory;
            StartCoinDrop();
        }

        public void StartCoinDrop()
        {
            StartCoroutine(DropCoins());
        }

        private IEnumerator DropCoins()
        {
           // audioSource.PlayDelayed(1f);

            GameObject coin = factory.GetNextCoin(Coin.CoinType.Silver);
            while (coin != null)
            {
                Vector3 newPos = spawnArea.transform.position + new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), 0f, 0f);
                coin.transform.position = newPos;
                coin.SetActive(true);

                coin = factory.GetNextCoin(Coin.CoinType.Silver);
                yield return new WaitForSeconds(timeBetweenCoins);
            }

            yield return new WaitForSeconds(1f);
           // audioSource.Stop();
        }
    } 

}
