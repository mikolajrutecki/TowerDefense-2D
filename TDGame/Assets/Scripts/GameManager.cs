using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

	public TowerButton ClickedButton{ get; set; }

	private int currency;

	[SerializeField]
	private Text currencyTxt;

    public ObjectPool Pool { get; set; }

    public bool WaveActive
    {
        get
        {
            return activeEnemies.Count > 0;
        }
    }

	private Tower selectedTower;

    private int lives;

    [SerializeField]
    private Text livesTxt;

    private bool gameOver = false;

    [SerializeField]
    private GameObject gameOverMenu;

    [SerializeField]
    private GameObject playButton;
    
    private int wave = 0;

    [SerializeField]
    private Text waveTxt;

    private List<EnemyScript> activeEnemies = new List<EnemyScript>();

	public int Currency
	{
		get 
		{
			return currency;
		}

		set
		{
			this.currency = value;
			this.currencyTxt.text = value.ToString () + "<color=lime>$</color>";
		}
	}

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            this.lives = value;

            if (lives <= 0)
            {
                this.lives = 0;
                GameOver();
            }

            livesTxt.text = lives.ToString();
        }
    }

    private void Awake()
    {
        Pool = GetComponent<ObjectPool>();
    }

    // Use this for initialization
    void Start ()
	{
        Lives = 100;
		Currency = 5;
	}
	
	// Update is called once per frame
	void Update () {
		HandleEscape();
	}

	public void PickTower(TowerButton towerButton)
	{
		if(Currency >= towerButton.Price)
		{
			this.ClickedButton = towerButton;
			Hover.Instance.Activate(towerButton.Sprite);
		}

	}

	public void BuyTower()
	{
		if(Currency >= ClickedButton.Price)
		{
			Currency -= ClickedButton.Price;
			Hover.Instance.Deactivate();
		}
	}

	public void SelectTower(Tower tower)
	{
		if (selectedTower != null)
		{
			selectedTower.Select();
		}

		selectedTower = tower;
		selectedTower.Select();
	}

	public void DeselectTower()
	{
		if (selectedTower != null)
		{
			selectedTower.Select();
		}

		selectedTower = null;
	}

	private void HandleEscape()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Hover.Instance.Deactivate();
		}
	}

    public void GameOver()
    {
        if(!gameOver)
        {
            gameOver = true;
            gameOverMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }
    
    public void StartWave()
    {
        wave++;
        waveTxt.text = string.Format("Wave: <color=lime>{0}</color>", wave);
        StartCoroutine(SpawnWave());
        playButton.SetActive(false);
    }

    private IEnumerator SpawnWave()
    {
        for(int i = 0; i < wave; i++)
        {
            string type = "enemy";
            EnemyScript enemy = Pool.GetObject(type).GetComponent<EnemyScript>();

            activeEnemies.Add(enemy);

            yield return new WaitForSeconds(2.5f);
        }

    }

    public void RemoveEnemy(EnemyScript enemy)
    {
        activeEnemies.Remove(enemy);

        if (!WaveActive)
        {
            playButton.SetActive(true);
        }
    }


    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
		
}
