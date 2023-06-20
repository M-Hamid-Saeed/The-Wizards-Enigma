using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    private Slider slider;
    private void Awake() {
        slider = GetComponent<Slider>();
    }
    public void setMaxHealth(int health) {
        slider.value = health;
        slider.maxValue = health;
    }
    public void setHealth(int health) {
        slider.value = health;
    }
  
}
