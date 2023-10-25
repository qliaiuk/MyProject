using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    // 该脚本用于游戏运行时保存和传递数据，在切换场景时不销毁
    public RoleInfo testPlayer;
    public RoleInfo testEnemy;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);   // 场景切换不删除该物体


        testPlayer=new RoleInfo();
        testEnemy=new RoleInfo();

        // 读取json文件中的战斗测试数据
        string path = "Assets/Script/testData.json";
        string jsonString = File.ReadAllText(path);
        listdata tt=JsonUtility.FromJson<listdata>(jsonString);
        testPlayer.roleName=tt.list[0].name;
        testPlayer.roleHp=tt.list[0].HP;
        testPlayer.roleMp=tt.list[0].MP;
        testPlayer.roleMagicAttack=tt.list[0].MagicAttack;
        testPlayer.roleDirectAttack=tt.list[0].DirectAttac;
        testPlayer.roleFlightSpeed=tt.list[0].FlightSpeed;
        testPlayer.roleBodyMethod=tt.list[0].BodyMethod;
        testPlayer.roleDef=tt.list[0].Def;

        testEnemy.roleName=tt.list[1].name;
        testEnemy.roleHp=tt.list[1].HP;
        testEnemy.roleMp=tt.list[1].MP;
        testEnemy.roleMagicAttack=tt.list[1].MagicAttack;
        testEnemy.roleDirectAttack=tt.list[1].DirectAttac;
        testEnemy.roleFlightSpeed=tt.list[1].FlightSpeed;
        testEnemy.roleBodyMethod=tt.list[1].BodyMethod;
        testEnemy.roleDef=tt.list[1].Def;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable] 
public class testinfo
{
    public string name;
    public int HP;
    public int MP;
    public int MagicAttack;
    public int DirectAttac;
    public int FlightSpeed;
    public int BodyMethod;
    public int Def;
}
public class listdata
{
    public List<testinfo> list;
}