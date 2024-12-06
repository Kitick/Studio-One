using UnityEngine;

public class Defense : MonoBehaviour {
	public enum DefenseType {Health, Armor, Sheild}
	public enum DamageType {Physical, Energy}

	[Header("0 = Health, 1 = Armor, 2 = Sheild")]
	[SerializeField] public int[] maxValues = new int[3];

	[Header("Debugging ONLY")]
	[SerializeField] private int[] defenseValues = new int[3];

	private void Awake(){
		for (int i = 0; i < defenseValues.Length; i++){
			defenseValues[i] = maxValues[i];
		}
	}

	public int GetDefense(DefenseType type) => defenseValues[(int)type];
	public int GetMaxDefense(DefenseType type) => maxValues[(int)type];

	private void SetDefense(DefenseType type, int value){
		int max = GetMaxDefense(type);

		if(value < 0){
			value = 0;
		}
		else if(value > max){
			value = max;
		}

		defenseValues[(int)type] = value;
	}

	private void Die(){

		AudioSource explosionSound = this.gameObject.GetComponent<AudioSource>();

		explosionSound.Play();
		
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
		}
	}

	public void DamageWith(DamageType type, int amount){
		switch(type){
			case DamageType.Physical: Damage(DefenseType.Armor, amount); break;
			case DamageType.Energy: Damage(DefenseType.Sheild, amount); break;
		}
	}
}