using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusObstacle : MonoBehaviour
{
    private bool _isDestroyed = false;
    public Text CoinCountText;
    // Use this for initialization
    void Start () {
        CoinCountText = GameObject.Find("Coin Count").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetAxisRaw("Fire2") > 0 && other.CompareTag("Player") && _isDestroyed == false)
        {
            int coins = int.Parse(CoinCountText.text);
            coins += 100;
            CoinCountText.text = coins.ToString();
            _isDestroyed = true;
            Destroy(this.gameObject);
        }
    }
}
