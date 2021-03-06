using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoStats : MonoBehaviour
{
    public float speed; //Скорость, по которой определяеться очередь хода
    public float health; //Здоровье динозавра (по нему определяеться цель для атаки)
    public float moveSpeed; //Скорость передвижения динозавра
    public float damage;
    public Transform attackPoint; //Точка для рейкаста (что бы не кастить из центра персонажа)
    

    public Image healthBar;


    public float agroDistance; //Длинна вектора рейкаста
    [Tooltip("Layer mask index")]
    public int team; //Из пометки понятно

    private void Start()
    {
        gameObject.layer = team;
    }

    
}
