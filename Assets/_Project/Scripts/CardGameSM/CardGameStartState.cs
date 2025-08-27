using redd096.StateMachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace cg
{
    /// <summary>
    /// The first state of the StateMachine, where check every manager is initialized
    /// </summary>
    [System.Serializable]
    public class CardGameStartState : IState<CardGameSM>
    {
        [SerializeField] private GameObject loadingPanel;
        [SerializeField] private GameObject loadingObj;
        [SerializeField] private GameObject errorObj;
        [Space]
        [SerializeField] private TMP_Text loadingLabel;
        [SerializeField] private Button backOnErrorButton;
        [SerializeField] private TMP_Text errorLabel;
        [Space]
        [SerializeField] private GameObject gamePanel;

        //consts
        public const float LOADING_ANIMATION_DELAY = 0.3f;
        public const float MINIMUM_TIME_LOADING = 1f;

        public CardGameSM StateMachine { get; set; }

        //example of defaultText: Loading<color=#0000>.</color><color=#0000>.</color><color=#0000>.</color>
        private string defaultText;
        private float animationTimer;
        private float minTimer;

        public void Enter()
        {
            //reset vars
            animationTimer = Time.time + LOADING_ANIMATION_DELAY;
            minTimer = Time.time + MINIMUM_TIME_LOADING;
            errorLabel.text = "";

            //register events
            backOnErrorButton.onClick.AddListener(OnClickBackOnError);

            //show loading
            ShowObj(showLoading: true);
            gamePanel.SetActive(false);

            //show panel and save default text (with transparent points)
            loadingPanel.SetActive(true);
            defaultText = loadingLabel.text;
        }

        public void UpdateState()
        {
            DoAnimation();

            //wait minimum time, then continue to call function
            if (Time.time > minTimer)
            {
                OnCompleteMinimumTime();
            }
        }

        public void Exit()
        {
            //unregister events
            backOnErrorButton.onClick.RemoveListener(OnClickBackOnError);

            //hide panel and reset text
            loadingPanel.SetActive(false);
            loadingLabel.text = defaultText;
        }

        private void DoAnimation()
        {
            //animation: every few seconds add one point "." 
            //(by removing transparency to avoid resize text) 
            //and when reach last, restart
            if (Time.time > animationTimer)
            {
                animationTimer = Time.time + LOADING_ANIMATION_DELAY;

                if (loadingLabel.text.Contains("<color=#0000>"))
                {
                    //remove transparency
                    string t = loadingLabel.text;
                    t = t.Remove(t.IndexOf("<color=#0000>"), "<color=#0000>".Length);
                    t = t.Remove(t.IndexOf("</color>"), "</color>".Length);
                    loadingLabel.text = t;
                }
                else
                {
                    //if every dot is visible, return transparency to every dot
                    loadingLabel.text = defaultText;
                }
            }
        }

        private void OnClickBackOnError()
        {
            //exit and re-enter in this state
            StateMachine.SetState(StateMachine.StartState);
        }

        /// <summary>
        /// Show LoadingObj or ErrorObj
        /// </summary>
        /// <param name="showLoading"></param>
        private void ShowObj(bool showLoading)
        {
            loadingObj.SetActive(showLoading);
            errorObj.SetActive(showLoading == false);
        }

        private void OnCompleteMinimumTime()
        {
            string error = "CardGameManager isn't in scene!";
            bool cardGameManagerInitialized = CardGameManager.instance && CardGameManager.instance.IsCorrect(out error);

            //if everything is initialized, start game
            if (cardGameManagerInitialized)
            {
                StateMachine.SetState(StateMachine.DrawStartingCardsState);
                gamePanel.SetActive(true);
            }
            //else show error
            else
            {
                errorLabel.text = error;
                ShowObj(showLoading: false);
            }
        }
    }
}