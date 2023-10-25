using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class FightingControl : MonoBehaviour
{

   // public GameObject roleObject;  // 游戏角色
   // public GameObject enemyObject;  // 怪物角色

    public RoleInfo roleInfoData; //角色数据
    public RoleInfo enemyInfoData;  //怪物数据
    
    void Start()
    {
        // 获取角色的测试数据
        roleInfoData=GameObject.Find("GameManager").GetComponent<GameDataManager>().testPlayer;
        enemyInfoData=GameObject.Find("GameManager").GetComponent<GameDataManager>().testEnemy;
        // 战斗测试
        BattleProcessCalculation(roleInfoData,enemyInfoData);

    }

    // 战斗实现逻辑
    // 由于战斗为全自动进行，因此可以直接计算出战斗结果，实现“跳过战斗”功能
    // 在快速计算完战斗结果时，返回一个存储战斗过程的变量
    // 实际玩家观看的战斗，为“播放”这个存储战斗过程的结果

    public void BattleProcessCalculation(RoleInfo role,RoleInfo enemy,int rounds=1)
    {
        // 采用递归方式进行战斗模拟，每一个回合进行一次递归
        // 出手顺序由“遁速”和“身份”共同影响
        RoleInfo playerA;  // 先出手角色
        RoleInfo playerB;  // 后出手角色
        
        if((role.roleFlightSpeed+role.roleBodyMethod)>=(enemy.roleFlightSpeed+enemy.roleBodyMethod))
        {
            playerA=role;
            playerB=enemy;
        }
        else
        {
            playerA=enemy;
            playerB=role;
        }
        string txt="第"+rounds.ToString()+"回合";
        Debug.Log(txt);
        // playerA发动一次攻击
        Tuple<RoleInfo,RoleInfo> result=AttackAction(playerA,playerB);
        playerA=result.Item1;
        playerB=result.Item2;
        // 战斗结束判断
        if(playerA.roleHp<=0)
        {
            txt="战斗结束!"+playerB.roleName+"获胜!";
            Debug.Log(txt);
        }
        else if(playerB.roleHp<=0)
        {
            txt="战斗结束!"+playerA.roleName+"获胜!";
            Debug.Log(txt);           
        }  
        else
        {
            // playerB发动一次攻击
            AttackAction(playerB,playerA);
            playerB=result.Item1;
            playerA=result.Item2;
            if(playerA.roleHp<=0)
            {
                txt="战斗结束!"+playerB.roleName+"获胜!";
                Debug.Log(txt);
            }
            else if(playerB.roleHp<=0)
            {
                txt="战斗结束!"+playerA.roleName+"获胜!";
                Debug.Log(txt);  
            }
            else
            {
                // 本回合未结束战斗，递归循环
                BattleProcessCalculation(role,enemy,rounds+1);
            }
        }




    }

 
     /// <summary>
     /// 随机判定结果，输入一个概率p，返回是否判定成功
     /// </summary>
     /// <param name="p"></param>
     /// <returns></returns>
    public bool RandomDetermine(float p)
    {
        //随机判定结果，输入一个概率p，返回是否判定成功
        float x=UnityEngine.Random.Range(0f,1f);
        if (x<=p)
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }

    /// <summary>
    /// 发动一次攻击
    /// </summary>
    /// <param name="pA"></param>
    /// <param name="pB"></param>
    /// <returns></returns>
    public Tuple<RoleInfo,RoleInfo> AttackAction(RoleInfo pA,RoleInfo pB)
    {
        
        // 通过随机数方式决定出手角色使用哪种攻击方式
        float x=UnityEngine.Random.Range(0f,1f);
        if(x<0.33f)
        {
            // 发动普通攻击
            Tuple<int,bool,bool> atkResult;
            atkResult=NormalAttackDamageCalculate(pA,pB);
            string txt=""; // 输出文字
            if(atkResult.Item2)    // 是否造成会心一击
            {
                if(atkResult.Item3)    //是否闪避
                {
                    txt=pA.roleName+"发动会心一击,试图就此结束战斗，然而"+pB.roleName+"凭借鬼魅的身法，躲开了这致命一击。";
                    Debug.Log(txt);
                }
                else
                {
                    txt=pA.roleName+"发动会心一击,精准命中！对"+pB.roleName+"造成"+atkResult.Item1.ToString()+"伤害！";
                    Debug.Log(txt);
                    pB.roleHp-=atkResult.Item1;
                    txt=pB.roleName+"剩余生命值"+pB.roleHp;
                    Debug.Log(txt);
                }
            }
            else
            {
                if(atkResult.Item3)    //是否闪避
                {
                    txt=pA.roleName+"发动攻击,招招致命,"+pB.roleName+"却身轻如燕，将攻势尽数躲开。";
                    Debug.Log(txt);
                }
                else
                {
                    txt=pA.roleName+"发动攻击,行动如风，出手犀利，"+pB.roleName+"躲闪不及，受到了"+atkResult.Item1.ToString()+"伤害。";
                    Debug.Log(txt);
                    pB.roleHp-=atkResult.Item1;
                    txt=pB.roleName+"剩余生命值"+pB.roleHp;
                    Debug.Log(txt);
                }
            }
            
            
        }
        else if(x<0.66f)
        {
            // 释放法术
            Tuple<int,int,string,bool> atkResult;
            atkResult=ReleaseMagicDamageCalculate(pA,pB);
            string txt; // 输出文字

            if(atkResult.Item4)  // 是否闪避
            {
                txt=pA.roleName+"双手作法，口中默念法咒，只见他一声大喝："+atkResult.Item3+"!";
                Debug.Log(txt);
                txt=pB.roleName+"身形一个恍惚，轻松躲过，还嘲笑道：“施法前摇这么长啊！”";
                Debug.Log(txt);
            }
            else
            {
                txt=pA.roleName+"双手作法，口中默念法咒，只见他一声大喝："+atkResult.Item3+"!";
                Debug.Log(txt);
                txt=pB.roleName+"被这狂暴的法力波动所惊讶，竟来不及躲闪，硬生生的吃了这一击。受到"+atkResult.Item1.ToString()+"点伤害";
                Debug.Log(txt);
                pB.roleHp-=atkResult.Item1;
                txt=pB.roleName+"剩余生命值"+pB.roleHp;
                Debug.Log(txt);
            }

        }
        else
        {
            // 使用法宝
            Tuple<int,int,string,bool,bool> atkResult;
            atkResult=MagicWeapenAttackDamageCalculate(pA,pB);
            string txt; // 输出文字

            if(atkResult.Item4)   // 是否闪避
            {
                if(atkResult.Item5)   // 是否暴击
                {
                    txt=pA.roleName+"一拍储物袋，一道金光闪过，竟然是一件法宝！";
                    Debug.Log(txt);
                    txt=pA.roleName+"大喝:"+atkResult.Item3;
                    Debug.Log(txt);
                    txt=pB.roleName+"连忙躲闪，摔了个灰头土脸，但也勉强躲开了这惊天一击，想想还是有些后怕。";
                    Debug.Log(txt);

                }
                else
                {
                    txt=pA.roleName+"一拍储物袋，一道金光闪过，竟然是一件法宝！";
                    Debug.Log(txt);
                    txt=pA.roleName+"大喝:"+atkResult.Item3;
                    Debug.Log(txt);
                    txt=pB.roleName+"连忙躲闪，摔了个灰头土脸，但也勉强躲开了这惊天一击，想想还是有些后怕。";
                    Debug.Log(txt);
                }
            }
            else
            {
                if(atkResult.Item5)   // 是否暴击
                {
                    txt=pA.roleName+"默念口诀，一道金光从口中飞出。"+atkResult.Item3+"!，去！";
                    Debug.Log(txt);
                    txt=pB.roleName+"从未见过如此犀利的法宝，想要施展身法躲避，但可惜速度不够，被击中了要害之处，受到了"+atkResult.Item1+"点伤害";
                    Debug.Log(txt);
                    pB.roleHp-=atkResult.Item1;
                    txt=pB.roleName+"剩余生命值"+pB.roleHp;
                    Debug.Log(txt);
                }
                else
                {
                    txt=pA.roleName+"默念口诀，一道金光从口中飞出。"+atkResult.Item3+"!，去！";
                    Debug.Log(txt);
                    txt=atkResult.Item3+"速度奇快无比，转瞬间已经就在眼前，对"+pB.roleName+"造成了"+atkResult.Item1+"点伤害";
                    Debug.Log(txt);
                    pB.roleHp-=atkResult.Item1;
                    txt=pB.roleName+"剩余生命值"+pB.roleHp;
                    Debug.Log(txt);
                }
                
            }
        }





        return Tuple.Create(pA,pB);

    }


     /// <summary>
     /// 普通攻击伤害计算,返回伤害值及是否暴击、是否闪避
     /// </summary>
     /// <param name="atkRole"></param>
     /// <param name="dmgRole"></param>
     /// <returns></returns>
    public Tuple<int,bool,bool> NormalAttackDamageCalculate(RoleInfo atkRole,RoleInfo dmgRole)
    {
        // 普通攻击伤害计算,返回伤害值及是否暴击
        float finaldamage;  // 最终造成的伤害
        bool isCritical=false;  // 是否造成暴击
        bool isDodge=false;  //是否闪避
        float x1=UnityEngine.Random.Range(0.9f,1.1f);  // 伤害波动，每次攻击造成的伤害范围为90%~110%
        finaldamage=atkRole.roleDirectAttack*x1;

        if(RandomDetermine(0.2f))  // 是否暴击判定,暴击概率0.2，暴击倍率1.5
        {
            finaldamage=finaldamage*1.5f;
            isCritical=true;
        }

        // 躲避判定，躲避概率为0.2
        if(RandomDetermine(0.2f))
        {
            // 闪避成功
            finaldamage=0f;
            isDodge=true;
        }

        // 伤害减免，基于受伤者的护甲值进行减免
        float rate=1f-dmgRole.roleDef/(dmgRole.roleDef+1000);
        finaldamage=finaldamage*rate;

        return Tuple.Create((int)finaldamage,isCritical,isDodge);
    }

    /// <summary>
    /// 释放法术伤害计算，返回伤害值，消耗的法力值，法术名称,是否闪避
    /// </summary>
    public Tuple<int,int,string,bool> ReleaseMagicDamageCalculate(RoleInfo atkRole,RoleInfo dmgRole)
    {
        float finaldamage;  // 最终伤害值
        int consumeMana;  // 消耗的法力值
        bool isDodge=false;  // 是否闪避
        string magicName;  // 法术名称

        // 随机释放一种法术，此处示例三种法术
        if(RandomDetermine(0.33f))
        {
            //火球术
            consumeMana=100;
            magicName="火球术";
            finaldamage=1.3f*atkRole.roleMagicAttack; //该法术造成1.3f倍率的法术伤害

        }
        else if(RandomDetermine(0.5f))
        {
            consumeMana=120;
            magicName="奔雷咒";
            finaldamage=0.1f*atkRole.roleMaxHp+0.6f*atkRole.roleMagicAttack;  // 0.1倍最大生命值+0.6倍法功伤害
            

        }
        else
        {
            consumeMana=150;
            magicName="大力金刚拳";
            finaldamage=atkRole.roleDirectAttack+0.3f*atkRole.roleMagicAttack; // 1倍普攻伤害+0.3倍法伤

        }
        
        // 伤害波动，每次攻击造成的伤害范围为95%~105%
        float x1=UnityEngine.Random.Range(0.95f,1.05f);  
        finaldamage=atkRole.roleMagicAttack*x1;

        // 躲避判定，躲避概率为0.1
        if(RandomDetermine(0.1f))
        {
            // 闪避成功
            finaldamage=0f;
            isDodge=true;
        }

        // 伤害减免，基于受伤者的护甲值进行减免
        float rate=1f-dmgRole.roleDef/(dmgRole.roleDef+1000);
        finaldamage=finaldamage*rate;        

        return Tuple.Create((int)finaldamage,consumeMana,magicName,isDodge);
    }

/// <summary>
/// 使用法宝造成伤害计算
/// 返回值伤害、消耗法力值、法宝名称、是否闪避，是否暴击
/// </summary>
/// <param name="atkRole">攻击者</param>
/// <param name="dmgRole">受击者</param>
public Tuple<int,int,string,bool,bool> MagicWeapenAttackDamageCalculate(RoleInfo atkRole,RoleInfo dmgRole)
{
        float finaldamage;  // 最终伤害值
        int consumeMana;  // 消耗的法力值
        bool isDodge=false;  // 是否闪避
        string magicName;  // 法术名称
        bool isCritical=false; //是否暴击

        // 随机使用一种法宝，此处示例三种法宝
        if(RandomDetermine(0.33f))
        {
            magicName="九火神龙罩";
            finaldamage=atkRole.roleDirectAttack+dmgRole.roleHp*0.1f;  // 造成1倍物理伤害+0.1倍受伤者当前生命值伤害
            consumeMana=30;
        }
        else if(RandomDetermine(0.5f))
        {
            magicName="混元金斗";
            finaldamage=2*atkRole.roleDirectAttack;  // 简单粗暴，造成2倍物理伤害
            consumeMana=60;
        }
        else
        {
            magicName="生死金石";
            float atkrate=UnityEngine.Random.Range(0.03f,3f);
            finaldamage=atkrate*atkRole.roleDirectAttack; //造成0.03至3倍的输出，要么生，要么死！
            consumeMana=60;

        }

        // 躲避判定，躲避概率为0.15
        if(RandomDetermine(0.15f))
        {
            // 闪避成功
            finaldamage=0f;
            isDodge=true;
        }
        //暴击判定，暴击概率为0.1
        if(RandomDetermine(0.1f))
        {
            // 闪避成功
            finaldamage=finaldamage*2.0f;
            isCritical=true;
        }

        // 伤害波动，每次攻击造成的伤害范围为100%~120%
        float x1=UnityEngine.Random.Range(1f,1.2f);  
        finaldamage=atkRole.roleMagicAttack*x1;
        // 伤害减免，基于受伤者的护甲值进行减免
        float rate=1f-dmgRole.roleDef/(dmgRole.roleDef+1000);
        finaldamage=finaldamage*rate;  


        return Tuple.Create((int)finaldamage,consumeMana,magicName,isDodge,isCritical);
}



}
