using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;	

public class ShopManager : MonoBehaviour {

	// Use this for initialization
	GameObject[] objlist = new GameObject[12];
	List<Goods> goodslist;
	UILabel pageobj,label2,label3,label4;
	int choose;
	int page;
	int count;
	int price;
	int money;

	void Start () {
		List<string> btnName = new List<string> ();
		btnName.Clear ();
		for (int i = 1; i <= 10; i++)
			btnName.Add ("Shop_Button_" + i);
		btnName.Add ("Shop_Left");
		btnName.Add ("Shop_Right");
		btnName.Add ("Shop_Up");
		btnName.Add ("Shop_Down");
		btnName.Add ("Shop_Buy");
		btnName.Add ("Shop_Exit");
		foreach(string btnname in btnName) {
			GameObject btnobj = GameObject.Find(btnname);
			btnobj.AddComponent<UIEventListener>();
			UIEventListener.Get (btnobj).onClick = onclick;
		}
		for (int i = 1; i <= 10; i++)
			objlist [i] = GameObject.Find ("Shop_Button_" + i);
		pageobj = GameObject.Find ("Shop_Label_1").GetComponent<UILabel> ();
		label2 = GameObject.Find ("Shop_Label_2").GetComponent<UILabel> ();
		label3 = GameObject.Find ("Shop_Label_3").GetComponent<UILabel> ();
		label4 = GameObject.Find ("Shop_Label_4").GetComponent<UILabel> ();
		Shop shop = new Shop ();
		shop.addGoods ("potion1", 1);
		shop.addGoods ("potion2", 2);
		shop.addGoods ("potion3", 3);
		shop.addGoods ("potion1", 4);
		shop.addGoods ("potion2", 5);
		shop.addGoods ("potion3", 6);
		shop.addGoods ("potion1", 7);
		shop.addGoods ("potion2", 8);
		shop.addGoods ("potion3", 9);
		shop.addGoods ("potion1", 10);
		shop.addGoods ("potion2", 11);
		shop.addGoods ("potion3", 12);
		shop.addGoods ("potion1", 13);
		shop.addGoods ("potion2", 14);
		shop.addGoods ("potion3", 15);
		shop.addGoods ("potion1", 16);
		shop.addGoods ("potion2", 17);
		shop.addGoods ("potion3", 18);
		shop.addGoods ("po123n1", 19);
		Init (shop);
	}


	void onclick(GameObject btn) {
		string name = btn.name;
		//Debug.Log (name);
		if (name.Contains ("Shop_Button_")) {
			if (name.Contains ("10"))
				return;
			if ((page - 1) * 9 + name [12] - '0' - 1 >= goodslist.Count)
				return;
			if (choose != name [12] - '0')
				count = 0;
			choose = name [12] - '0';
			return;
		} else if (name == "Shop_Left") {
			if (page > 1) {
				page--;
				count = 0;
				choose = 1;
			}
		} else if (name == "Shop_Right") {
			if (page * 9 < goodslist.Count) {
				page++;
				count = 0;
				choose = 1;
			}
		} else if (name == "Shop_Up") {
			count++;
		} else if (name == "Shop_Down") {
			if (count > 0)
				count--;
		} else if (name == "Shop_Buy") {
		} else if (name == "Shop_Exit") {
		}
	}

	// Update is called once per frame
	void Update () {
		pageobj.text = "" + page;
		for (int i = (page - 1) * 9, j = 1; i < page * 9; i++, j++) {
			//Debug.Log (i + " " +  j);
			if (i < goodslist.Count) {
				objlist[j].transform.GetChild(0).GetComponent<UISprite>().spriteName = goodslist[i].spname;
				objlist[j].transform.GetChild(1).GetComponent<UILabel>().text = "";
				if (goodslist[i].price != 0)
					objlist[j].transform.GetChild(1).GetComponent<UILabel>().text += goodslist[i].price;
			} else {
				objlist[j].transform.GetChild(0).GetComponent<UISprite>().spriteName = "icon-null";
				objlist[j].transform.GetChild(1).GetComponent<UILabel>().text = "";
			}
		}
		objlist [10].transform.GetChild (0).GetComponent<UISprite> ().spriteName =
			objlist [choose].transform.GetChild (0).GetComponent<UISprite> ().spriteName;
		objlist [10].transform.GetChild (1).GetComponent<UILabel> ().text =
			objlist [choose].transform.GetChild (1).GetComponent<UILabel> ().text;
		label2.text = "" + count;
		price = goodslist [(page - 1) * 9 + choose - 1].price;
		label3.text = "" + count * price;
		label4.text = "" + money;
	}

	protected static ShopManager instance_ = null;
	public static ShopManager instance {
		get {
			if (instance_ == null) {
				instance_ = new ShopManager();
			}
			return instance_;
		}
	}

	void Init(Shop shop) {
		if (shop == null)
			return;
		goodslist = shop.goodslist;
		page = 1;
		choose = 1;
		count = 0;
		price = 0;
		money = 100;
		/*
		//UIAtlas at = (UIAtlas)Resources.Load ("icon");
		for (int i = 0; i < 9; i++)
			if (i >= goodslist.Count)
				break;
			else {
				//objlist[i + 1].transform.GetChild(0).GetComponent<UISprite>().atlas = at;
				objlist[i + 1].transform.GetChild(0).GetComponent<UISprite>().spriteName = goodslist[i].spname;
				objlist[i + 1].transform.GetChild(1).GetComponent<UILabel>().text = "" + goodslist[i].price;
		}*/
	}

}


public class Goods{
	public Goods(string name_, int price_) {
		name = name_;
		price = price_;
		if (name == "potion1")
			spname = ("icon-potion1");
		else if (name == "potion2")
			spname = ("icon-potion2");
		else if (name == "potion3")
			spname = ("icon-potion3");
		else 
			spname = ("coin-icon");
	}
	public string name;
	public int price;
	public string spname;
}

public class Shop {
	public Shop() {
		goodslist = new List<Goods> ();
		goodslist.Clear ();
	}


	public void addGoods(Goods g) {
		goodslist.Add (g);
	}
	public void addGoods(string name_, int price_) {
		goodslist.Add (new Goods (name_, price_));
	}
	public void delGoods(Goods g) {
		if (goodslist.Contains (g))
			goodslist.Remove (g);
	}

	public string getName(int i) {
		return goodslist [i].name;
	}

	public int getPrice(int i) {
		return goodslist [i].price;
	}


	public List<Goods> goodslist;


}