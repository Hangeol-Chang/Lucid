using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rapidbutterflyBehaviour : MonoBehaviour
{
    PlayerBehaviour playerbehaviour;
    public GameObject player;

    private float ypower;
    private float xpower = .8f; //테스트용
    private float power = 1f; //테스트용
    //private float xpower = 8f; //빌드용
    //private float power = 10f; //빌드용
    private float direction;
    private Rigidbody2D rigid;
    private bool canmove = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Monster"))
        {
            GetComponent<Animator>().SetBool("die", true);
            this.rigid.velocity = new Vector3(0, 0, 0);
            this.transform.localScale *= 0.4f;
            canmove = false;
            StartCoroutine(die());
        }
    }
    IEnumerator die()
    {
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
    }
    void Start()
    {
        this.rigid = GetComponent<Rigidbody2D>();
        playerbehaviour = player.GetComponent<PlayerBehaviour>();
        direction = playerbehaviour.lookdirection;
        this.transform.localScale = new Vector3(direction * 1.5f, 1.5f, 1);
        canmove = true;
    }
    private void OnEnable()
    {
        Invoke("deactive", 1.5f);
        direction = playerbehaviour.lookdirection;
        this.transform.localScale = new Vector3(direction * 1.5f, 1.5f, 1);
        canmove = true;
    }
    void deactive()
    {
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (canmove == true)
        {
            ypower = Random.Range(-1, 2);
            //transform.Translate(direction * xpower,0, 0);
            this.rigid.AddForce(new Vector2(direction * xpower, power * ypower));
        }
    }
}
