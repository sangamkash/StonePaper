using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace RockPaperScissors.Rules
{
    [System.Serializable]
    public struct GameRuleData
    {
        public HandGesture HandGesture;
        public List<HandGesture> winAgainst;
    }
    [CreateAssetMenu(fileName = "GameRules", menuName = "ScriptableObjects/GameRules", order = 1)]
    public class GameRules : ScriptableObject
    {
        [SerializeField] private List<GameRuleData> gameRulesChart;
        private Dictionary<HandGesture, HashSet<HandGesture>> gameRules;

        private static GameRules _instance;
        public static GameRules Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance=Resources.Load<GameRules>("GameRules");
                    _instance.Init();
                }
                return _instance;
            }
        }

        private void Init()
        {
            gameRules = new Dictionary<HandGesture, HashSet<HandGesture>>();
            foreach (var ruleData in gameRulesChart)
            {
                if (gameRules.ContainsKey(ruleData.HandGesture))
                {
                    Debug.LogError("Rules are not configure properly");
                }
                else
                {
                    try
                    {
                        gameRules.Add(ruleData.HandGesture, new HashSet<HandGesture>(ruleData.winAgainst));
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"Rules are not configure properly {e}");
                    }
                }
                
            }
        }

        public bool IsWon(HandGesture playerMove,HandGesture aIMove)
        {
            if (gameRules.ContainsKey(playerMove))
            {
                return gameRules[playerMove].Contains(aIMove);
            }

            Debug.LogError("No Rule are set in game rule chart");
            return false;
        }
    }
}