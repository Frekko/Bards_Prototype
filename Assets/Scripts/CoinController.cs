using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinController : MonoBehaviour
{
    public Text CoinCountText;

//    void OnTriggerEnter(Collider other)
//    {
//        int coinCount = int.Parse(coinCounter.text);
//        coinCount++;
//        coinCounter.text = coinCount.ToString();
//        Destroy(this.gameObject);
//    }

	// Use this for initialization
	void Start ()
	{
	    CoinCountText = GameObject.Find("Coin Count").GetComponent<Text>();
        Debug.Log(CoinCountText);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            int coins = int.Parse(CoinCountText.text);
            coins += 1000;
            CoinCountText.text = coins.ToString();
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
