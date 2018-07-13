using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleController : MonoBehaviour
{
//    private BoxCollider2D _collider2D;
//    private SpriteRenderer _sprite;
    private BoxCollider2D _bonusCollider2D;

    private bool _isDestroyed;

    public Text CoinCountText;

    // Use this for initialization
    void Awake ()
	{
//	    _collider2D = GetComponent<BoxCollider2D>();
//	    _sprite = GetComponent<SpriteRenderer>();
	    _bonusCollider2D = this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>();
	    CoinCountText = GameObject.Find("Coin Count").GetComponent<Text>();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (_isDestroyed == false && other.CompareTag("Player"))
        {
            Destroy(other.gameObject, 0.01f);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //если играем с телефона, то есть Touch, и мы смотрим его координаты
        //на компьютере этого Touch не будет
        if (Input.touchCount > 0)
        {
            if (Input.GetAxisRaw("Fire1") > 0 && other.CompareTag("Player") && _isDestroyed == false)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x >= Screen.width / 2)
                {
                    TriggerDestruction(other);
                }
            }
        }
        //если играем с компьютера, то смотрим, какая кнопка нажата
        else if (Input.GetAxisRaw("Fire2") > 0 && other.CompareTag("Player") && _isDestroyed == false)
        {
            TriggerDestruction(other);
        }
    }

    private void TriggerDestruction(Collider2D other)
    {
        if (other.IsTouching(_bonusCollider2D))
        {
            int coins = int.Parse(CoinCountText.text);
            coins += 100;
            CoinCountText.text = coins.ToString();
        }
        _isDestroyed = true;
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update () {
    }

    
}
