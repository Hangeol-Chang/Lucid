using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalatk_butterflybehaviour2 : MonoBehaviour
{ 
    private float damage = 10000;
    private int hitnum = 3;
    private bool canmove = false;

    Rigidbody2D rigid;
    GameObject Enemy;
    Animator ani;
    private float startpower = 200;
    private float chasepower = 1.4f;
    private float xdir;
    private float ydir;
    private int ydir2;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Monster"))
        {
            canmove = false;
            rigid.velocity = new Vector3(0, 0, 0);
            StartCoroutine(die());
        }
    }
    IEnumerator die()
    {
        ani.SetBool("die", true);
        this.rigid.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void Start()
    {
        Enemy = GameObject.FindWithTag("Monster");
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        ani = GetComponent<Animator>();
        this.rigid = GetComponent<Rigidbody2D>();
        xdir = Random.Range(-1.0f, 1.0f);
        ydir = Random.Range(2.0f, 3.0f);
        ydir2 = Random.Range(0, 2);
        if (ydir2 == 0) ydir = -ydir;
        rigid.AddForce(new Vector2(xdir, ydir) * startpower);
        StartCoroutine(decelleration());
    }

    IEnumerator decelleration()
    {
        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(new Vector2(-xdir, -ydir) * startpower * 0.4f);
        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(new Vector2(-xdir, -ydir) * startpower * 0.3f);
        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(new Vector2(-xdir, -ydir) * startpower * 0.2f);
        yield return new WaitForSeconds(0.2f);
        canmove = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (Enemy.gameObject.activeSelf == false) die();
        if (canmove == true)
        {
            Vector2 Pos = new Vector2(Enemy.transform.position.x - this.transform.position.x, (Enemy.transform.position.y - this.transform.position.y) * 2f).normalized /2;
            if (Enemy == null) Destroy(gameObject);
            rigid.AddForce(Pos * chasepower);
            this.transform.localScale = new Vector3(-Pos.x / Mathf.Abs(Pos.x), 1, 1) * 0.5f;
        }
    }
}
