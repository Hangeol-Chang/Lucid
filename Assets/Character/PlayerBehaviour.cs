using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBehaviour : MonoBehaviour
{
    private BoxCollider2D HitBox;
    private Rigidbody2D rigid2D;
    private Animator animator;

    public GameObject DoublejumpEffect;
    public GameObject teleportEffect;
    public GameObject rapidfirebodyEffect;
    public GameObject[] flowerbombEffect = new GameObject[3];
    public GameObject summoner;

    public GameObject[] finalatkPrefab = new GameObject[2];
    private List<GameObject> finalPool = new List<GameObject>();
    private readonly int Maxcount = 26;
    private int Index = 0;

    private int canmove = 1;
    private bool canjump = true;
    private bool candoublejump = false;
    private bool candown = true;
    private bool canLadder = true;
    private bool canteleport = true;
    private bool canrapidfire = true;
    private bool canflowerbomb = true;

    private float movedirection;
    private float movedirectiony;
    public float lookdirection = 1;

    private float speedAbs;
    //private float walkforce = 20f; //빌드용
    private float walkforce = 3f; //유니티 테스트용
    private float maxWalkSpeed = 2f;
    private float jumpforce = 280f;
    private float doublejumpforce = 300f;
    private float doublejumpgap = .8f;
    private float LadderSpeed = .02f;

    //float clicktime = 0;
    //int oneClick = 0;
    //float doubleclicklimit = 0.5f;

    //쿨타임
    private float teleportcooldown = 0.7f;
    private Vector3 DoubleJunmpPos = new Vector3(0, 0, 0);

    private void OnCollisionStay2D(Collision2D factor) 
    {
        if (factor.gameObject.name.Contains("map") && this.rigid2D.velocity.y == 0) //땅에 닿고 있는 동안
        {
            if(animator.GetBool("Rapidfire") == false) canmove = 1;
            canjump = true;
            candoublejump = false;
            candown = true;

            animator.SetBool("Jump", false);
        }
        else canjump = false;
    }

    private void OnTriggerEnter2D(Collider2D factor)
    {
        if (factor.gameObject.name.Contains("map"))
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D factor)
    {
        if (factor.gameObject.name.Contains("Ladder"))
        {
            if (Input.GetKey(KeyCode.UpArrow) && canLadder == true)
            {
                if (animator.GetBool("Ladder") == false)
                {
                    animator.SetBool("Ladder", true);
                    animator.SetBool("Jump", false);
                    rigid2D.velocity = new Vector3(0, 0, 0);
                    this.transform.position = new Vector3(factor.transform.position.x, this.transform.position.y, 0);
                    rigid2D.gravityScale = 0;

                    canmove = 0;
                    canjump = true;
                    candoublejump = false;
                    candown = false;
                }
                transform.Translate(0, LadderSpeed, 0);

                this.animator.speed = 1f;
            }
            else if (animator.GetBool("Ladder") == true && Input.GetKey(KeyCode.DownArrow))
            {
                this.animator.speed = 1f;
                transform.Translate(0, - LadderSpeed, 0);
            }
            else if (animator.GetBool("Ladder") == true) this.animator.speed = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D factor)
    {
        if (factor.gameObject.name.Contains("map")) GetComponent<BoxCollider2D>().isTrigger = false;
        else if (factor.gameObject.name.Contains("Ladder"))
        {
            if (animator.GetBool("Ladder") == true)
            {
                animator.SetBool("Ladder", false);
                rigid2D.gravityScale = 1.8f;
                this.animator.speed = 1f;
            }
            canLadder = true;
        }
    }

    private void walk()
    {
        if(movedirection != 0) animator.SetBool("Walk", true);
        speedAbs = Mathf.Abs(this.rigid2D.velocity.x);

        if (speedAbs < this.maxWalkSpeed && movedirection !=0)
        {
            this.rigid2D.AddForce(transform.right * movedirection * this.walkforce);
        }
    }

    IEnumerator Down()
    { 
        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (animator.GetBool("Walk") == true) animator.SetBool("Walk", false);

            canmove = 0;
            canjump = false;
            canLadder = false;
            animator.SetBool("Down", true);
            if (Input.GetKeyDown(KeyCode.LeftAlt) && this.transform.position.y > -4)
            {
                this.rigid2D.AddForce(Vector3.up * 200f);
                animator.SetBool("Jump", true);
                candown = false;

                yield return new WaitForSeconds(0.1f);
                GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            canmove = 1;
            canjump = true;
            animator.SetBool("Down", false);
            canLadder = true;
        }
    }

    private void jump()
    {
        if(animator.GetBool("Ladder") == true)
        {
            animator.SetBool("Ladder", false);
            canLadder = false;
            canmove = 1;
            rigid2D.gravityScale = 1.8f;
            this.animator.speed = 1f;
        }
        canjump = false;
        animator.SetBool("Jump", true);
        this.rigid2D.AddForce(Vector3.up * jumpforce);
        candoublejump = true;
        candown = false;
    }

    private void doublejump()
    {
        candoublejump = false;
        rigid2D.velocity = new Vector3(0, rigid2D.velocity.y, 0);

        //Debug.Log(lookdirection);
        DoubleJunmpPos.x = (this.transform.position.x  - doublejumpgap * lookdirection);
        DoubleJunmpPos.y = (this.transform.position.y);
        DoublejumpEffect.transform.localScale = new Vector3(1f * lookdirection, 1f, 1f);

        Instantiate(DoublejumpEffect, DoubleJunmpPos, this.transform.rotation);

        this.rigid2D.AddForce(new Vector3(lookdirection * doublejumpforce,120,0));
    }

    IEnumerator teleport()
    {
        canteleport = false;
        teleportEffect.transform.localScale = new Vector3(.4f, .4f, 1f);
        Instantiate(teleportEffect,this.transform.position, this.transform.rotation );
        transform.position = new Vector3(this.transform.position.x + movedirection * 2.5f, this.transform.position.y + movedirectiony * 2.5f + .3f, this.transform.position.z);
        this.rigid2D.velocity = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(teleportcooldown);
        canteleport = true;
        yield return null;
    }

    void rapidWalk()
    {
            if (Input.GetKey(KeyCode.RightArrow)) movedirection = 1;
            else if (Input.GetKey(KeyCode.LeftArrow)) movedirection = -1;
            else movedirection = 0;

        /*if (Time.time - clicktime < doubleclicklimit)
        {
            if (oneClick == 1 && Input.GetKeyDown(KeyCode.RightArrow)) Debug.Log("ff"); lookdirection = 1; oneClick = 0;
            if (oneClick == 2 && Input.GetKeyDown(KeyCode.LeftArrow)) Debug.Log("dd"); lookdirection = -1; oneClick = 0;
        }

        Debug.Log(oneClick);
        if (oneClick == 0)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                oneClick = 1;
                clicktime = Time.time;
                Debug.Log(1);
            }else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                oneClick = 2;
                clicktime = Time.time;
                Debug.Log(2);
            }
        }*/
       

        speedAbs = Mathf.Abs(this.rigid2D.velocity.x);
        if (speedAbs < this.maxWalkSpeed /1.7f && movedirection != 0)
        {
            this.rigid2D.AddForce(transform.right * movedirection * this.walkforce);
        }
    }

    void rapidfire()
    {
        canmove = 2;
        canLadder = false;
        canjump = false;
        candown = false;

        rapidfirebodyEffect.SetActive(true);
        this.animator.SetBool("Rapidfire", true);
    }

    IEnumerator flowerbomb()
    {
        canmove = 0;
        canLadder = false;
        candown = false;
        canflowerbomb = false;
        animator.SetBool("atk1", true);
        //Instantiate(flowerbombEffect, this.transform.position, this.transform.rotation);
        for(int i = 0; i<3; i++)
        {
            if (!flowerbombEffect[i].activeSelf)
            {
                flowerbombEffect[i].SetActive(true);
                break;
            }
        }

        yield return new WaitForSeconds(0.5f);
        animator.SetBool("atk1", false);

        yield return new WaitForSeconds(0.3f);
        canflowerbomb = true;
        canmove = 1;
        canLadder = true;
        candown = true;
    }
    public void finalatk(GameObject monster)
    {
        int finalatkconst1 = Random.Range(0, 101);
        
        for(int i=0; i < Maxcount; i++)
        {
            if (!finalPool[i].activeSelf)
            {
                Index = i;
                break;
            }
        }

        if (finalatkconst1 >= 65)
        {
            for (int i = 0; i < 2; i++)
            {
                finalPool[Index].transform.position = this.transform.position;
                finalPool[Index].GetComponent<finalatk_butterflyBehaviour>().Enemy = monster;
                finalPool[Index].gameObject.SetActive(true);
                if (Index >= Maxcount - 1) Index = 0;
                else Index++;
            }
        }
        /*if (finalatkconst1 >= 95)
        {
            for (int i = 0; i < 2; i++)
            {
                finalatk2.GetComponent<finalatk_butterflyBehaviour>().Enemy = monster;
                Instantiate(finalatk2, this.transform.position, this.transform.rotation);
            }
        }
        else if (finalatkconst1 >= 85)
        {
            for (int i = 0; i < 2; i++)
            {
                finalatk1.GetComponent<finalatk_butterflyBehaviour>().Enemy = monster;
                Instantiate(finalatk1, this.transform.position, this.transform.rotation);
            }
        */
    }
    void summonerfire()
    {
        StartCoroutine( summoner.GetComponent<SummonerBehaviour>().fire());

        canmove = 0;
        rigid2D.gravityScale = 0f;
        canjump = true;
        candown = true;
        canrapidfire = false;
        canflowerbomb = false;
        GetComponent<BoxCollider2D>().isTrigger = true;
        this.rigid2D.velocity = new Vector3(0, 0, 0);
    }

    void Start()
    {
        for (int i = 0; i < Maxcount; i++)
        {
            GameObject a = Instantiate<GameObject>(finalatkPrefab[i/18]);
            a.gameObject.SetActive(false);
            finalPool.Add(a);
        }
        for (int i = 0; i < 2; i++) finalatkPrefab[i].SetActive(false);

        this.animator = GetComponent<Animator>();
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.HitBox = GetComponent<BoxCollider2D>();

        rapidfirebodyEffect.SetActive(false);
    }

    void Update()
    {
        if (canmove != 2)
        {
            if (Input.GetKey(KeyCode.RightArrow)) { movedirection = 1; lookdirection = 1; }
            else if (Input.GetKey(KeyCode.LeftArrow)) { movedirection = -1; lookdirection = -1; }
            else movedirection = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow)) { movedirectiony = 1; }
        else if (Input.GetKey(KeyCode.DownArrow)) { movedirectiony = -1; }
        else movedirectiony = 0;

        transform.localScale = new Vector3(-lookdirection, 1, 1); //캐릭터 좌우 바라보기
        if(movedirection == 0) animator.SetBool("Walk", false);

        if (Input.GetKey(KeyCode.LeftControl) && canrapidfire == true) rapidfire();
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.animator.SetBool("Rapidfire", false); canmove = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canflowerbomb == true) StartCoroutine(flowerbomb());

        if (Input.GetKey(KeyCode.Space)) summonerfire();
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            canmove = 1;
            rigid2D.gravityScale = 1.8f;
            canjump = true;
            candown = true;
            canrapidfire = true;
            canflowerbomb = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
            summoner.GetComponent<SummonerBehaviour>().endfire();
        }


        if (canmove == 1) walk();
        else if (canmove == 2) rapidWalk();

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (canjump == true) jump();
            else if (candoublejump == true) doublejump();
        }
        if (candown == true) StartCoroutine(Down());
        else if (Input.GetKeyUp(KeyCode.DownArrow)) animator.SetBool("Down", false);
        if (canteleport == true && Input.GetKeyDown(KeyCode.Z)) StartCoroutine( teleport());
    }

}
