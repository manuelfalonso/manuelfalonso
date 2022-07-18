using UnityEngine;

/// <summary>
/// Spins the object in the defined x, y and/or z axis.
/// </summary>
public class Spinner : MonoBehaviour 
{
	[SerializeField]
    private float rotation_speed_x = 0f;
	[SerializeField]
    private float rotation_speed_y = 1f;
	[SerializeField]
    private float rotation_speed_z = 0f;

    void Update() {
		Spin();
    }
	
	private void Spin() {
		this.transform.Rotate(rotation_speed_x, rotation_speed_y, rotation_speed_z, Space.Self);
	}
}