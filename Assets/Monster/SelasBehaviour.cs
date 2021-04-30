using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelasBehaviour : MonoBehaviour
{
    private float hp = 10000000;
    private float movedirection = 1;
    
    //private float speed = 0.005f; //빌드용
    private float speed = 0.0005f; //테스트용
    private float Weight = 10;
    Animator ani;
    public PlayerBehaviour player;
    public GameManager gm;

    private bool canmove = true;
    private bool canhit = true;

    void Start()
    {
        this.ani = GetComponent<Animator>();
        InvokeRepeating("direction", 3f, 5f);
    }
    private void OnEnable()
    {
        hp = 10000000;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Skill"))
        {
            StartCoroutine(gm.damagein(this.gameObject, collision.gameObject.name));
            gm.Increase_gagein();
            if (!collision.gameObject.tag.Contains("final")) StartCoroutine(hit());
            if (canhit == true)
            {
                float Pos = collision.transform.position.x;
                StartCoroutine(hitmotion(Pos));
            }
        }
        if (collision.gameObject.tag.Contains("nightmare")) gm.Increase_gageout();


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("map")) movedirection = -movedirection;
    }

    public IEnumerator decreasehp(int damage, int hitnum)
    {
        this.hp -= damage * hitnum;
        if(hp <= 0)
        {
            ani.SetBool("die", true);

            //this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            yield return new WaitForSeconds(1.5f);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if(movedirection !=0 ) this.transform.localScale = new Vector3(-movedirection, 1, 1);
       if(canmove == true )  transform.Translate(movedirection * speed, 0, 0);

    }
    void direction()
    {
        movedirection = Random.Range(-1, 2);
    }
    IEnumerator hit()
    {
        player.finalatk(gameObject);
        yield return null;
    }
    IEnumerator hitmotion(float P)
    {
        yield return null;
        canmove = false;
        canhit = false;
        ani.SetBool("hit", true);
        movedirection = (P - transform.position.x) / Mathf.Abs(P - transform.position.x);
        
        transform.Translate(-movedirection / Weight, 0, 0);

        yield return new WaitForSeconds(0.2f);
        ani.SetBool("hit", false);
        transform.Translate(movedirection / (2* Weight), 0, 0);
        canmove = true;
        canhit = true;
    }

}
