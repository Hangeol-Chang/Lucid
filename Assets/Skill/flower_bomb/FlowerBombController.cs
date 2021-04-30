using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBombController : MonoBehaviour
{
    PlayerBehaviour playerbehaviour;
    public GameObject player;

    public GameObject[] prefab = new GameObject[5];
    private readonly int Maxcount1 = 9;
    private List<GameObject> flowermain = new List<GameObject>();

    public GameObject[] prefabw = new GameObject[3];
    private readonly int Maxcount2 = 3;
    private Vector3[] flowerwposition = new Vector3[3];
    private List<GameObject> flowersub = new List<GameObject>();

    public GameObject hiteff;

    private Vector3[] flowerposition = new Vector3[2];

    private float direction;
    private float scale = 1;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌");
        if (collision.gameObject.tag.Contains("Monster"))
        {
            Vector3 Pos = new Vector3(collision.transform.position.x , collision.transform.position.y, 0);
            StartCoroutine(hit(Pos));
        }

    }
    IEnumerator hit(Vector3 Pos)
    {
        for (int i = 0; i < 6; i++)
        {
            Pos = new Vector3(Pos.x + Random.Range(-0.3f, 0.3f), Pos.y + Random.Range(-0.3f, 0.3f), 0);
            Instantiate(hiteff, Pos, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
    void Awake()
    {
        playerbehaviour = player.GetComponent<PlayerBehaviour>();

        for (int i = 0; i < Maxcount1; i++)
        {
            GameObject a = Instantiate<GameObject>(prefab[i%5]);
            a.gameObject.SetActive(false);
            flowermain.Add(a);
        }
        for (int i = 0; i < Maxcount2; i++)
        {
            GameObject a = Instantiate<GameObject>(prefabw[i % 3]);
            a.gameObject.SetActive(false);
            flowersub.Add(a);
        }

        for (int i = 0;i<5;i++) prefab[i].SetActive(false);
        for (int i = 0; i < 3; i++) prefabw[i].SetActive(false);
        gameObject.SetActive(false);
    }
    private void Start()
    {
        direction = playerbehaviour.lookdirection;
        StartCoroutine(firstline());
        StartCoroutine(secondline());

        Invoke("da", 2.8f);
    }
    private void OnEnable()
    {
        direction = playerbehaviour.lookdirection;
        this.transform.localScale = new Vector3(direction, 1, 1);
        StartCoroutine(firstline());
        StartCoroutine(secondline());

        Invoke("da", 2.8f);
    }
    void da()
    {
        gameObject.SetActive(false);
    }

    IEnumerator firstline()
    {
        if (flowermain[0].gameObject.activeSelf)
        {
            yield break;
        }
        flowermain[0].transform.position = new Vector3(player.transform.position.x +2*direction, player.transform.position.y + Random.Range(0.1f, 0.5f),player.transform.position.z);
        flowermain[0].transform.localScale = new Vector3(1, 1, 1);
        flowermain[0].gameObject.SetActive(true);

        flowerposition[0] = flowermain[0].transform.position;
        flowerposition[1] = flowermain[0].transform.position;
        for (int q = 1; q < 9; q=q+2)
        {
            scale *= 0.9f;
            flowerposition[0] = new Vector3(flowerposition[0].x + Random.Range(0.5f, 0.8f), flowerposition[0].y + Random.Range(-0.1f, 0.4f) * direction, 0);
            flowerposition[1] = new Vector3(flowerposition[1].x + Random.Range(-0.5f, -0.8f), flowerposition[1].y + Random.Range(0.1f, -0.4f) * direction, 0);

            for (int i = 0; i < 2; i++)
            {
                flowermain[q + i].transform.position = flowerposition[i];
                flowermain[q + i].transform.localScale = new Vector3(1,1,1) * scale;
                flowermain[q + i].SetActive(true);

                yield return new WaitForSeconds(0.1f);
            }
        }
        scale = 1;


    }

    IEnumerator secondline()
    {
        for (int a = 0; a < 3; a++) flowerwposition[a] = player.transform.position;
        for (int a = 0; a < 3; a++)
        {
            switch (a)
            {
                case 0:
                    flowerwposition[a].x += 2 * direction;
                    flowerwposition[a].y += Random.Range(0.0f, 0.5f);
                    break;
                case 1:
                    flowerwposition[a].x += 3.8f * direction;
                    flowerwposition[a].y += Random.Range(-0.5f, 0.0f);
                    break;
                case 2:
                    flowerwposition[a].x += 0.2f * direction;
                    flowerwposition[a].y += Random.Range(0.0f, 0.5f);
                    break;
            }
            flowersub[a].transform.position = flowerwposition[a];
            flowersub[a].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }

    }
}
