using UnityEngine;
using UnityEngine.UI;

namespace UIManagement
{

	[System.Serializable]
	public class UICredits
	{

		public GameObject credits = null;

        [Space]
        [Header("Components:")]

        public Image imgCredits = null;
		public Sprite[] sprCredits;
		public CanvasGroup cnvgImage = null;

	}

}