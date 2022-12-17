using Cinemachine;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Scene : MonoBehaviour
{

    public GameObject enemyPrefab;
    public GameObject characterPrefab;
    public GameObject counterPrefab;

    private Enviorment enviorment;
    private KillCounter killCounter;
    private TextMeshProUGUI winLose;
    private Image shadow;

    public KillCounter KillCounter { get => killCounter; set => killCounter = value; }
    private bool end = false;
    private GameObject hero1;


    private AudioSource win, lose;

    private int attack;
    private int armor;
    private int potCount;

    void Start()
    {

        enviorment = GameObject.Find("Enviorment").GetComponent<Enviorment>();
        killCounter = GameObject.Find("Kills").GetComponent<KillCounter>();
        winLose = GameObject.Find("WinLose").GetComponent<TextMeshProUGUI>();
        shadow = GameObject.Find("Shadow").GetComponent<Image>();

        win = GetComponents<AudioSource>()[0];
        lose = GetComponents<AudioSource>()[1];

        getPlayerPrefs();
        shadow.DOFade(0f,1f);
        createCharacters();


        this.Wait(1f, () => {
            startCounter();
            shadow.enabled = false;
        });


        TextMeshProUGUI potTxt = GameObject.Find("PotTxt").GetComponent<TextMeshProUGUI>();
        potTxt.text = potCount.ToString();

    }

    private void setPlayerPrefs(bool win)
    {
        switch (Level.selectedLevel.Leveln)
        {
            case Level.Levels.Level1:
                PlayerPrefs.SetInt("level2", Convert.ToInt32(win));
                if (win)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 100);
                }
                
                break;
            case Level.Levels.Level2:
                PlayerPrefs.SetInt("level3", Convert.ToInt32(win));
                if (win)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 150);
                }
                
                break;
            case Level.Levels.Level3:
                PlayerPrefs.SetInt("level4", Convert.ToInt32(win));
                if (win)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 200);
                }
                
                break;
            case Level.Levels.Level4:
                PlayerPrefs.SetInt("level5", Convert.ToInt32(win));
                if (win)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 250);
                }
                
                break;
            case Level.Levels.Level5:
                PlayerPrefs.SetInt("level6", Convert.ToInt32(win));
                if (win)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 300);
                }
                
                break;
            case Level.Levels.Level6:
                PlayerPrefs.SetInt("levelBoss", Convert.ToInt32(win));
                if (win)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 350);
                }
                
                break;
            case Level.Levels.LevelBoss:
                if (win)
                {
                    PlayerPrefs.SetInt("money", PlayerPrefs.GetInt("money") + 500);
                }

                break;
            default:
                break;
        }

        if (Level.selectedLevel.Leveln == Level.Levels.LevelBoss)
        {
            PlayerPrefs.SetInt("exitPoint", 0);
        }
        else
        {
            PlayerPrefs.SetInt("exitPoint", 8);
        }

        PlayerPrefs.SetInt("pot", hero1.GetComponent<Hero>().PotCount);
    }

    

    private void getPlayerPrefs()
    {
        int Cattack = 0;
        int Carmor = 0;

        if (PlayerPrefs.GetInt("armor") != -1)
        {
            Carmor += Item.createItem((Item.Items)PlayerPrefs.GetInt("armor")).Power;
        }
        if (PlayerPrefs.GetInt("shield") != -1)
        {
            Carmor += Item.createItem((Item.Items)PlayerPrefs.GetInt("shield")).Power;
        }
        if (PlayerPrefs.GetInt("sword") != -1)
        {
            Cattack += Item.createItem((Item.Items)PlayerPrefs.GetInt("sword")).Power;
        }
        if (PlayerPrefs.GetInt("bow") != -1)
        {
            Cattack += Item.createItem((Item.Items)PlayerPrefs.GetInt("bow")).Power;
        }
        potCount = PlayerPrefs.GetInt("pot");
        armor = Carmor;
        attack = Cattack;
    }


    private void startCounter()
    {

        GameObject canvas = GameObject.Find("Canvas");

        Instantiate(counterPrefab, canvas.transform.position, Quaternion.identity, canvas.transform).GetComponent<Counter>().run(3.ToString());
        this.Wait(1f,() => {
            Instantiate(counterPrefab, canvas.transform.position, Quaternion.identity, canvas.transform).GetComponent<Counter>().run(2.ToString());
        });

        this.Wait(2f, () => {
            Instantiate(counterPrefab, canvas.transform.position, Quaternion.identity, canvas.transform).GetComponent<Counter>().run(1.ToString());
        });
    }

    public void endFunction(bool win)
    {
        if (!end)
        {
            end = true;

            setPlayerPrefs(win);

            if (win)
            {
                winLose.text = "Kazandin";
                this.win.Play();

            }
            else
            {
                winLose.text = "Kaybettin";
                this.lose.Play();
            }
            winLose.DOFade(1f, 1f);

            this.Wait(2f, () => {
                shadow.enabled = true;
                shadow.DOFade(1f, 1f);
            });



            this.Wait(4f, () => {
                SceneManager.LoadScene("MainPage");
            });
        }
        
       
    }

    public void respawn(Hero hero,Bandit bandit)
    {

        if (!end)
        {
            int random = Random.Range(0, 3);
            Vector2 pos;
            switch (random)
            {
                case 0:
                    pos = new Vector2(0, 0);
                    break;
                case 1:
                    pos = new Vector2(14.5f, 0);
                    break;
                case 2:
                    pos = new Vector2(-14.5f, 0);
                    break;
                default:
                    pos = new Vector2(0, 0);
                    break;
            }


            if (hero != null)
            {
                enviorment.allCharacters.Remove(hero.gameObject);
                potCount = hero.gameObject.GetComponent<Hero>().PotCount;                

                this.Wait(1f, () =>
                {
                    Destroy(hero.gameObject);
                    GameObject characterObj = Instantiate(characterPrefab, new Vector2(pos.x, 1.6f), Quaternion.identity, transform);
                    hero1 = characterObj;
                    enviorment.allCharacters.Add(characterObj);
                    GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = characterObj.transform;
                    characterObj.GetComponent<Hero>().active = true;
                    characterObj.GetComponent<Hero>().Armor = armor;
                    characterObj.GetComponent<Hero>().Damage = attack;
                    characterObj.GetComponent<Hero>().PotCount = potCount;
                });


            }
            else
            {
                enviorment.allCharacters.Remove(bandit.gameObject);
                int team = bandit.team;

                this.Wait(1f, () =>
                {
                    if (bandit != null)
                    {
                        Destroy(bandit.gameObject);
                    }
                    

                    GameObject enemyObj = Instantiate(enemyPrefab, pos, Quaternion.identity, transform);
                    enviorment.allCharacters.Add(enemyObj);
                    Bandit bandit2 = enemyObj.GetComponent<Bandit>();
                    bandit2.canlan(team, Level.selectedLevel.EnemyHealth, Level.selectedLevel.EnemyDamage,Level.selectedLevel.Leveln);
                    bandit2.active = true;
                });
            }
        }

    }

    private void createCharacters()
    {
        GameObject characterObj = Instantiate(characterPrefab, new Vector3(0, 1.6f, 0), Quaternion.identity, transform);
        hero1 = characterObj;
        enviorment.allCharacters.Add(characterObj);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = characterObj.transform;


        characterObj.GetComponent<Hero>().Armor = armor;
        characterObj.GetComponent<Hero>().Damage = attack;
        characterObj.GetComponent<Hero>().PotCount = potCount;
        this.Wait(4f, () => {
            characterObj.GetComponent<Hero>().active = true;           
        });



        for (int i = 0; i < Level.selectedLevel.TeamCount - 1; i++)
        {
            for (int j = 0; j < Level.selectedLevel.TeamPlayerCount; j++)
            {
                GameObject enemyObj;
                if (i == 0)
                {
                    enemyObj = Instantiate(enemyPrefab, new Vector3(14.5f - j, 0, 0), Quaternion.identity, transform);
                }
                else
                {
                    enemyObj = Instantiate(enemyPrefab, new Vector3(-14.5f + j, 0, 0), Quaternion.identity, transform);
                }
                enviorment.allCharacters.Add(enemyObj);
                Bandit bandit = enemyObj.GetComponent<Bandit>();
                bandit.canlan(i + 1, Level.selectedLevel.EnemyHealth, Level.selectedLevel.EnemyDamage, Level.selectedLevel.Leveln);
                this.Wait(4f, () => {
                    bandit.active = true;
                });

            }
        }
    }
}
