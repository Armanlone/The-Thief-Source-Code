using UnityEngine;
using UnityEngine.UI;

namespace UIManagement
{

    [System.Serializable]
    public class UIOptions
    {

        public GameObject options = null;

        [Space]
        [Header("Components:")]

        public RectTransform rctfMap = null;

        public GameObject btnSounds = null;
        public GameObject btnQuit = null;
        public GameObject btnMenu = null;
        public GameObject btnLeave = null;

        [Tooltip("0 = Menu | 1 = Help | 2 = Levels | 3 = Game | 4 = Warrant")]
        public GameObject[] btnExit;

        public GameObject btnHelp = null;
        public GameObject btnRestart = null;

        public Image imgSounds = null;
        public Sprite sprMute = null, sprtUnmute = null;

        public GameObject imgHelpSection = null, imgWarrantSection = null;
    }

}
