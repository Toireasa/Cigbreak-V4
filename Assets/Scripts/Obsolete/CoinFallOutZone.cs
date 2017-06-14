using UnityEngine;
using System.Collections;

namespace CigBreak
{
    /// <summary>
    /// Old clear area for coin stack
    /// </summary>
    [System.Obsolete]
    public class CoinFallOutZone : MonoBehaviour
    {
        private CoinTube coinTube = null;

        private void Start()
        {
            coinTube = GetComponentInParent<CoinTube>();
        }

        private void OnTriggerEnter(Collider col)
        {
            if (col.tag == CigBreakConstants.Tags.Coin)
            {
                coinTube.FixCoinPositions();
            }
        }

    }

}
