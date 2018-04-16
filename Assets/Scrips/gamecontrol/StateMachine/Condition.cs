/********************************************
 * Maded by Jesús Gracia Güell 15/7/2017	*
********************************************/
[System.Serializable]
public class Condition{

	public int varToCheck = 0;
	public float compareWith = 0;
	public ConditionType type;

	public enum ConditionType{
		EqualTo,
		LowerThan,
		HigerThan
	}

	/// <summary>
	/// Check if specified i passes the condition.
	/// </summary>
	/// <param name="i">value to compare.</param>
	public bool check (float i){
		switch (type) {
		case ConditionType.EqualTo:
			return i == compareWith;
		case ConditionType.HigerThan:
			return i > compareWith;
		case ConditionType.LowerThan:
			return i < compareWith;
		default:
			return false;
		}
	}

}
