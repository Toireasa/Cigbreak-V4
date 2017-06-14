using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

namespace CigBreak
{
    public class MapCameraController : MonoBehaviour
    {
        [SerializeField]
        private GameObject mapLock = null;

        private Vector3 scrollStart = Vector3.zero;
        public bool Scrolling { get; private set; }

        private Vector3 scrollVelocity = Vector3.zero;
        [SerializeField]
        private float velocityDecay = 0.1f;
        [SerializeField]
        private bool lockX = true;
        [SerializeField]
        private float scrollThreshold = 0.05f;

        [SerializeField]
        private GameObject offsetReference = null;
        private Vector3 offset = Vector3.zero;

        public GameObject ViewTarget { get; private set; }

        void Awake()
        {
            //buttonController.OnButtonsInitialised += FocusOnLastLevel;
        }

        // Use this for initialization
        void Start()
        {
            offset = transform.position - offsetReference.transform.position;
            if (ViewTarget != null)
            {
                transform.position = ViewTarget.transform.position + offset;
            }
        }

        Vector3 m_initPosition = new Vector3(0, 0, 0);
        public void SetViewTarget(GameObject obj)
        {
            ViewTarget = obj;
            if (ViewTarget != null)
            {
                m_initPosition = transform.position;
                transform.position = ViewTarget.transform.position + offset;
            }
            AdjustToLockArea();
        }

        public void SetInitViewTarget()
        {
            transform.position = m_initPosition;
            AdjustToLockArea();
        }

        // Update is called once per frame
        void Update()
        {
            // Only accept event if they are not being captured by the gui
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (scrollStart == Vector3.zero)
                    {
                        scrollVelocity = Vector3.zero;

                        RaycastHit hit;
                        if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity, 1 << 10))
                        {
                            scrollStart = hit.point;
                        }
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    RaycastHit hit;
                    if (Physics.Raycast(GetComponent<Camera>().ScreenPointToRay(Input.mousePosition), out hit, float.PositiveInfinity, 1 << 10))
                    {
                        Vector3 difference = scrollStart - hit.point;

                        if(difference.magnitude > scrollThreshold)
                        {
                            Scrolling = true;
                        }

                        if (lockX)
                        {
                            scrollVelocity = new Vector3(0f, 0f, difference.z);
                        }
                        else
                        {
                            scrollVelocity = new Vector3(difference.x, 0f, difference.z);
                        }
                        GetComponent<Camera>().transform.position += scrollVelocity;

                        AdjustToLockArea();
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    Scrolling = false;
                    scrollStart = Vector3.zero;
                }

                if (!Scrolling && scrollVelocity.magnitude > 0)
                {
                    scrollVelocity *= (1f - velocityDecay);
                    GetComponent<Camera>().transform.position += scrollVelocity;
                    AdjustToLockArea();
                }
            }
        }

        private void AdjustToLockArea()
        {
            Vector3 lockedPosition = GetComponent<Camera>().transform.position;
            if (GetComponent<Camera>().transform.position.x < mapLock.GetComponent<Collider>().bounds.min.x)
            {
                lockedPosition = new Vector3(mapLock.GetComponent<Collider>().bounds.min.x, lockedPosition.y, lockedPosition.z);
            }
            if (GetComponent<Camera>().transform.position.x > mapLock.GetComponent<Collider>().bounds.max.x)
            {
                lockedPosition = new Vector3(mapLock.GetComponent<Collider>().bounds.max.x, lockedPosition.y, lockedPosition.z);
            }
            if (GetComponent<Camera>().transform.position.z < mapLock.GetComponent<Collider>().bounds.min.z)
            {
                lockedPosition = new Vector3(lockedPosition.x, lockedPosition.y, mapLock.GetComponent<Collider>().bounds.min.z);
            }
            if (GetComponent<Camera>().transform.position.z > mapLock.GetComponent<Collider>().bounds.max.z)
            {
                lockedPosition = new Vector3(lockedPosition.x, lockedPosition.y, mapLock.GetComponent<Collider>().bounds.max.z);
            }

            if (lockX)
            {
                lockedPosition.x = transform.position.x;
                transform.position = lockedPosition;
            }
            else
            {
                transform.position = lockedPosition;
            }
        }
    }
}