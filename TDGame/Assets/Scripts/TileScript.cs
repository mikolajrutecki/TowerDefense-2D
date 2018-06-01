using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : Singleton<TileScript>
{

	public Point GridPosition { get; private set; }

	public bool IsEmpty { get; set; }

	private Tower myTower;

	private Color32 fullColor = new Color32(255, 96, 96, 255);

	private Color32 emptyColor = new Color32(96, 255, 90, 255);

	private SpriteRenderer spriteRenderer;

	public Vector2 WorldPosition
	{
		get 
		{
			return new Vector2(transform.position.x +(GetComponent<SpriteRenderer>().bounds.size.x/2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2));
		}
	}


	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
		string tileName = GetComponent<SpriteRenderer>().sprite.texture.name;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
	{
		IsEmpty = true;
		this.GridPosition = gridPos;
		transform.position = worldPos;
		transform.SetParent(parent);

		LevelManager.Instance.Tiles.Add(gridPos, this);
	}

	private void OnMouseOver()
	{
		string tileName = GetComponent<SpriteRenderer>().sprite.texture.name;
		if (!EventSystem.current.IsPointerOverGameObject () && (tileName == "path"))
		{
			IsEmpty = false;
		}
		if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null) {
			if (IsEmpty) {
				ColorTile (emptyColor);
			} 
			if (!IsEmpty) {
				ColorTile (fullColor);
			} else if (Input.GetMouseButtonDown (0)) {
				PlaceTower ();
			}
		} 
		else if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton == null && Input.GetMouseButtonDown(0))
		{
			if (myTower != null)
			{
				GameManager.Instance.SelectTower(myTower);
			}
			else
			{
				GameManager.Instance.DeselectTower();
			}
		}
	}

	private void OnMouseExit()
	{
		ColorTile(Color.white);
	}

	private void PlaceTower()
	{
		GameObject tower = (GameObject)Instantiate(GameManager.Instance.ClickedButton.TowerPrefab, transform.position, Quaternion.identity);
		tower.GetComponent<SpriteRenderer>().sortingOrder = GridPosition.Y;

		tower.transform.SetParent(transform);

		this.myTower = tower.transform.GetChild(0).GetComponent<Tower>();

		ColorTile(Color.white);
		IsEmpty = false;

		GameManager.Instance.BuyTower();
	}

	private void ColorTile(Color newColor)
	{
		spriteRenderer.color = newColor;
	}
}
