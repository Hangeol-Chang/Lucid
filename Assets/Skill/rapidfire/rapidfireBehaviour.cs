using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rapidfireBehaviour : MonoBehaviour
{
    public GameObject player;
    GameObject enemy;

    private Vector3 hitPos;
    private Vector3 hitPos2;

    private float direction;
    private float speed = .02f; //테스트용
    //private float speed = .2f; //빌드용
    private bool canmove = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Monster"))
        {
            GetComponent<Animator>().SetBool("die", true);
            canmove = false;
            StartCoroutine(hit());
        }
    }

    IEnumerator hit()
    {
        yield return new WaitForSeconds(0.15f);
        gameObject.SetActive(false);
    }
    void Start()
    {
    }
    private void OnEnable()
    {
        Invoke("deactive", 1);
        direction = player.GetComponent<PlayerBehaviour>().lookdirection;
        this.transform.localScale = new Vector3(direction, 1, 1);
        canmove = true;
    }
    void deactive()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(canmove == true)
            this.transform.Translate(direction * speed, 0,0);
    }
}
