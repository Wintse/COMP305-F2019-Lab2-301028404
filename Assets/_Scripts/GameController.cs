using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;
/// <summary>
/// Victoria Liu
/// 301028404
/// </summary>

public class GameController : MonoBehaviour
{
    [Header("Scene Game Objects")]
    public GameObject cloud;
    public GameObject island;
    public int numberOfClouds;
    public List<GameObject> clouds;

    [Header("Audio Sources")]
    public SoundClip activeSoundClip;
    public AudioSource[] audioSources;

    [Header("Scoreboard")]
    [SerializeField]
    private int _lives;

    [SerializeField]
    private int _score;


    public Text livesLabel;
    public Text scoreLabel;
    public Text highScoreLabel;

    //public HighScoreSO highScoreSO;



    [Header("UI Control")]
    public GameObject startLabel;
    public GameObject startButton;
    public GameObject endLabel;
    public GameObject restartButton;

    [Header("Game Settings")]
    public ScoreBoard scoreBoard;

    [Header("Scene Settings")]
    public SceneSettings activeSceneSettings;
    public List<SceneSettings> sceneSettings;

    // public properties
    public int Lives
    {
        get
        {
            return _lives;
        }

        set
        {
            _lives = value;
            scoreBoard.lives = _lives;
            if (_lives < 1)
            {

                SceneManager.LoadScene("End");
            }
            else
            {
                livesLabel.text = "Lives: " + _lives;
            }

        }
    }

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
            scoreBoard.score = _score;

            if (scoreBoard.highScore < _score)
            //if (highScoreSO.score < _score)
            {
                scoreBoard.highScore = _score;

            }
            scoreLabel.text = "Score: " + _score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObjectInitialization();
        SceneConfiguration();
    }

    private void GameObjectInitialization()
    {
        //scoreBoard = GameObject.Find("ScoreBoard");

        startLabel = GameObject.Find("StartLabel");
        endLabel = GameObject.Find("EndLabel");
        startButton = GameObject.Find("StartButton");
        restartButton = GameObject.Find("RestartButton");

        //scoreBoard = ReSources.FindObjectOfTypeAll<ScoreBoard>()[0] as ScoreBoard;
        //highScoreSO = Resources.FindObjectsOfTypeAll<HighScoreSO>()[0] as HighScoreSO;
    }


    private void SceneConfiguration()
    {
        //selects the current scene
        Scene sceneToCompare = (Scene) Enum.Parse(typeof(Scene),
                        SceneManager.GetActiveScene().name.ToUpper());

        //compares the current scene with the setting list
        var query = from settings in sceneSettings
                where settings.scene == sceneToCompare
                select settings;

        //query the list and find the first match sets it to the correct scene
        activeSceneSettings = query.ToList().First();
        
        {
            //chekcis main scene is active and sets up the initial values for score and lives
            if (activeSceneSettings.scene == Scene.MAIN)
	        {
                Lives = 5;
                Score = 0;
	        }
            
            //appplies all scenesettings from the scriptable object
            activeSoundClip = activeSceneSettings.activeSoundClip;;  
            scoreLabel.enabled = activeSceneSettings.scoreLabelActive;
            livesLabel.enabled = activeSceneSettings.LivesLabelActive;
            highScoreLabel.enabled = activeSceneSettings.highScoreLabelActive;
            startLabel.SetActive(activeSceneSettings.startLabelActive);
            endLabel.SetActive(activeSceneSettings.endLabelActive);
            startButton.SetActive(activeSceneSettings.startButtonActive);
            restartButton.SetActive(activeSceneSettings.restartButtonActive); 

            //score values being assigned
            highScoreLabel.text = "High Score: " + scoreBoard.highScore;
            livesLabel.text = "Lives: " + scoreBoard.lives;
            scoreLabel.text = "Score: " + scoreBoard.score;
        }


        if ((activeSoundClip != SoundClip.NONE) && (activeSoundClip != SoundClip.NUM_OF_CLIPS))
        {
            AudioSource activeAudioSource = audioSources[(int)activeSoundClip];
            activeAudioSource.playOnAwake = true;
            activeAudioSource.loop = true;
            activeAudioSource.volume = 0.5f;
            activeAudioSource.Play();
        }



        // creates an empty container (list) of type GameObject
        clouds = new List<GameObject>();

        for (int cloudNum = 0; cloudNum < numberOfClouds; cloudNum++)
        {
            clouds.Add(Instantiate(cloud));
        }

        Instantiate(island);
    }


    // Event Handlers
    public void OnStartButtonClick()
    {
        DontDestroyOnLoad(scoreBoard);
        SceneManager.LoadScene("Main");
    }

    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene("Main");
    }
}
