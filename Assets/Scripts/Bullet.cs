using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    public float speed = 30;

    public Rigidbody2D rigidBody;

    public Sprite ExplodedAlienImage;

	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.up * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Wall")
        {
            Destroy(gameObject);
        }

        if (col.tag == "Alien")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.ShipExplosion);

            IncreaseUITextScore();

            col.GetComponent<SpriteRenderer>().sprite = ExplodedAlienImage;

            Destroy(gameObject);

            Object.Destroy(col.gameObject, 0.5f);
        }

        if (col.tag == "Shield")
        {
            Destroy(gameObject);
            Object.Destroy(col.gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);  
    }

    void IncreaseUITextScore()
    {   
        var textUIComp = GameObject.Find("Score").GetComponent<Text>();

        int score = int.Parse(textUIComp.text);

        score += 10;

        textUIComp.text = score.ToString();
    }
}
