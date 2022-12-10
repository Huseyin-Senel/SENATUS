using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClickHolderLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI teamCount, teamPlayerCount, enemyHealth;


    public void setItem(Level level)
    {
        teamCount.text = level.TeamCount.ToString();
        teamPlayerCount.text = level.TeamPlayerCount.ToString();
        enemyHealth.text = level.EnemyHealth.ToString();
    }
}
