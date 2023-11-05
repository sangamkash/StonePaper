using System.Collections;
using System.Collections.Generic;
using RockPaperScissors.Rules;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RockPaperScissors
{
    [System.Serializable]
    public enum HandGesture
    {
        Rock,
        Paper,
        Scissor,
        Lizard,
        Spock
    }
    public class GameManager : MonoBehaviour
    {
        private int score;
        [SerializeField] private float timerLimit=1f;
        [SerializeField] private TextMeshProUGUI aiMoveText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject startScreen;
        [SerializeField] private GameObject gamePlayScreen;
        [SerializeField] private Image timerImg;
        [SerializeField] private TextMeshProUGUI endScreenScore;
        private HandGesture currentAiMove;
        
        public void HideStartScreen()
        {
            score = 0;
            startScreen.SetActive(false);
            gamePlayScreen.SetActive(true);
            PlayGame();
        }

        private void PlayGame()
        {
            StopAllCoroutines();
            PickRandomAIMove();
            scoreText.text = $"Score\n{score}";
            StartCoroutine(StartTimer(timerLimit));
        }

        private void PickRandomAIMove()
        {
            currentAiMove = (HandGesture)(Random.Range(0, 5));
            aiMoveText.text = $"Computer Move\n\n{currentAiMove}";
        }
        
        private IEnumerator StartTimer(float time)
        {
            var startTime = Time.time;
            var endTime = startTime + time;
            while (Time.time < endTime)
            {
                var fillAmount = (Time.time-startTime) / time;
                timerImg.fillAmount = 1f - fillAmount;
                yield return null;
            }
            timerImg.fillAmount = 0;
            EndGame();
        }

        private void EndGame()
        {
            StopAllCoroutines();
            endScreenScore.text = $"You score\n{score}";
            startScreen.SetActive(true);
        }

        public void OnMoveSelect(int handGesture)
        {
            if (GameRules.Instance.IsWon((HandGesture)handGesture, currentAiMove))
            {
                score++;
                PlayGame();
            }
            else
            {
                EndGame();
            }
        }
    }
}
