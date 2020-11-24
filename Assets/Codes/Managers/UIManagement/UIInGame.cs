using UnityEngine;
using TMPro;

namespace UIManagement
{

    [System.Serializable]
    public class UIInGame
    {

        public GameObject game = null;

        [Space]
        [Header("Components:")]

        public TextMeshProUGUI txtScore = null;

        [Tooltip("0 = Canvas Bounty | 1 = Sprite Bounty")]
        public RectTransform[] rctfBounty = null;
        public RectTransform rctfScore = null;
        public RectTransform rctfMiniMap = null;

    }

}
