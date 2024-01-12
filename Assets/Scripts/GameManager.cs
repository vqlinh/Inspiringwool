using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public int bestScore = 0; 
    public GameObject wool;
    private int livesCount = 3;
    public GameObject window;
    public GameObject basket;
    public GameObject iconLives;
    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textBestScore; 
    public GameObject livesTransform;
    public GameObject[] iconLivesArray;
    public TextMeshProUGUI textScoreOver;

    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if(instance !=null &&instance !=this){
            Destroy(this);  
        }
        else instance = this;
        
    }

    void Start()
    {
        iconLivesArray = new GameObject[livesCount];
        for (int i = 0; i < livesCount; i++)
        {
            iconLivesArray[i] = Instantiate(iconLives, livesTransform.transform);
        }

        // Lấy best score từ PlayerPrefs khi khởi động game
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        textBestScore.text = bestScore.ToString();
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void SetScore(int point)
    {
        score += point;
        textScore.text = score.ToString();

        if (score > bestScore)
        {
            bestScore = score;
            textBestScore.text = bestScore.ToString();
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save(); 
        }
    }

    public void SetLives()
    {
        if (livesCount > 0)
        {
            livesCount--;
            iconLivesArray[livesCount].SetActive(false);
            Debug.Log(livesCount);
            if (livesCount == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        textScoreOver.text = score.ToString();
        wool.SetActive(false);
        basket.SetActive(false);
        window.SetActive(true);
    }
}
