using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalatk_butterflyBehaviour : MonoBehaviour
{
    private bool canmove = false;

    Rigidbody2D rigid;
    public GameObject Enemy;
    Animator ani;
    private float startpower = 60;
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
            ani.SetBool("die", true);
            Invoke("da", 0.5f);
        }
    }
    void da()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        ani = GetComponent<Animator>();
        this.rigid = GetComponent<Rigidbody2D>();

        StartCoroutine(start());
    }
    private void OnEnable()
    {
        canmove = false;
        StartCoroutine(start());
    }

    IEnumerator start()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        xdir = Random.Range(-2.0f, 2f);
        ydir = Random.Range(2.0f, 3.0f);
        ydir2 = Random.Range(0, 2);
        if (ydir2 == 0) ydir = -ydir;
        rigid.AddForce(new Vector2(xdir, ydir) * startpower);

        yield return new WaitForSeconds(0.3f);
        rigid.AddForce(new Vector2(-xdir, -ydir) * startpower * 0.4f);
        yield return new WaitForSeconds(0.1f);
        rigid.AddForce(new Vector2(-xdir, -ydir) * startpower * 0.2f);
        //yield return new WaitForSeconds(0.1f);
        //rigid.AddForce(new Vector2(-xdir, -ydir) * startpower * 0.2f);
        yield return new WaitForSeconds(0.2f);
        canmove = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!Enemy.gameObject.activeSelf)
        {
            ani.SetBool("die", true);
            rigid.velocity = new Vector3(0, 0, 0);
            Invoke("da", 0.5f);
        }
        if (canmove == true)
        {
            Vector2 Pos = new Vector2(Enemy.transform.position.x - this.transform.position.x, (Enemy.transform.position.y - this.transform.position.y)*2f).normalized /2;
            if (Enemy == null) Destroy(gameObject);
            rigid.AddForce(Pos * chasepower);
            this.transform.localScale = new Vector3(-Pos.x / Mathf.Abs(Pos.x), 1, 1) * 0.5f;
        }
        if (Vector3.Distance(this.transform.position, Enemy.transform.position) < 1f)
        {
            ani.SetBool("die", true);
            rigid.velocity = new Vector3(0, 0, 0);
            Invoke("da", 0.5f);
        }
    }
}
