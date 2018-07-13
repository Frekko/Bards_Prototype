using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoadController : MonoBehaviour
{
    private Text CoinText;
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            int coinCount = int.Parse(CoinText.text);
            coinCount += 2;
            CoinText.text = coinCount.ToString();
//            Destroy(this.gameObject, 3);
        }
    }

	// Use this for initialization
	void Start ()
	{
	    CoinText = GameObject.Find("Coin Count").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
