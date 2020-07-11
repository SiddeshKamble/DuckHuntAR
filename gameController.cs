using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;

public class gameController : DefaultTrackableEventHandler
{
    public static gameController instance;

    public int playerScore = 0;
    public Text scoreCountText;
    public Text livesCountText;
    public int round = 1;
    public Text roundTextTargetText;
    public Text GameOverScoreText;
    public Text roundTextNumber;
    public int shotsPerRound = 3;
    private int lives = 2;
    public GameObject[] shells;

    //Show Hide GameObjects
    public GameObject GUIScoreText;
    public GameObject GUILivesText;
    public GameObject GUICenterTarget;
    public GameObject GUIFireButton;
    public GameObject GUIDog;
    public GameObject GUIRoundText;
    public GameObject GUIGameOverPanel;
    public GameObject startPanel;
    public GameObject Terrain;
    public GameObject GUIGun;

    AudioSource audio;
    public AudioClip[] clips;

    //RoundRules
    private int roundTargetScore = 3;
    public int roundScore = 0;
    private int scoreIncrement = 2;
    public bool playerStarted = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        playerScore = int.Parse(scoreCountText.text);
        showStartPanel();
        audio = GetComponent<AudioSource>();
        livesCountText.text = lives.ToString();
    }

    private void playFX(int sound) {
        audio.clip = clips[sound];
        audio.Play();
    }



    // Update is called once per frame
    void Update()
    {
        if (DefaultTrackableEventHandler.trueFalse == true) {
            hideStartPanel();
            showItems();
            if (playerStarted == false) {
                StartCoroutine(playRound());
            }
            playerStarted = true;
        }
        else {
            showStartPanel();
            hideItems();
        }
        if (roundScore == roundTargetScore) {
            playFX(0);
            StartCoroutine(newRound());
            roundScore = 0;
            roundTargetScore = roundTargetScore + scoreIncrement;
       
        }

        if (shotsPerRound == 0) {
            shells[0].SetActive(false);
            StartCoroutine(loseLife());
            shotsPerRound = 3;
           }
        scoreCountText.text = playerScore.ToString();
    }
    public void showItems() {
        GUIScoreText.SetActive(true);
        GUILivesText.SetActive(true);
        GUICenterTarget.SetActive(true);
        GUIFireButton.SetActive(true);
        Terrain.SetActive(true);
        GUIGun.SetActive(true);
        showShells();

    }

    public void hideItems() {
        GUIScoreText.SetActive(false);
        GUILivesText.SetActive(false);
        GUICenterTarget.SetActive(false);
        GUIFireButton.SetActive(false);
        Terrain.SetActive(false);
        GUIGun.SetActive(false);
    }

    public IEnumerator playRound()
    {
        yield return new WaitForSeconds(2f);
        roundTextTargetText.text = "SHOOT " + roundTargetScore + " DUCKS";
        GUIRoundText.SetActive(true);
        playFX(0);
        StartCoroutine(hideRoundText());
    }

    private IEnumerator newRound() {
        yield return new WaitForSeconds(3);
        round++;
        GUIRoundText.SetActive(true);
        roundTextTargetText.text = "SHOOT " +roundTargetScore + " DUCKS";
        roundTextNumber.text = round.ToString(); 
        StartCoroutine(hideRoundText());
    }

    private IEnumerator hideRoundText() {
        yield return new WaitForSeconds (4f);
        GUIRoundText.SetActive(false);
    }

    public void showShells() {
        if (shotsPerRound == 3)
        {
            shells[0].SetActive(true);
            shells[1].SetActive(true);
            shells[2].SetActive(true);
        }
        else if (shotsPerRound == 2)
        {
            shells[0].SetActive(true);
            shells[1].SetActive(true);
            shells[2].SetActive(false);
        }
        else if (shotsPerRound == 1)
        {
            shells[0].SetActive(true);
            shells[1].SetActive(false);
            shells[2].SetActive(false);
        }
       /* else {
            shells[0].SetActive(false);
            shells[1].SetActive(false);
            shells[2].SetActive(false);
        }*/

    }


    private void showStartPanel(){
	startPanel.SetActive (true);
	}
	
	private void hideStartPanel(){
	startPanel.SetActive (false);
	}

    private IEnumerator loseLife() {
        
        lives--;
        if (lives == 0)
        {
            GUIFireButton.SetActive(false);
            playFX(1);
            GUIGameOverPanel.SetActive(true);
            GameOverScoreText.text = playerScore.ToString();
            lives = 0;
        }
        else {
            GUIFireButton.SetActive(false);
            playFX(2);
            GUIDog.SetActive(true);
            yield return new WaitForSeconds(3);
            GUIDog.SetActive(false);
            GUIFireButton.SetActive(true);
            shotsPerRound = 3;
        }
        yield return new WaitForSeconds(0);
        livesCountText.text = lives.ToString();
    }

    public void restart() {
        hideItems();
        lives = 2;
        livesCountText.text = lives.ToString();
        playerScore = 0;
        scoreCountText.text = playerScore.ToString();
        roundTargetScore = 3;
        roundScore = 0;
        GameOverScoreText.text = "0";
        round = 1;
        roundTextNumber.text = round.ToString();
        GUIGameOverPanel.SetActive(false);
        StartCoroutine(playRound());
    }

    public void quit()
    {
        //Application.Quit();
        SceneManager.LoadScene("intro");

    }



}

