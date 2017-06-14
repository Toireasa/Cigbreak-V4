using UnityEngine;
using System.Collections;
using UnityEngine.Events;

namespace CigBreakGUI
{
    public class YouWinAnimReceiver : MonoBehaviour
    {
        public UnityAction OnCompleted { get; set; }

        public void OnAnimationCompleted()
        {
            if(OnCompleted != null)
            {
                OnCompleted();
            }
        }
    } 
}
