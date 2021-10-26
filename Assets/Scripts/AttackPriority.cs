using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPriority : MonoBehaviour
{
    public GameObject[] dinoArr;
    [HideInInspector] public Queue<GameObject> attackPriorityList;
    private GameObject firstHero;

    void Start()
    {
        dinoArr = GameObject.FindGameObjectsWithTag("Dino");
        attackPriorityList = new Queue<GameObject>(dinoArr.Length);

        DeterminingPriority();
    }

    //Распределение приоритета атаки в зависимости от параметра speed
    public void DeterminingPriority()
    {
        for (int i = 0; i < dinoArr.Length; i++)
        {
            for (int j = 0; j < dinoArr.Length; j++)
            {
                if (dinoArr[i].GetComponent<DinoStats>().speed > dinoArr[j].GetComponent<DinoStats>().speed)
                {
                    GameObject tamp = dinoArr[i];
                    dinoArr[i] = dinoArr[j];
                    dinoArr[j] = tamp;
                }
            }
        }

        for (int i = 0; i < dinoArr.Length; i++)
        {
            attackPriorityList.Enqueue(dinoArr[i]);
        }
    }

    //Из названия понятно, что эта штука передаёт право атаки
    public void ReallocationPriority()
    {
            firstHero = attackPriorityList.Dequeue();

            attackPriorityList.Enqueue(firstHero);
    }
}
