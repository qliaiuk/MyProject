using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightingControl : MonoBehaviour
{

    public GameObject roleObject;  // 游戏角色
    public GameObject enemyObject;  // 怪物角色

    public RoleInfo roleInfoData; //角色数据
    public EnemyInfo enemyInfoData;  //怪物数据
    


    // 战斗实现逻辑
    // 由于战斗为全自动进行，因此可以直接计算出战斗结果，实现“跳过战斗”功能
    // 在快速计算完战斗结果时，返回一个存储战斗过程的变量
    // 实际玩家观看的战斗，为“播放”这个存储战斗过程的结果

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
