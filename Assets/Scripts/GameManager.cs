using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public AudioSource gameMusic;

    private GroundPiece[] allGroundPieces;
    // Start is called before the first frame update
    void Start()
    {
        SetupNewLevel();
        DontDestroyOnLoad(gameMusic);
    }

    private void SetupNewLevel()
    {
        gameMusic.Play();
        allGroundPieces = FindObjectsOfType <GroundPiece>();
    }

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        SetupNewLevel();
    }

    public void CheckComplete()
    {
        bool isFinished = true;

        for (int i = 0; i < allGroundPieces.Length; i++)
        {
            if (allGroundPieces[i].isColoured == false)
            {
                isFinished = false;
                break;
            }
           

        }

        if (isFinished)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < (SceneManager.sceneCountInBuildSettings - 1))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            Debug.Log("Game Over");

    }

    // Update is called once per frame

    void ShowGameOver()
    {
    }

    void Update()
    {
        
    }
}
