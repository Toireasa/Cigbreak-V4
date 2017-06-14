using UnityEngine;
using System.Collections;
using System.Linq;

namespace CigBreak
{
    public class PowerupEffect : MonoBehaviour
    {
        [SerializeField]
        private PowerupData[] triggeringPowerups = null;
        [SerializeField]
        private AudioClip SFX = null;

        private ParticleSystem m_ParticleSystem = null;
        private AudioSource audioSource = null;
        private Gameplay gameplay = null;

        // Use this for initialization
        void Start()
        {
            m_ParticleSystem = GetComponent<ParticleSystem>();
            audioSource = GetComponent<AudioSource>();
            gameplay = Gameplay.gameplay;
            if (gameplay != null)
            {
                gameplay.OnPowerupCollected += Play;
            }
            else
            {
                StartCoroutine(Wait());
            }
        }

        private IEnumerator Wait()
        {
            while (Gameplay.gameplay == null)
            {
                yield return new WaitForEndOfFrame();
            }

            gameplay = Gameplay.gameplay;
            gameplay.OnPowerupCollected += Play;
        }

        private void Play(InteractableObject powerup)
        {
            if (triggeringPowerups.FirstOrDefault(p => p.GUID == powerup.ItemData.GUID) != null)
            {
                m_ParticleSystem.Play();
                audioSource.PlayOneShot(SFX);
            }
        }

        private void OnDisable()
        {
            if (gameplay != null)
            {
                gameplay.OnPowerupCollected -= Play;
            }
        }
    }
}
