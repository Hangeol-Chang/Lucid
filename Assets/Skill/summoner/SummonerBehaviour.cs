using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonerBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject fireee;
    Rigidbody2D rig;
    Animator ani;

    private float maxspeed = 5f;
    private float direction;
    private float lookd;
    private float yjudge;
    Vector3 playerPos;
    Vector3 Pos;
    private float firetime;
    private bool canmove = true;

    private float firecool = 30;
    private float firecoolstart = 0;
    private bool canfire = true;
    // Start is called before the first frame update
    public void endfire()
    {

    }
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
    void Update()
    {
        if(canfire == false)
        {
            if (Time.time - firecoolstart > firecool)
            {
                canfire = true;
                firecoolstart = 0;
            }
        }
        playerPos = player.transform.position;
        direction = player.GetComponent<PlayerBehaviour>().lookdirection;
        Pos = new Vector3(playerPos.x - direction * 1f, playerPos.y + 0.5f, 0);


        if (canmove == true)
        {
            /*if (Vector3.Distance(transform.position, playerPos) >= 4)
            {*/
                this.transform.position = Pos;
                this.rig.velocity = new Vector2(0, 0);
            //}
            /*
            yjudge = (Pos.y - this.transform.position.y) / Mathf.Abs(Pos.y - this.transform.position.y);

            lookd = Mathf.Sign(-(Pos.x - this.transform.position.x));
            this.transform.localScale = new Vector3(lookd * 0.5f, 0.5f, 1);

            //===================예전코드
            if (Pos.x == this.transform.position.x) this.rig.velocity = 0.3f * this.rig.velocity;
            if (Mathf.Abs(rig.velocity.magnitude) < maxspeed || lookd == Mathf.Sign(rig.velocity.x))
                rig.AddForce(new Vector2(Pos.x - this.transform.position.x, Pos.y - this.transform.position.y) / 10);
            if (Mathf.Abs(rig.velocity.magnitude) < maxspeed || yjudge == Mathf.Sign(rig.velocity.y))
                rig.AddForce(new Vector2(Pos.x - this.transform.position.x, Pos.y - this.transform.position.y) / 10);
            //===================
            */
        }
    }
    public IEnumerator fire()
    {
        if (canfire == true)
        {
            canfire = false;
            firecoolstart = Time.time;

            ani.SetBool("teleport", true);
            yield return new WaitForSeconds(0.2f);
            fireee.SetActive(true);
            fireee.transform.position = new Vector3(playerPos.x - direction * 5f, playerPos.y + 2, 0);
            gameObject.SetActive(false);
        }
    }
}
