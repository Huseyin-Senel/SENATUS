using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour
{

    public GameObject enemy;
    public GameObject character;

    private Enviorment enviorment;

    public List<int> killCounts = new List<int>();


    void Start()
    {

        enviorment = GameObject.Find("Enviorment").GetComponent<Enviorment>();


        for (int i =0;i < Level.selectedLevel.TeamCount;i++)
        {
            killCounts.Add(0);
        }




        createCharacters();

       
    }

    public bool checkEnd()
    {
        return false;
    }

    public void respawn(Hero hero,Bandit bandit)
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


            this.Wait(1f, () =>
            {
                Destroy(hero.gameObject);
                GameObject characterObj = Instantiate(character, new Vector2(pos.x,1.6f), Quaternion.identity, transform);
                enviorment.allCharacters.Add(characterObj);
                GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = characterObj.transform;
            });


        }
        else
        {
            enviorment.allCharacters.Remove(bandit.gameObject);
            int team = bandit.team;

            this.Wait(1f, () =>
            {
                Destroy(bandit.gameObject);

                GameObject enemyObj = Instantiate(enemy, pos, Quaternion.identity, transform);
                enviorment.allCharacters.Add(enemyObj);
                Bandit bandit2 = enemyObj.GetComponent<Bandit>();
                bandit2.canlan(team, Level.selectedLevel.EnemyHealth, Level.selectedLevel.EnemyDamage);
            });
        }



    }

    private void createCharacters()
    {
        GameObject characterObj = Instantiate(character, new Vector3(0, 1.6f, 0), Quaternion.identity, transform);
        enviorment.allCharacters.Add(characterObj);
        GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = characterObj.transform;

        for (int i = 0; i < Level.selectedLevel.TeamCount - 1; i++)
        {
            for (int j = 0; j < Level.selectedLevel.TeamPlayerCount; j++)
            {
                GameObject enemyObj;
                if (i == 0)
                {
                    enemyObj = Instantiate(enemy, new Vector3(14.5f - j, 0, 0), Quaternion.identity, transform);
                }
                else
                {
                    enemyObj = Instantiate(enemy, new Vector3(-14.5f + j, 0, 0), Quaternion.identity, transform);
                }
                enviorment.allCharacters.Add(enemyObj);
                Bandit bandit = enemyObj.GetComponent<Bandit>();
                bandit.canlan(i + 1, Level.selectedLevel.EnemyHealth, Level.selectedLevel.EnemyDamage);
            }
        }
    }
}
