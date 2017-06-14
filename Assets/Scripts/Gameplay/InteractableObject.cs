using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace CigBreak
{
    /// <summary>
    /// Represents all in game objects that the player can interact with through input
    /// </summary>
    public abstract class InteractableObject : MonoBehaviour
    {
        // Reference to data object being used
        [SerializeField]
        protected GameItemData itemData = null;
        public GameItemData ItemData { get { return itemData; } }

        // Object health
        protected int currentHealth = 0;
        public int CurrentHealth { get { return currentHealth; } }

        protected int MaterialIndex { get { return Mathf.Min(itemData.Health - currentHealth, itemData.Materials.Count - 1); } }

        // Components
        protected Rigidbody2D rigidBody2D = null;
        protected SpriteRenderer spriteRenderer = null;
        protected BoxCollider2D bCollider = null;
        protected AudioSource audioSource = null;

        // for pause functionality
        protected bool paused = false;
        protected Vector3 velocity = Vector3.zero;
        protected float angularVelocity = 0f;
        protected float gravityScale = 1f;

        // Input response functions
        public abstract void OnSwipe();
        public abstract void OnTap();

        // Called when object was 'destroyed' due to user input
        public UnityAction<InteractableObject> OnObjectDestroyed { get; set; }
        // Called when object was collected due to user input
        public UnityAction<InteractableObject> OnObjectCollected { get; set; }
        // Called when object fell off screen into the clear zone
        public UnityAction<InteractableObject> OnObjectOutOfScreen { get; set; }

        public UnityAction<InteractableObject> OnMultiObjectDestroyed { get; set; }

        protected void Awake()
        {
            // Get and cache component references
            rigidBody2D = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            bCollider = GetComponent<BoxCollider2D>();
            audioSource = GetComponent<AudioSource>();
        }

        public virtual void Initialise(GameItemData itemData)
        {
            this.itemData = itemData;
            currentHealth = itemData.Health;

            gravityScale = rigidBody2D.gravityScale;

            Vector2 colliderSize = new Vector2(itemData.ColliderX, itemData.ColliderY);
            bCollider.size = colliderSize;

            UpdateMaterial();
        }

        /// <summary>
        /// Update material to match current object health
        /// </summary>
        protected void UpdateMaterial()
        {
            if (itemData != null)
            {
                spriteRenderer.sprite = itemData.Materials[MaterialIndex].Sprite;
                spriteRenderer.sharedMaterial = itemData.Materials[MaterialIndex].Material;
            }
        }

        public void OutOfScreen()
        {
            if (OnObjectOutOfScreen != null)
            {
                OnObjectOutOfScreen(this);
            }
        }

        /// <summary>
        /// Pause or unpause object
        /// Cache values of moving object so it can be sent to contnue the same movement when unpause
        /// </summary>
        /// <param name="pause">true to pause, false to unpause</param>
        public void TogglePause(bool pause)
        {
            paused = pause;
            if (pause)
            {
                velocity = rigidBody2D.velocity;
                angularVelocity = rigidBody2D.angularVelocity;

                rigidBody2D.velocity = Vector3.zero;
                rigidBody2D.angularVelocity = 0f;
                rigidBody2D.gravityScale = 0f;
            }
            else
            {
                rigidBody2D.velocity = velocity;
                rigidBody2D.angularVelocity = angularVelocity;
                rigidBody2D.gravityScale = gravityScale;
            }       
        }

        // Reset the object so it can be reused
        public void Reset()
        {
            currentHealth = itemData.Health;
            UpdateMaterial();
            TogglePause(false);
        }

    }

}