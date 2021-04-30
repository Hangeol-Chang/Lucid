using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerBombBehaviourr : MonoBehaviour
{
    public GameObject prefab;

    private List<GameObject> Pool = new List<GameObject>();
    private readonly int Maxcount = 9;
    private int Index = 0;

    public GameObject player;
    PlayerBehaviour playerbehaviour;
    private float direction;
    private Vector3 summonPos;
    private Quaternion rotate;
    private Vector3 scale;
    IEnumerator die()
    {
        yield return new WaitForSeconds(2.4f);
        gameObject.SetActive(false);
    }
    void Start()
    {
        playerbehaviour = player.GetComponent<PlayerBehaviour>();

        for (int i = 0; i < Maxcount; i++)
        {
            GameObject a = Instantiate<GameObject>(prefab);
            a.gameObject.SetActive(false);
            Pool.Add(a);
        }
        rotate.x = 0; rotate.y = 0;
        prefab.SetActive(false);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        direction = playerbehaviour.lookdirection;
        StartCoroutine(flowerBomb());
        scale.x = direction * 0.5f;
        scale.y = 0.5f;
        scale.z = 0.5f;
    }
    
    private IEnumerator flowerBomb()
    {
        yield return null;

        if (Pool[Index].gameObject.activeSelf)
        {
            yield break;
        }
        summonPos.y = player.transform.position.y + Random.Range(0.3f, 0.7f);
        summonPos.z = 0;
        summonPos.x = -direction * (player.transform.position.x + 3f);

        for (int i = 0; i < 3; i++)
        {
            summonPos.x += direction * 2f;
            Pool[Index].transform.position = summonPos;
            Pool[Index].transform.rotation = Quaternion.Euler(0,0,Random.Range(-50,51));
            Pool[Index].transform.localScale = scale;
            Pool[Index].transform.localScale = scale * (i + 1) / 1.5f;

            Pool[Index].gameObject.SetActive(true);
            if (Index >= Maxcount - 1) Index = 0;
            else Index++;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
