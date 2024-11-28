using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI scoreTXT;
    private static float score = 0;
    private int enemyKills = 0;

    private void Awake()
    {
        #region SINGLETON
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }
    private void Start()
    {
        score = 0;
    }
    private void Update()
    {
        scoreTXT.text = score.ToString();
    }
    public void IncrementScore(float value)
    {
        score += value;
        scoreTXT.GetComponent<Animator>().SetTrigger("ScoreChanged");
    }
    public void IncrementEnemyKillCount()
    {
        enemyKills++;
    }
    public float GetScore()
    {
        return score;
    }
    public int GetEnemyKillCount()
    {
        return enemyKills;
    }
   

}
