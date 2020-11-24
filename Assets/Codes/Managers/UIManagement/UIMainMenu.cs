using UnityEngine;

namespace UIManagement
{

    [System.Serializable]
    public class UIMainMenu
    {

        public GameObject menu = null;

        [Space]
        [Header("Components:")]

        public RectTransform rctfTitle = null;
        public RectTransform rctfTitleMap = null;
        public RectTransform rctfPressSpace = null;
        public RectTransform rctfMiniMap = null;

    }

}
