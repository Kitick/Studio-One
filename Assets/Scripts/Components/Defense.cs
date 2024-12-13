using UnityEngine;

public class Defense : MonoBehaviour {
	public enum DefenseType {Health, Armor, Sheild}
	public enum DamageType {Physical, Energy}

	[Header("0 = Health, 1 = Armor, 2 = Sheild")]
	[SerializeField] private float[] maxValues = new float[3];

	[Header("Debugging ONLY")]
	[SerializeField] private float[] currentValues = new float[3];

	private void Awake(){
		if(maxValues.Length != currentValues.Length && maxValues.Length != 3){
			Debug.LogError("Defense: maxValues and currentValues must be the same length.");
		}

		for (int i = 0; i < currentValues.Length; i++){
			currentValues[i] = maxValues[i];
		}
	}

	public float GetDefense(DefenseType type) => currentValues[(int)type];
	public float GetMaxDefense(DefenseType type) => maxValues[(int)type];

	private void SetDefense(DefenseType type, float value){
		float max = GetMaxDefense(type);

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

		SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
		sprite.enabled = false;

		Destroy(gameObject, 1.0f);
	}

	public void Restore(float amount, DefenseType type){
		SetDefense(type, GetDefense(type) + amount);
	}

	public void TakeDamage(DefenseType type, float amount){
		float remaining = GetDefense(type) - amount;
		SetDefense(type, remaining);

		if(remaining >= 0){ return; }

		remaining = Mathf.Abs(remaining);

		if(type == DefenseType.Health){
			Die();
		}
		else{
			TakeDamage(DefenseType.Health, remaining);
		}
	}

	public void DamageWith(DamageType type, int amount){
		switch(type){
			case DamageType.Physical: TakeDamage(DefenseType.Armor, amount); break;
			case DamageType.Energy: TakeDamage(DefenseType.Sheild, amount); break;
		}
	}
}