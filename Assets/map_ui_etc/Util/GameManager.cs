using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    GameObject Damage;
    //public List<GameObject> DamagePool = new List<GameObject>();
    //private readonly int Maxcount = 100;
    public GameObject Damageprefabs;
    public GameObject gagein;
    public GameObject gageout;
    public GameObject finalatk3;
    private float damage;
    private int hitnum;

    public void ResetScene()
    {
        SceneManager.LoadScene("GameScene");
    }
    

    public IEnumerator damagein(GameObject monster, string name)
    {
        if (name.Contains("rapidfire")) {damage = 30000000; hitnum = 2; }
        else if(name.Contains("rapidbutterfly")) { damage = 5000000; hitnum = 1; }
        else if(name.Contains("finalatk")) { damage = 10000000; hitnum = 2; }
        else if (name.Contains("flower")) { damage = 60000000; hitnum = 6; }

        Damage = Damageprefabs;
        //StartCoroutine(monster.GetComponent<puBehaviour>().decreasehp(damage, hitnum));

        Vector3 Pos = monster.transform.position;
        for (int i = 0; i < hitnum; i++)
        {
            Damage.GetComponent<DamageBehaviour>().damage = (int)damage + (int)Random.Range(-damage *0.1f, damage*0.1f);
            Pos.y = Pos.y + 0.3f;
            Instantiate(Damage, Pos, transform.rotation);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void Increase_gagein()
    {
        gagein.GetComponent<Image>().fillAmount += 0.01f;
    }
    public void Increase_gageout()
    {
        gageout.GetComponent<Image>().fillAmount += 0.1f;
        if (gageout.GetComponent<Image>().fillAmount >= 1f) StartCoroutine(gagerelease());
    }

    IEnumerator gagerelease()
    {
        gageout.GetComponent<Image>().fillAmount = 0f;
        for (int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(0.1f);
            Instantiate(finalatk3, player.transform.position, player.transform.rotation);
            Instantiate(finalatk3, player.transform.position, player.transform.rotation);
            Instantiate(finalatk3, player.transform.position, player.transform.rotation);
        }

    }
    void Start()
    {
        /*for (int i = 0; i < Maxcount; i++)
        {
            GameObject a = Instantiate<GameObject>(DamagePrefab);
            a.gameObject.SetActive(false);
            finalPool.Add(a);
        }
        for (int i = 0; i < 2; i++) DamagePrefab[i].SetActive(false);*/
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
