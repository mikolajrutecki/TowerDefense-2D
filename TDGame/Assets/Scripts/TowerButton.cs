using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour {

	[SerializeField]
	private GameObject towerPrefab;

	[SerializeField]
	private Sprite sprite;

	[SerializeField]
	private int price;

	[SerializeField]
	private Text priceTxt;

	public int Price
	{
		get
		{
			return price;
		}

		set
		{
			this.price = value;
		}
	}

	public GameObject TowerPrefab
	{
		get
		{
			return towerPrefab;
		}
	}

	public Sprite Sprite
	{
		get
		{
			return sprite;
		}
	}

	private void Start()
	{
		//priceTxt.text = Price + "$";
	}

}
