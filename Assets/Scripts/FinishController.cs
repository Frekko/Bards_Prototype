using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishController : MonoBehaviour
{

    public Text TextMessage;
    public Image ImageBox;
    public Sprite FinishFlag;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            TextMessage.text = "YOU WIN";
            ImageBox.sprite = FinishFlag;
            Destroy(other.gameObject);
        }
    }
}
