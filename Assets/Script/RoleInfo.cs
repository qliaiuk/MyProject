using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RoleInfo
{
    // 角色属性类
    // 人物属性
    public int roleAge; //当前年龄
    public string roleName; //姓名
    public int roleMaxAge; //寿命
    public string roleIdentity; //身份
    public string roleTitle; // 称号
    public int energyStone; //灵石

    // 基础属性
    public int roushen;  //肉身
    public int shenhun;  //神魂
    public int linming;  //灵敏
    public int gengu;  //根骨
    public string manaRoot; //灵根
    public int fortune; //机缘
    public int talent; //悟性
    public string bodyType; //体质

    // 修炼属性
    public string xiulianRank; //境界
    public int xiulianSpeed; //修炼速度
    public int roleExp; //修为
    public int roleMaxExp;  //进阶修为
    public string magicFormula; //功法
    public string magicList; //掌握法术列表
    public string magicWeaponList; //装备的法宝


    //  战斗属性
    public int roleHp;   //生命值
    public int roleMaxHp;   //最大生命值
    public int roleMp;     // 法力值
    public int roleMaxMp;   //最大法力值
    public int roleMagicAttack;   // 法术攻击力
    public int roleDirectAttack;   //肉身攻击力
    public int roleMentality;  //神识
    public int roleFlightSpeed;  // 遁速
    public int roleBodyMethod;  // 身法
    public int roleDef;  //防御



}
