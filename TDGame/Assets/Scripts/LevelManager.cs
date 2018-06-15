
using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager> {

	[SerializeField]
	private GameObject[] tilePrefabs;

	private GameObject go;

	public Dictionary<Point, TileScript> Tiles { get; set; }

	private Point portalSpawn;
	private Point enemySpawn;
    private Point baseSpawn;


	[SerializeField]
	private Transform map;

	[SerializeField]
	private GameObject portalPrefab;

    [SerializeField]
    private GameObject basePrefab;

	[SerializeField]
	private GameObject enemyPrefab;

	[SerializeField]
	private float spawnDelay;

	[SerializeField]
	private GameObject goalPrefab;

	[SerializeField]
	private CameraMovement cameraMovement;

	public List<Point> waypoints = new List<Point>();

	public static Transform[] goals;

	public static Transform[] Goals
	{
		get
		{
			return goals;
		}
	}

	public float TileSize
	{
		get { return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x; }
	}
		
	void Start()
	{
		CreateLevel ();
        CreateGoals();
        //InvokeRepeating("SpawnEnemy", spawnDelay, spawnDelay);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLevel()
	{

		Tiles = new Dictionary<Point, TileScript>();

		string[] mapData = ReadLevelFromText();

		int mapX = mapData[0].ToCharArray().Length;
		int mapY = mapData.Length;


		Vector3 maxTile = Vector3.zero;

		Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3 (0, Screen.height, 0));

		for (int y = 0; y < mapY; y++) 
		{
			char[] newTiles = mapData[y].ToCharArray ();
			for (int x = 0; x < mapX; x++)
			{
				maxTile = PlaceTile(newTiles[x].ToString(), x, y, worldStart);
			}
		}

		cameraMovement.SetLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

		SpawnPortal();
        SpawnBase();
	}

	private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStart)
	{
		int tileIndex = int.Parse(tileType);

		TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

		newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);

		return newTile.transform.position;

	}

	private string[] ReadLevelFromText()
	{
		TextAsset bindData = Resources.Load("Level") as TextAsset;

		string data = bindData.text.Replace(Environment.NewLine, string.Empty);

		return data.Split('-');
	}

	private void SpawnPortal()
	{
		portalSpawn = new Point(0, 0);

		Instantiate(portalPrefab, Tiles[portalSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
	}

    private void SpawnBase()
    {
        baseSpawn = new Point(13, 6);

        Instantiate(basePrefab, Tiles[baseSpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }


    public void CreateGoals()
    {
        waypoints.Add(new Point(0, 0));
        waypoints.Add(new Point(10, 0));
        waypoints.Add(new Point(10, 2));
        waypoints.Add(new Point(1, 2));
        waypoints.Add(new Point(1, 4));
        waypoints.Add(new Point(7, 4));
        waypoints.Add(new Point(7, 6));
        waypoints.Add(new Point(13, 6));

        goals = new Transform[waypoints.Count];

        for (int i = 0; i < waypoints.Count; i++)
        {
            go = new GameObject("goal");
            go = Instantiate(goalPrefab, Tiles[waypoints[i]].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
            goals[i] = go.transform;
            //Debug.Log(goals[i].position);
        }
    }

    //public void SpawnEnemy()
    //{
    //    enemySpawn = new Point(0, 0);

    //    Instantiate(enemyPrefab, Tiles[enemySpawn].GetComponent<TileScript>().WorldPosition, Quaternion.identity);

    //    return;
    //}

}
