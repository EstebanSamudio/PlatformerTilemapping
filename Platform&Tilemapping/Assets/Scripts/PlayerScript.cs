using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text life;
    private int scoreValue = 0;
    private int lifeValue = 3;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public AudioClip music;
    public AudioClip SFX;
    public AudioSource musicSource;
    public AudioSource SFXsource;


    // Start is called before the first frame update
    void Start()
    {
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        life.text = lifeValue.ToString();
        musicSource.clip = music;
        musicSource.Play();
        musicSource.loop = true;
        SFXsource.clip = SFX;
        SFXsource.Stop();
        SFXsource.loop = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if(collision.collider.tag == "Enemy")
        {
            lifeValue -= 1;
            life.text = lifeValue.ToString();
        }

        if(collision.collider.tag == "Spike")
        {
            lifeValue = 0;
            life.text = lifeValue.ToString();
        }

        if (scoreValue == 4)
        {
            transform.position = new Vector3(63f, 3f, 0f);
            scoreValue = 5;
        }

        if (scoreValue >= 9)
        {
            winTextObject.SetActive(true);
            musicSource.clip = music;
            musicSource.Stop();
            SFXsource.clip = SFX;
            SFXsource.Play();
            SFXsource.loop = false;
            Destroy(this);
        }

        if (lifeValue <= 0)
        {
            loseTextObject.SetActive(true);
            Destroy(this);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKey(KeyCode.W))
        {
            rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
    }
}
