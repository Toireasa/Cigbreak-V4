using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections.Generic;

public class RemovableObject : MonoBehaviour
{
    [SerializeField]
    List<GameObject> m_OjectToActivate = new List<GameObject>();

   public UnityAction OnButtonClicked { get; set; }

    private void OnMouseUpAsButton()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if(OnButtonClicked != null)
            {
                OnButtonClicked();
            }
            gameObject.SetActive(false);

            if (m_OjectToActivate.Count>0)
            {
                for(int i = 0; i<m_OjectToActivate.Count; i++)
                m_OjectToActivate[i].SetActive(true);
            }
        }
    }
}
