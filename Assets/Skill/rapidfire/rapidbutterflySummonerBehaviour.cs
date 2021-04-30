using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rapidbutterflySummonerBehaviour : MonoBehaviour
{
    public GameObject[] butterfly = new GameObject[3];
    public GameObject arrow;


    private List<GameObject> arrowPool = new List<GameObject>();
    private List<GameObject> flyPool = new List<GameObject>();
    private readonly int arrowMaxcount = 11;
    private readonly int flyMaxcount = 16;
    private int curflyIndex = 0;
    private int curarrowIndex = 0;

    private float summonPosy;

    PlayerBehaviour playerbehaviour;
    public GameObject player;
    private void Start()
    {
        playerbehaviour = player.GetComponent<PlayerBehaviour>();

        for(int i = 0; i < arrowMaxcount; i++)
        {
            GameObject a = Instantiate<GameObject>(arrow);
            a.gameObject.SetActive(false);
            arrowPool.Add(a);
        }
        for (int i = 0; i < flyMaxcount; i++)
        {
            int d = i % 3;
            GameObject b = Instantiate<GameObject>(butterfly[d]);
            b.gameObject.SetActive(false);
            flyPool.Add(b);
        }
        for (int i = 0; i < 3; i++) butterfly[i].SetActive(false);
        arrow.SetActive(false);
    }

    private void OnEnable()
    {
        InvokeRepeating("summonfly", 0, 0.1f);
        InvokeRepeating("summonarrow", 0, 0.1f);
    }
    private void OnDisable()
    {
        CancelInvoke("summonfly");
        CancelInvoke("summonarrow");
    }
    void summonarrow()
    {
        if (arrowPool[curarrowIndex].gameObject.activeSelf)
        {
            return;
        }
        arrowPool[curarrowIndex].transform.position = new Vector3(player.transform.position.x + playerbehaviour.lookdirection, player.transform.position.y - 0.1f, 0);
        arrowPool[curarrowIndex].gameObject.SetActive(true);
        if (curarrowIndex >= arrowMaxcount - 1) curarrowIndex = 0;
        else curarrowIndex++;
    }
    void summonfly()
    {
        if (flyPool[curflyIndex].gameObject.activeSelf)
        {
            return;
        }
        summonPosy = Random.Range(-0.4f, 0.2f);
        flyPool[curflyIndex].transform.position = new Vector3(player.transform.position.x + playerbehaviour.lookdirection * 0.5f, player.transform.position.y + summonPosy, 0);
        flyPool[curflyIndex].gameObject.SetActive(true);
        if (curflyIndex >= flyMaxcount - 1) curflyIndex = 0;
        else curflyIndex++;
    }
}
