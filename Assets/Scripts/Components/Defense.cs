using UnityEngine;

public class Defense : MonoBehaviour {
	[SerializeField] private HealthBar healthBar;
	public enum DefenseType {Health, Armor, Sheild}
	public enum DamageType {Physical, Energy}

	[Header("0 = Health, 1 = Armor, 2 = Sheild")]
	[SerializeField] private int[] maxValues = new int[3];

	private int[] currentValues = new int[3];

	private void Awake(){
        for (int i = 0; i < currentValues.Length; i++){
			currentValues[i] = maxValues[i];
		}
		if (healthBar != null){
			healthBar.UpdateHealthBar(GetMaxDefense(DefenseType.Health), GetDefense(DefenseType.Health));
		}
    }

	public int GetDefense(DefenseType type) => currentValues[(int)type];
	public int GetMaxDefense(DefenseType type) => maxValues[(int)type];

	private void SetDefense(DefenseType type, int value){
		int max = GetMaxDefense(type);

		if(value < 0){
			value = 0;
		}
		else if(value > max){
			value = max;
		}

		currentValues[(int)type] = value;
	}

	private void Die(){
		AudioSource explosionSound = gameObject.GetComponent<AudioSource>();
		explosionSound.Play();

		SelectionManager selectionManager = FindObjectOfType<SelectionManager>();
		selectionManager.Deselect(gameObject.GetComponent<Selectable>());

		SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
		if(renderer != null){ renderer.enabled = false; }

		if(healthBar != null){ healthBar.gameObject.SetActive(false); }

		Destroy(gameObject, 1.0f);
	}

	public void Restore(int amount, DefenseType type){
		SetDefense(type, GetDefense(type) + amount);
	}

	public void Damage(DefenseType type, int amount){
		int remaining = GetDefense(type) - amount;
		SetDefense(type, remaining);


		if(remaining >= 0){ return; }

		remaining = Mathf.Abs(remaining);

		if(type == DefenseType.Health){
			Die();
		}
		else{
			Damage(DefenseType.Health, remaining);
			healthBar.UpdateHealthBar(maxValues[0], currentValues[0]);
        }

    }

	public void DamageWith(DamageType type, int amount){
		switch(type){
			case DamageType.Physical: Damage(DefenseType.Armor, amount); break;
			case DamageType.Energy: Damage(DefenseType.Sheild, amount); break;
		}
	}
}