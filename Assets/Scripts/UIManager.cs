using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    
    public GameObject splashScreen;
    public GameObject mainMenuScreen;
    public GameObject loadingScreen;
    public GameObject gamePlayScreen;
    public GameObject optionScreen;
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public Text scoreBoard;
    public GameObject player;
    public int NewScore = 0;
    public PlayerController playerControll;
    public GameObject[] collectibles;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(DelayScreenStatus(splashScreen , 2 , mainMenuScreen));
        //MainMenuScreenStatus(true);
        //GamePlayScreenStatus(false);
        Time.timeScale = 0;


    }
    public void PlayButton()
    {
        StartCoroutine(DelayScreenStatus(loadingScreen , 2 ,gamePlayScreen));
        MainMenuScreenStatus(false);
        //SplashScreenStatus(false);
    }
    
    public void PauseButton()
    {
        Time.timeScale = 0;
        PauseScreenStatus(true);
        GamePlayScreenStatus(false);       
    }
    public void ResumeButton()
    {
        Time.timeScale = 1;
        GamePlayScreenStatus(true);
        PauseScreenStatus(false);
    }
    public void OptionButton()
    {
        MainMenuScreenStatus(false);
        OptionScreenStatus(true);
    }

    public void OptionBackButton()
    {
        MainMenuScreenStatus(true);
        OptionScreenStatus(false);
    }
    public void RestartButton()
    {
        scoreBoard.text = "Score : " + NewScore.ToString(); 
        player.transform.position = new Vector3(0, 0.5f, 0);
        playerControll.GetComponent<Rigidbody>().isKinematic = false;
        for (int i = 0; i < collectibles.Length; i++)
        {
            if (!collectibles[i].activeSelf)
            {
                collectibles[i].SetActive(true);
            }
        }


        playerControll.ResetEnvironmentMaterialsColor();
        
        Time.timeScale = 1;

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        GamePlayScreenStatus(true);
        playerControll.SetCountText();
        playerControll._joystick.ResetJoystickPosition();
        GameOverScreenStatus(false);
    }
    public void RecordButton()
    {
        Debug.Log("Records");
    }
    public void ExitButton()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void BackButton()
    {
        OptionScreenStatus(false);
        MainMenuScreenStatus(true);
    }
    public void GameOverScreenStatus(bool status)
    {
        gameOverScreen.SetActive(status);
    }
    public void SplashScreenStatus(bool status)
    {
        splashScreen.SetActive(status);
    }
    public void MainMenuScreenStatus(bool status)
    {
        mainMenuScreen.SetActive(status);
    }

    public void LoadingScreenStatus(bool status)
    {
        loadingScreen.SetActive(status);

    }

    public void GamePlayScreenStatus(bool status)
    {
        gamePlayScreen.SetActive(status);
    }
    public void PauseScreenStatus(bool status)
    {
        pauseScreen.SetActive(status);
    }

    public void OptionScreenStatus(bool status)
    {
        optionScreen.SetActive(status);
    }

   
    public IEnumerator DelayScreenStatus(GameObject screen, int time, GameObject next)
    {
        screen.SetActive(true);
        yield return new WaitForSecondsRealtime(time);
        screen.SetActive(false);

        if(screen.name == loadingScreen.name)
        {
            Time.timeScale = 1;
        }
        next.SetActive(true);
    }
   
}
