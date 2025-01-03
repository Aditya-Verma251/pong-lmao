using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ballscript : MonoBehaviour
{
    public float initialBallImpulse;
    private Vector3 imp;
    bool hasBounced = false, hasScored = false, reset = false;
    Rigidbody2D rb;
    public playercontrol p;
    public enemycontrol e;
    public AudioSource hitsource, scoresource;
    private int pScore, eScore;
    private float balltimer = 0;
    /*public*/ private TextMeshProUGUI pScoreText, eScoreText;
    public GameObject pScoreTMP, eScoreTMP;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        balltimer = 0;
        rb = GetComponent<Rigidbody2D>();
        pScoreText = pScoreTMP.GetComponentInChildren<TextMeshProUGUI>();
        eScoreText = eScoreTMP.GetComponentInChildren <TextMeshProUGUI>();
        eScore = pScore = 0;
        if (Random.Range(0, 2) == 0)
        {
            imp = new Vector3(initialBallImpulse, 0, 0);
        }
        else
        {
            imp = new Vector3(-initialBallImpulse, 0, 0);
        }
        rb.AddForce(imp, ForceMode2D.Impulse);
        //eScore = 19;
        //pScore = 19;
        //pScoreText.text = "1";
        //eScoreText.text = "2";
    }

    // Update is called once per frame
    void Update()
    {
        /*if (transform.position.y < -20 || transform.position.y > 20)
        {
            rb.AddForce(-imp, ForceMode2D.Impulse);
            imp.y = -imp.y;
            rb.AddForce(imp, ForceMode2D.Impulse);
        }

        if (transform.position.x < -40 || transform.position.x > 40)
        {
            rb.AddForce(-imp, ForceMode2D.Impulse);
            imp.x = -imp.x;
            rb.AddForce(imp, ForceMode2D.Impulse);
        }*/
    }

    private void FixedUpdate()
    {
        if (!hasBounced)
        {
            if (rb.IsTouching(p.GetComponent<BoxCollider2D>()))
            {
                //   Debug.Log("p_bounce");
                imp = (transform.position - p.transform.position).normalized * imp.magnitude;
                rb.AddForce(imp, ForceMode2D.Impulse);
                hitsource.GetComponent<AudioSource>().Play();
                //initialBallImpulse = -initialBallImpulse;
                //Vector3 tvel = (transform.position - p.transform.position).normalized * rb.linearVelocity.magnitude;
                //rb.linearVelocity = new Vector2 (tvel.x, tvel.y);
                hasBounced = true; // Prevent repeated bouncing
                StartCoroutine(ResetBounce());
            }

            if (rb.IsTouching(e.GetComponent<BoxCollider2D>())){
                //   Debug.Log("e_bounce");
                imp = (transform.position - e.transform.position).normalized * imp.magnitude;
                rb.AddForce(imp, ForceMode2D.Impulse);
                hitsource.GetComponent<AudioSource>().Play();
                //initialBallImpulse = -initialBallImpulse;
                //Vector3 tvel = (transform.position - e.transform.position).normalized * rb.linearVelocity.magnitude;
                //rb.linearVelocity = new Vector2(tvel.x, tvel.y);
                hasBounced = true; // Prevent repeated bouncing
                StartCoroutine(ResetBounce());
            }
        }

        /*if (transform.position.y < -20 || transform.position.y > 20)
        {
            rb.AddForce(-imp, ForceMode2D.Impulse);
            imp.y = - imp.y;
            rb.AddForce(imp, ForceMode2D.Impulse);
        }

        if (transform.position.x < -40 || transform.position.x > 40)
        {
            rb.AddForce(-imp, ForceMode2D.Impulse);
            imp.x = -imp.x;
            rb.AddForce(imp, ForceMode2D.Impulse);
        }*/

        if (transform.position.y < -19 || transform.position.y > 19)
        {
            imp.y = -imp.y;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, imp.y); // Flip velocity directly
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -19, 19), transform.position.z); // Clamp to bounds
        }

        if (transform.position.x < -26)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = new Vector3(1000, 1000, 0);
            eScore += 1;
            eScoreText.text = eScore.ToString();
            transform.position = new Vector3(0, 0, transform.position.z);
            scoresource.GetComponent<AudioSource>().Play();
            balltimer = 1.5f;
            hasScored = true;
            if (eScore >= 21)
            {
                SceneManager.LoadScene("WinScreenP1");
            }
            //Destroy(gameObject);
        }

        if (transform.position.x > 26)
        {
            rb.linearVelocity = Vector2.zero;
            transform.position = new Vector3(1000, 1000, 0);
            pScore += 1;
            pScoreText.text = pScore.ToString();
            transform.position = new Vector3(0, 0, transform.position.z);
            scoresource.GetComponent<AudioSource>().Play();
            balltimer = 1.5f;
            hasScored = true;
            if (pScore >= 21)
            {
                SceneManager.LoadScene("WinScreenP2");
            }
            //Destroy(gameObject);
        }

        //rb.angularVelocity = 0;
        //rb.rotation = 0;

        if (balltimer > 0) 
        {
            balltimer -= Time.deltaTime;
            rb.linearVelocity = Vector2.zero;
        }
        else if (hasScored)
        {
            hasScored = false;
            if (Random.Range(0, 2) == 0)
            {
                imp = new Vector3(initialBallImpulse, 0, 0);
            }
            else
            {
                imp = new Vector3(-initialBallImpulse, 0, 0);
            }
            rb.AddForce(imp, ForceMode2D.Impulse);
        }

        if (reset)
        {
            reset = false;
            hasBounced = false;
            pScore = eScore = 0;
            pScoreText.text = pScore.ToString();
            eScoreText.text = eScore.ToString();
            transform.position = Vector3.zero;
            if (Random.Range(0, 2) == 0)
            {
                imp = new Vector3(initialBallImpulse, 0, 0);
            }
            else
            {
                imp = new Vector3(-initialBallImpulse, 0, 0);
            }
            rb.AddForce(imp, ForceMode2D.Impulse);
        }
    }

    private IEnumerator ResetBounce()
    {
        yield return new WaitForSeconds(0.03f); // Adjust duration as needed
        hasBounced = false;
    }

    public void Reset()
    {
        reset = true;
        transform.position = Vector3.zero;
        rb.linearVelocity = Vector2.zero;
    }
}
