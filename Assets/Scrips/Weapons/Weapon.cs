/********************************************
 * Maded by Jesús Gracia Güell 18/6/2017	*
********************************************/


public interface Weapon {

	int amunition {
		get;
		set;
	}

	WeaponType type {
		get;
	}

	/// <summary>
	/// Framecall contains all the control of the weapon functionality
	/// </summary>
	void framecall ();

	/// <summary>
	/// Exit called when te weapon gets exchanged.
	/// </summary>
	void exit();

	/// <summary>
	/// Atack is called to fire the weapon forcefully.
	/// </summary>
	void atack ();
}
