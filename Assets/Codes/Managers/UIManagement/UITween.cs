using DG.Tweening;
using UnityEngine;
using System.Collections;

namespace UIManagement
{

    public class UITween : MonoBehaviour
    {

        private static UITween INSTANCE = null;

        [SerializeField]
        private UIMainMenu menu = null;

        [SerializeField]
        private UILevelSelect levels = null;

        [SerializeField]
        private UIOptions options = null;

        [SerializeField]
        private UIInGame game = null;

        [SerializeField]
        private UITransition transition = null;

        [SerializeField]
        private UICredits credits = null;

        private const float DURATION = 1f;

        private void Awake()
        {

            if (INSTANCE != null && INSTANCE != this)
            {
                Destroy(gameObject);
                return;
            }

            INSTANCE = this;

            Debug.Log("UI Tween created.");

        }

        //Sets the tween capacity.
        public static void SetTweenCapacity()
        {
            DOTween.SetTweensCapacity(1000, 1000);
        }

        #region Transition

        //Slides the rctfTransition.
        public static void rctfTransitionSlide(float posY, float delay)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.transition == null)
                return;

            else if (INSTANCE.transition.rctfTransition == null)
                return;

            else
                INSTANCE.transition.rctfTransition.DOAnchorPosY(posY, DURATION).SetDelay(delay);

        }

        //Fades the cnvgPanel
        public static void cnvgPanelFade(float alpha, float duration)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.transition == null)
                return;

            else if (INSTANCE.transition.cnvgPanel == null)
                return;

            else
            {
                INSTANCE.transition.cnvgPanel.DOFade(alpha, duration);
            }

        }

        #endregion

        #region Menu

        //Slides in the menu.
        public static void menuSlideIn()
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.menu == null)
                return;

            else if (INSTANCE.menu.rctfMiniMap == null || INSTANCE.menu.rctfPressSpace == null || INSTANCE.menu.rctfTitleMap == null || INSTANCE.menu.rctfTitle == null)
                return;

            else
            {
                INSTANCE.menu.rctfMiniMap.DOAnchorPosY(10f, DURATION).SetDelay(0.95f);
                INSTANCE.menu.rctfPressSpace.DOAnchorPosY(10f, DURATION).SetDelay(1.25f);
                INSTANCE.menu.rctfTitleMap.DOAnchorPosY(17f, DURATION).SetDelay(1.35f);
                INSTANCE.menu.rctfTitle.DOAnchorPosY(17f, DURATION).SetDelay(1.45f);
            }

        }

        //Slides out the menu.
        public static void menuSlideOut()
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.menu == null)
                return;

            else if (INSTANCE.menu.rctfMiniMap == null || INSTANCE.menu.rctfPressSpace == null || INSTANCE.menu.rctfTitleMap == null || INSTANCE.menu.rctfTitle == null)
                return;

            else
            {
                INSTANCE.menu.rctfTitle.DOAnchorPosY(517f, DURATION);
                INSTANCE.menu.rctfTitleMap.DOAnchorPosY(517f, DURATION).SetDelay(0.25f);
                INSTANCE.menu.rctfPressSpace.DOAnchorPosY(510f, DURATION).SetDelay(0.50f);
                INSTANCE.menu.rctfMiniMap.DOAnchorPosY(510f, DURATION).SetDelay(0.65f);
            }

        }

        #endregion

        #region Levels

        //Slides in the level select.
        public static void levelsSlideIn()
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.levels == null)
                return;

            else if (INSTANCE.levels.rctfMiniMap == null)
                return;

            else
                INSTANCE.levels.rctfMiniMap.DOAnchorPosY(10, DURATION).SetDelay(0.95f);

            INSTANCE.btnRoomsSlide(-48f, 1.25f, 32f, 19, -1);

        }

        //Slides out the level select.
        public static void levelsSlideOut()
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.levels == null)
                return;

            INSTANCE.btnRoomsSlide(548f, 0f, -32f, 0, 1);

            if (INSTANCE.levels.rctfMiniMap == null)
                return;

            INSTANCE.levels.rctfMiniMap.DOAnchorPosY(510f, DURATION).SetDelay(0.95f);

        }

        //Loops through all of the buttons at Level Select region.
        private void btnRoomsSlide(float posY, float delay, float posYAdditive, int x, int xAdditive)
        {

            for (int i = 0; i < 4; i++)
            {

                for (int j = 0; j < 5; j++)
                {

                    if (INSTANCE.levels.rctfRooms[x] == null)
                        return;

                    else
                    {
                        INSTANCE.levels.rctfRooms[x].DOAnchorPosY(posY, DURATION).SetDelay(delay);
                        x += xAdditive;
                    }

                }

                posY += posYAdditive;
                delay += 0.1f;

            }

        }

        #endregion

        #region Game

        //Set the UI's txtScore.
        public static void UpdateTextScore(int score)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.game == null)
                return;

            else if (INSTANCE.game.txtScore == null)
                return;

            else
            {
                //Updates the score.
                INSTANCE.game.txtScore.text = score.ToString();

                //Punches the sack.
                INSTANCE.rctfBountyPunch();
            }
        }

        //Punch the rctfBounty.
        private void rctfBountyPunch()
        {

            if (game == null)
                return;

            else if (game.rctfBounty[1] == null)
                return;

            else
            {

                Vector3 punch = new Vector3(0.5f, 0.5f, 0.5f);
                float duration = 0.25f;
                int vibrato = 5;
                float elasticity = 0.5f;

                game.rctfBounty[1].transform.DORewind();
                game.rctfBounty[1].transform.DOPunchScale(punch, duration, vibrato, elasticity);

            }

        }

        //Slides in the in-game.
        public static void gameSlideIn()
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.game == null)
                return;

            else
            {
                INSTANCE.game.rctfMiniMap.DOAnchorPosY(10f, DURATION).SetDelay(0.95f);
                INSTANCE.game.rctfScore.DOAnchorPosY(-52f, DURATION).SetDelay(1.25f);
                INSTANCE.game.rctfBounty[0].DOAnchorPosY(-8f, DURATION).SetDelay(1.35f);
            }

        }

        //Slides out the in-game.
        public static void gameSlideOut()
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.game == null)
                return;

            else
            {
                INSTANCE.game.rctfBounty[0].DOAnchorPosY(492f, DURATION);
                INSTANCE.game.rctfScore.DOAnchorPosY(448f, DURATION).SetDelay(0.25f);
                INSTANCE.game.rctfMiniMap.DOAnchorPosY(474f, DURATION).SetDelay(0.5f);
            }

        }

        #endregion

        #region Options

        //Change the sprite of imgSounds.
        public static void imgSoundsChangeSprite(bool isMute)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.imgSounds == null)
                return;

            else
            {

                INSTANCE.options.imgSounds.sprite = isMute ? INSTANCE.options.sprMute : INSTANCE.options.sprtUnmute;
                INSTANCE.options.imgSounds.preserveAspect = true;

            }

        }

        //Slides the rctfMap.
        public static void rctfMapSlide(float posY, float delay)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.rctfMap == null)
                return;

            else
                INSTANCE.options.rctfMap.DOAnchorPosY(posY, DURATION).SetDelay(delay);

        }

        #endregion

        #region Credits

        //Fade the credits and go back to main menu.
        public static void LoadingCredits(float fadeSpeed)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.credits == null)
                return;

            else if (INSTANCE.credits.credits == null)
                return;

            else if (INSTANCE.credits.cnvgImage == null || INSTANCE.credits.imgCredits == null)
                return;

            else
                INSTANCE.StartCoroutine(INSTANCE.EnumCreditsFade(fadeSpeed));

        }

        //Enumerator for fading in/out credits.
        private IEnumerator EnumCreditsFade(float fadeSpeed)
        {

            WaitForSeconds wait = new WaitForSeconds(2f);

            yield return wait;

            creditsActive(true);

            //Fade in and out of the credits.
            for (byte i = 0; i < credits.sprCredits.Length; i++)
            {

                if (credits.sprCredits[i] == null)
                    yield return null;

                else
                {

                    //Set the alpha to 0 and set the image to the element in sprCredits array.
                    //credits.cnvgImage.alpha = 0;
                    credits.imgCredits.sprite = credits.sprCredits[i];

                    //Fade in
                    credits.cnvgImage.DOFade(1, fadeSpeed);

                    //Wait...
                    yield return wait;

                    if (i == credits.sprCredits.Length - 1)
                        yield return new WaitForSeconds(4f);

                    //Fade out
                    credits.cnvgImage.DOFade(0, fadeSpeed);

                }

            }

            yield return wait;

            creditsActive(false);

            //Go to main menu.
            RoomManager.LoadRoom(RoomManager.GetMainMenuIndex());
            UIManager.CallClickMenuExit();

        }

        #endregion

        #region Enable/Disable Transition

        //Enable/Disable the transition.
        public static void transitionActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.transition == null)
                return;

            else if (INSTANCE.transition.transition == null)
                return;

            else
            {

                if (isActive)
                    INSTANCE.transitionEnable();

                else
                    INSTANCE.methodDisableDelayed("transitionDisable");

            }

        }

        private void transitionEnable() => transition.transition.SetActive(true);

        private void transitionDisable() => transition.transition.SetActive(false);

        #endregion

        #region Enable/Disable Menu

        //Enable/Disable the menu.
        public static void menuActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.menu == null)
                return;

            else if (INSTANCE.menu.menu == null)
                return;

            else
            {

                if (isActive)
                    INSTANCE.menuEnable();

                else
                    INSTANCE.methodDisableDelayed("menuDisable");

            }

        }

        //Getter for menu's active state.
        public static bool IsMenuActive()
        {
            return INSTANCE.menu.menu.activeInHierarchy;
        }

        private void menuEnable() => menu.menu.SetActive(true);

        private void menuDisable() => menu.menu.SetActive(false);

        #endregion

        #region Enable/Disable Levels

        //Enable/Disable the levels.
        public static void levelsActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.levels == null)
                return;

            else if (INSTANCE.levels.levels == null)
                return;

            else
            {

                if (isActive)
                    INSTANCE.levelsEnable();

                else
                    INSTANCE.methodDisableDelayed("levelsDisable");

            }

        }

        private void levelsEnable() => levels.levels.SetActive(true);

        private void levelsDisable() => levels.levels.SetActive(false);

        #endregion

        #region Enable/Disable Game

        //Enable/Disable the game.
        public static void gameActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.game == null)
                return;

            else if (INSTANCE.game.game == null)
                return;

            else
            {

                if (isActive)
                    INSTANCE.gameEnable();

                else
                    INSTANCE.methodDisableDelayed("gameDisable");

            }

        }

        private void gameEnable() => game.game.SetActive(true);

        private void gameDisable() => game.game.SetActive(false);

        #endregion

        #region Enable/Disable Options

        //Enable/Disable the options menu.
        public static void optionsMenuActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.btnExit[0] == null || INSTANCE.options.btnHelp == null || INSTANCE.options.btnQuit == null)
                return;

            else
            {

                INSTANCE.options.btnExit[0].SetActive(isActive);
                INSTANCE.options.btnHelp.SetActive(isActive);
                INSTANCE.options.btnQuit.SetActive(isActive);

            }
        }

        //Enable/Disable the options levels.
        public static void optionsLevelsActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.btnExit[2] == null || INSTANCE.options.btnMenu == null)
                return;

            else
            {

                INSTANCE.options.btnExit[2].SetActive(isActive);
                INSTANCE.options.btnMenu.SetActive(isActive);

            }

        }

        //Enable/Disable the options game.
        public static void optionsGameActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.btnExit[3] == null || INSTANCE.options.btnRestart == null || INSTANCE.options.btnLeave == null)
                return;

            else
            {

                INSTANCE.options.btnExit[3].SetActive(isActive);
                INSTANCE.options.btnRestart.SetActive(isActive);
                INSTANCE.options.btnLeave.SetActive(isActive);

            }

        }

        //Enable/Disable the help section.
        public static void helpSectionActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.imgHelpSection == null || INSTANCE.options.btnExit[1] == null)
                return;

            else
            {

                INSTANCE.options.btnExit[1].SetActive(isActive);
                INSTANCE.options.imgHelpSection.SetActive(isActive);

            }

        }

        //Enable/Disable the warrant section.
        public static void warrantSectionActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.imgWarrantSection == null || INSTANCE.options.btnExit[4] == null )
                return;

            else
            {

                INSTANCE.options.btnExit[4].SetActive(isActive);
                INSTANCE.options.imgWarrantSection.SetActive(isActive);

            }

        }

        //Enable/Disable the btnSounds separately.
        public static void btnSoundsActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else if (INSTANCE.options.btnSounds == null)
                return;

            else
                INSTANCE.options.btnSounds.SetActive(isActive);

        }

        //Enable/Disable the options.
        public static void optionsActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.options == null)
                return;

            else
            {

                if (isActive)
                    INSTANCE.optionsEnable();

                else
                    INSTANCE.methodDisableDelayed("optionsDisable");

            }

        }

        private void optionsEnable() => options.options.SetActive(true);

        private void optionsDisable() => options.options.SetActive(false);

        #endregion

        #region Enable/Disable Credits

        //Enable/Disable the credits.
        public static void creditsActive(bool isActive)
        {

            if (INSTANCE == null)
                return;

            else if (INSTANCE.credits == null)
                return;

            else if (INSTANCE.credits.credits == null)
                return;

            else if (INSTANCE.credits.cnvgImage == null)
                return;

            else
            {

                if (isActive)
                    INSTANCE.creditsEnable();

                else
                    INSTANCE.methodDisableDelayed("creditsDisable");

            }

        }

        private void creditsEnable() => credits.credits.SetActive(true);

        private void creditsDisable() => credits.credits.SetActive(false);


        #endregion

        //For disabling a certain method with a delay.
        private void methodDisableDelayed(string methodName)
        {
            Invoke(methodName, DURATION);
        }
    }

}