
using UnityEngine;
using System;
using System.Collections.Generic;

public class LevelManager : Singleton<LevelManager> {

	[SerializeField]
	private GameObject[] tilePrefabs;

	public Dictionary<Point, TileScript> Tiles { get; set; }

	private Point portalSpawn;

	[SerializeField]
	private GameObject portalPrefab;

	public float TileSize
	{
		get { return tilePrefabs[0].GetComponent<SpriteRenderer> ().sprite.bounds.size.x; }
	}

	// Use this for initialization
	void Start () {
		CreateLevel ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void CreateLevel()
	{

		Tiles = new Dictionary<Point, TileScript>();

		string[] mapData = ReadLevelFromText();

		int mapX = mapData[0].ToCharArray ().Length;
		int mapY = mapData.Length;

		Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3 (0, Screen.height, 0));

		for (int y = 0; y < mapY; y++) 
		{
			char[] newTiles = mapData [y].ToCharArray ();
			for (int x = 0; x < mapX; x++)
			{
				PlaceTile(newTiles[x].ToString(), x, y, worldStart);
			}
		}

		SpawnPortal();
	}

	private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
	{
		int tileIndex = int.Parse(tileType);
		TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();

		newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));

		//Tiles.Add(new Point(x, y), newTile);

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

}
