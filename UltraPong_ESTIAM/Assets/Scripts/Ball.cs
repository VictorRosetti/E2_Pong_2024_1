using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    public float speed = 10;
    public TMP_Text points_player_left;
    public TMP_Text points_player_right;
    int playerA_points=0;
    int playerB_points=0;
    public Camera mainCam;
    void Start()
    {
        if((playerA_points>=3)||(playerB_points>=3))
        {
            SceneManager.LoadScene(2);
        }
        float x = Random.Range(0,2) == 0 ? -1:1;
        /*
        float x = Random.Range(0,2);
        if(x==0)
        {
            x=-1;
        }else
        {
            x=1;
        }
        */
        float y = Random.Range(0,2) == 0 ? -1:1;
        GetComponent<Rigidbody>().velocity = new Vector2(speed*x, speed*y);
        points_player_left.SetText(playerA_points.ToString());
        points_player_right.SetText(playerB_points.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision hit)
    {
        GetComponent<AudioSource>().Play();
        if(hit.gameObject.name == "Left")
        {
            transform.position = new Vector3(0,0,9);
            playerB_points++;
            Start();
        }else if (hit.gameObject.name == "Right")
        {
            transform.position = new Vector3(0,0,9);
            playerA_points++;
            Start();
        }else if(hit.gameObject.name=="PowerUp")
        {
            this.transform.localScale = new Vector3(1,1,1);
            Destroy(hit.gameObject);
            StartCoroutine(PowerEnd(3));
        }
    }

    void OnTriggerEnter(Collider touch)
    {
        if(touch.gameObject.name == "PowerUp2")
        {
            mainCam.fieldOfView = 150;
            Destroy(touch.gameObject);
        }
    }

    IEnumerator PowerEnd(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        this.transform.localScale = new Vector3(3,3,3);
    }
}
