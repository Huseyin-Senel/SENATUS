using DG.Tweening;
using TMPro;
using UnityEngine;

public class Scane2Events : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] keys;
    public GameObject[] tetxs;
    public GameObject duvar1;

    public GameObject level1, level2, level3, level4, level5, level6;


    private GameObject character;
    private bool run = false;


    private Envanter envanter;
    private Hero hero;
    private Dukkan dukkan;


    private float exitPoint;

    void Start()
    {
        envanter = GameObject.Find("Envanter").GetComponent<Envanter>();
        dukkan = GameObject.Find("Dukkan").GetComponent<Dukkan>();
        character = GameObject.Find("Hero");
        hero = character.GetComponent<Hero>();

        initilazePlayerPrefs();
        getPlayerPrefs();


        if ()
        {
            duvar1.GetComponent<BoxCollider2D>().enabled = true;
            character.GetComponent<Hero>().active = true;
            run = false;

            character.transform.localPosition = new Vector2(PlayerPrefs.GetInt("exitPoint"), character.transform.localPosition.y);
        }
    }
    void Update()
    {
        if (run)
        {
            character.GetComponent<Hero>().scriptRun(1f);
        }

    }


    public void clickControllerStart()
    {
        foreach (GameObject obj in keys)
        {
            obj.GetComponent<SpriteRenderer>().DOFade(1, 2);
        }

        foreach (GameObject obj in tetxs)
        {
            obj.GetComponent<TextMeshPro>().DOFade(1, 2);
        }



        run = true;
        this.Wait(2f, () =>
        {
            duvar1.GetComponent<BoxCollider2D>().enabled = true;
            character.GetComponent<Hero>().active = true;
            run = false;
        });
    }

    public static void initilazePlayerPrefs()
    {
        if (!PlayerPrefs.HasKey("level1"))
        {
            PlayerPrefs.SetInt("level1", 1);
        }
        if (!PlayerPrefs.HasKey("level2"))
        {
            PlayerPrefs.SetInt("level2", 0);
        }
        if (!PlayerPrefs.HasKey("level3"))
        {
            PlayerPrefs.SetInt("level3", 0);
        }
        if (!PlayerPrefs.HasKey("level4"))
        {
            PlayerPrefs.SetInt("level4", 0);
        }
        if (!PlayerPrefs.HasKey("level5"))
        {
            PlayerPrefs.SetInt("level5", 0);
        }
        if (!PlayerPrefs.HasKey("level6"))
        {
            PlayerPrefs.SetInt("level6", 0);
        }
        if (!PlayerPrefs.HasKey("levelBoss"))
        {
            PlayerPrefs.SetInt("levelBoss", 0);
        }



        if (!PlayerPrefs.HasKey("money"))
        {
            PlayerPrefs.SetInt("money", 0);
        }
        if (!PlayerPrefs.HasKey("armor"))
        {
            PlayerPrefs.SetInt("armor", -1);
        }
        if (!PlayerPrefs.HasKey("shield"))
        {
            PlayerPrefs.SetInt("shield", -1);
        }
        if (!PlayerPrefs.HasKey("sword"))
        {
            PlayerPrefs.SetInt("sword", -1);
        }
        if (!PlayerPrefs.HasKey("bow"))
        {
            PlayerPrefs.SetInt("bow", -1);
        }
        if (!PlayerPrefs.HasKey("pot"))
        {
            PlayerPrefs.SetInt("pot", 0);
        }


        if (!PlayerPrefs.HasKey("exitPoint"))
        {
            PlayerPrefs.SetInt("exitPoint", -16);
        }

    }
    private void getPlayerPrefs()
    {
        int Cattack = 0;
        int Carmor = 0;
        GameObject a;

        if (PlayerPrefs.GetInt("armor") !=-1)
        {
            a = dukkan.createItem((Item.Items)PlayerPrefs.GetInt("armor"), envanter.armor);
            Carmor+= a.GetComponent<Item>().Power;

        }
        if (PlayerPrefs.GetInt("shield") != -1)
        {
            a = dukkan.createItem((Item.Items)PlayerPrefs.GetInt("shield"), envanter.shiled);
            Carmor += a.GetComponent<Item>().Power;
        }
        if (PlayerPrefs.GetInt("sword") != -1)
        {
            a = dukkan.createItem((Item.Items)PlayerPrefs.GetInt("sword"), envanter.sword);
            Cattack += a.GetComponent<Item>().Power;
        }
        if (PlayerPrefs.GetInt("bow") != -1)
        {
            a = dukkan.createItem((Item.Items)PlayerPrefs.GetInt("bow"), envanter.bow);
            Cattack += a.GetComponent<Item>().Power;
        }
        envanter.Money = PlayerPrefs.GetInt("money");
        envanter.PotCount = PlayerPrefs.GetInt("pot");
        envanter.Carmor1 = Carmor;
        envanter.Cattack1 = Cattack;
        envanter.refreshUI();

        exitPoint = PlayerPrefs.GetInt("exitPoint");


        level1.GetComponent<Level>().setAvaliable(PlayerPrefs.GetInt("level1") != 0);
        level2.GetComponent<Level>().setAvaliable(PlayerPrefs.GetInt("level2") != 0);
        level3.GetComponent<Level>().setAvaliable(PlayerPrefs.GetInt("level3") != 0);
        level4.GetComponent<Level>().setAvaliable(PlayerPrefs.GetInt("level4") != 0);
        level5.GetComponent<Level>().setAvaliable(PlayerPrefs.GetInt("level5") != 0);
        level6.GetComponent<Level>().setAvaliable(PlayerPrefs.GetInt("level6") != 0);
    } //level boss ekle
    public void setPlayerPrefs()
    {
        if (envanter.armor.GetComponent<Slot>().getItem() != null)
        {
            PlayerPrefs.SetInt("armor", (int)envanter.armor.GetComponent<Slot>().getItem().GetComponent<Item>().Item1);
        }
        else
        {
            PlayerPrefs.SetInt("armor", -1);
        }

        if (envanter.shiled.GetComponent<Slot>().getItem() != null)
        {
            PlayerPrefs.SetInt("shield", (int)envanter.shiled.GetComponent<Slot>().getItem().GetComponent<Item>().Item1);
        }
        else
        {
            PlayerPrefs.SetInt("shield", -1);
        }

        if (envanter.sword.GetComponent<Slot>().getItem() != null)
        {
            PlayerPrefs.SetInt("sword", (int)envanter.sword.GetComponent<Slot>().getItem().GetComponent<Item>().Item1);
        }
        else
        {
            PlayerPrefs.SetInt("sword", -1);
        }

        if (envanter.bow.GetComponent<Slot>().getItem() != null)
        {
            PlayerPrefs.SetInt("bow", (int)envanter.bow.GetComponent<Slot>().getItem().GetComponent<Item>().Item1);
        }
        else
        {
            PlayerPrefs.SetInt("bow", -1);
        }

        PlayerPrefs.SetInt("money", envanter.Money);
        PlayerPrefs.SetInt("pot", envanter.PotCount);     
    }

    public void resetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        initilazePlayerPrefs();
    }



}
