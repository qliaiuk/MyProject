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
        Console.WriteLine(txt);
        // playerA发动一次攻击
        Tuple<RoleInfo,RoleInfo> result=AttackAction(playerA,playerB);
        playerA=result.Item1;
        playerB=result.Item2;
        // 战斗结束判断
        if((playerA.roleHp<=0)||(playerB.roleHp<=0))
        {
            txt="战斗结束!";
            Console.WriteLine(txt);
        }
        else
        {
            // playerB发动一次攻击
            AttackAction(playerB,playerA);
            playerB=result.Item1;
            playerA=result.Item2;
            if((playerA.roleHp<=0)||(playerB.roleHp<=0))
            {
                txt="战斗结束!";
                Console.WriteLine(txt);
            }
            else
            {
                // 本回合未结束战斗，递归循环
                BattleProcessCalculation(role,enemy,rounds);
            }
        }




    }

 
     //随机判定结果，输入一个概率p，返回是否判定成功
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


     // 普通攻击伤害计算,返回伤害值及是否暴击
    public Tuple<int,bool,bool> AttackDamageCalculate(RoleInfo atkRole,RoleInfo dmgRole)
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

        return Tuple.Create((int)finaldamage,isCritical,isDodge);
    }

    // 发动一次攻击
    public Tuple<RoleInfo,RoleInfo> AttackAction(RoleInfo pA,RoleInfo pB)
    {
        
        // 通过随机数方式决定出手角色使用哪种攻击方式
        float x=UnityEngine.Random.Range(0f,1f);
        if(x<0.33f)
        {
            // 发动普通攻击
            Tuple<int,bool,bool> atkResult;
            atkResult=AttackDamageCalculate(pA,pB);
            string txt=""; // 输出文字
            if(atkResult.Item2)    // 是否造成会心一击
            {
                if(atkResult.Item3)    //是否闪避
                {
                    txt=pA.roleName+"发动会心一击,试图就此结束战斗，然而"+pB.roleName+"凭借鬼魅的身法，躲开了这致命一击。";
                    Console.WriteLine(txt);
                }
                else
                {
                    txt=pA.roleName+"发动会心一击,精准命中！对"+pB.roleName+"造成"+atkResult.Item1.ToString()+"伤害！";
                    Console.WriteLine(txt);
                    pB.roleHp-=atkResult.Item1;
                    txt=pB.roleName+"剩余生命值"+pB.roleHp;
                    Console.WriteLine(txt);
                }
            }
            else
            {
                if(atkResult.Item3)    //是否闪避
                {
                    txt=pA.roleName+"发动攻击,招招致命,"+pB.roleName+"却身轻如燕，将攻势尽数躲开。";
                    Console.WriteLine(txt);
                }
                else
                {
                    txt=pA.roleName+"发动攻击,行动如风，出手犀利，"+pB.roleName+"躲闪不及，受到类"+atkResult.Item1.ToString()+"伤害。";
                    Console.WriteLine(txt);
                    pB.roleHp-=atkResult.Item1;
                    txt=pB.roleName+"剩余生命值"+pB.roleHp;
                    Console.WriteLine(txt);
                }
            }
            
            
        }
        else if(x<0.66f)
        {
            // 释放法术
        }
        else
        {
            // 使用法宝
        }





        return Tuple.Create(pA,pB);

    }





}
