using UnityEngine;

namespace UIManagement
{

    [System.Serializable]
    public class UILevelSelect
    {

        public GameObject levels = null;

        [Space]
        [Header("Components:")]

        public RectTransform[] rctfRooms = null;
        public RectTransform rctfMiniMap = null;
    }

}
