using System;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttackSkill : ISkill
{
    //SkillData data;

    public void MakeEffect()
    {
        Debug.Log("Do Damage");
    }

    public void PostEffect()
    {
    }

    public void PreCheck()
    {
    }
}
