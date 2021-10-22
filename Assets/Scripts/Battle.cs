using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Battle : MonoBehaviour
{
    private AttackPriority attackPriority;
    private GameObject attackingHero;

    void Start()
    {
        attackPriority = GetComponent<AttackPriority>();
    }

    void Update()
    {
        //Начало атаки
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            attackingHero = attackPriority.attackPriorityList.Peek();
            attackingHero.GetComponent<Attack>().AttackStart();
            
        }

        //Конец атаки
        if (Input.GetKeyDown(KeyCode.G))
        {
            attackingHero.GetComponent<Attack>().AttackEnd(attackPriority);
        }
    }
}
