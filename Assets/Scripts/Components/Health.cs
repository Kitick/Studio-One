using UnityEngine;

public class Health : MonoBehaviour {
	[SerializeField] private int maxHealth = 100;
	[SerializeField] private int currentHealth;

	private void Awake(){
		currentHealth = maxHealth;
	}

	public int getHealth() => currentHealth;

	public void Heal(int amount){
		currentHealth += amount;

		if (currentHealth > maxHealth){
			currentHealth = maxHealth;
		}
	}

	public void Damage(int amount){
		currentHealth -= amount;

		if (currentHealth <= 0){
			Destroy(gameObject);
		}
	}
}