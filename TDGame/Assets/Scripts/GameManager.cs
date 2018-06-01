using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

	public TowerButton ClickedButton{ get; set; }

	private int currency;

	[SerializeField]
	private Text currencyTxt;

	private Tower selectedTower;

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


	// Use this for initialization
	void Start ()
	{
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
		
}
