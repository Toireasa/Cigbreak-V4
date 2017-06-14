using UnityEngine;
using System.Collections;
using UnityEngine.Events;

/// <summary>
/// Old coin class that was used for conin stack in game
/// </summary>
[System.Obsolete]
public class Coin : MonoBehaviour {

    [SerializeField]
    private AudioClip coinDrop = null;
    private AudioSource audioSource = null;

    public enum CoinType { Copper, Silver, Gold };
    [SerializeField]
    private CoinType coinType = CoinType.Copper;
    public CoinType TypeOfCoin { get { return coinType; } }

    private bool playedSound = false;

    public static float coinFallSpeed = 10f;
    public static Vector3 CoinOffset { get; private set; }

    private bool resting = false;

    public UnityAction<Coin> OnCoinLanded { get; set; }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (CoinOffset == Vector3.zero)
        {
            CalculateCoinOffset();
        }
    }

    private void PlaySound()
    {
        if (!playedSound)
        {
            audioSource.PlayOneShot(coinDrop);
            playedSound = true;
        }
    }


    private void OnEnable()
    {
        //resting = false;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!resting)
        {
            if (col.gameObject.tag == CigBreakConstants.Tags.Coin && col.gameObject.transform.position.y < transform.position.y)
            {
                Vector3 newPos = col.gameObject.transform.position + CoinOffset;
                transform.position = newPos;
                resting = true;

                if (OnCoinLanded != null)
                {
                    OnCoinLanded(this);
                }
            }
            else if (col.gameObject.tag == CigBreakConstants.Tags.CoinFallOutZone)
            {
                resting = true;
                PlaySound();
            }

        }
    }

    private void Update()
    {
        if (!resting)
        {
            Vector3 newPos = transform.position;
            newPos.y -= coinFallSpeed * Time.deltaTime;
            transform.position = newPos;
        }
    }

    private void CorrectBehaviour()
    {
        resting = true;
        if (OnCoinLanded != null)
        {
            OnCoinLanded(this);
        }
    }

    private void CalculateCoinOffset()
    {
        float z = GetComponent<BoxCollider>().bounds.extents.z * 0.25f;
        CoinOffset = new Vector3(0f, z, 0f);
    }

    private void OnDisable()
    {
        OnCoinLanded = null;
    }
}
