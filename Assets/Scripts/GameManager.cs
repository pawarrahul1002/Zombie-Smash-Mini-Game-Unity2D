using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*Game manager class manage all the zombie movements, score contolling,
 health system, scene management for menu and restart scene*/

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject[] zombies;
    private bool isRising = false;
    private bool isFalling = false;
    private int activeZombieIndex;
    private Vector2 startPos;
    private int zombieSmashed;
    private int livesRemain;
    private bool gameOver;

    [SerializeField] Image[] heart;
    public Text scoreText;
    public Text highScoreText;
    public Button gameOverButton;
    [SerializeField] public float riseSpeed = 1f;
    [SerializeField] public int scoreThreshold = 5;
    public string gameScene;
    public string mainMenuScene;


    void Start()
    {
        gameOver = false;
        zombieSmashed = 0;
        scoreText.text = zombieSmashed.ToString() + '0';
        livesRemain = 3;
        pickNewZombie();
    }

    // Update is controlling zombie movement
    void Update()
    {
        if (gameOver == false)
        {
            ZombieMovement();
        }

    }

    /* moving zombie in upword and downword with transform.translate and vector2.up and vector2.down
    and checks if distance difference of start pos and current pos is zero then will reduce life
    and also pick new zombie  */

    private void ZombieMovement()
    {
        if (isRising)
        {
            zombies[activeZombieIndex].transform.Translate(Vector2.up * Time.deltaTime * riseSpeed);
            if (zombies[activeZombieIndex].transform.position.y - startPos.y >= 3f)
            {
                isRising = false;
                isFalling = true;
            }
        }
        else if (isFalling)
        {
            if (zombies[activeZombieIndex].transform.position.y - startPos.y <= 0f)
            {
                isRising = false;
                isFalling = false;
                livesRemain--;
                updateLifeUI();
            }
            else
            {
                zombies[activeZombieIndex].transform.Translate(Vector2.down * Time.deltaTime * riseSpeed);
            }
        }
        else
        {
            zombies[activeZombieIndex].transform.position = startPos;
            pickNewZombie();

        }

    }

    /*updateLifeUI control health system 
    it set to heart to false when called  also display gameover button when game is over
    and display the high score*/

    private void updateLifeUI()
    {
        heart[livesRemain].gameObject.SetActive(false);

        if (livesRemain == 0)
        {
            heart[livesRemain].gameObject.SetActive(false);
            gameOver = true;
            gameOverButton.gameObject.SetActive(true);
            highScoreText.text = "Your Score : " + zombieSmashed.ToString();
        }
    }

    /*this fun gives random num and set it to active zombie index
    and also update its pos to active zombie index*/
    private void pickNewZombie()
    {
        isRising = true;
        isFalling = false;
        activeZombieIndex = UnityEngine.Random.Range(0, zombies.Length);
        startPos = zombies[activeZombieIndex].transform.position;
    }

    /*after touchung enemy this fun will get called
    this will increase score also increase speed by calling IncreaseSpawnSpeed fun
    also update score and then will called pickNewZombie  */
    public void killEnemy()
    {
        // Debug.Log("enemy killed");
        zombieSmashed++;            //increasing score;
        IncreaseSpawnSpeed();
        scoreText.text = zombieSmashed.ToString();
        zombies[activeZombieIndex].transform.position = startPos;
        pickNewZombie();

    }

    /*this will increase speed after passing some scoreThreshold*/
    private void IncreaseSpawnSpeed()
    {
        if (zombieSmashed >= scoreThreshold)
        {
            riseSpeed += 1;
            scoreThreshold *= 2;
        }
    }

    //these two fun are for buttons

    public void OnRestart()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}
