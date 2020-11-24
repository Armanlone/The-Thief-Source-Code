using UnityEngine;

namespace UIManagement
{

    [System.Serializable]
    public class UITransition
    {

        public GameObject transition = null;

        [Space]
        [Header("Components:")]

        public CanvasGroup cnvgPanel = null;

        public RectTransform rctfTransition = null;

    }

}
