using UnityEngine;

namespace SombraStudios.Shared.Gameplay
{
    /// <summary>
    /// transform.LookAt: Simple instruction to rotate an object to look at another.
    /// Quaternion.LookRotation to look at another Transform.
    /// </summary>
    public class LookAt : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
	
	    [SerializeField]
        private float _speed;

        void Update()
        {
            //TransformLookAt();
            //QuaternionLookRotation();
		    //QuaternionLookRotationWithLerp();

            //LookAtMousePosition();
            //LookAtMouseClick();
        }

        private void TransformLookAt()
        {
            transform.LookAt(_target.position, Vector3.up);
        }

        private void QuaternionLookRotation()
        {
            Vector3 relativePos = _target.position - transform.position;
            transform.rotation = Quaternion.LookRotation(relativePos);
        }
	
	    private void QuaternionLookRotationWithLerp()
        {
            Vector3 relativePos = _target.position - transform.position;
            Quaternion newRotation = Quaternion.LookRotation(relativePos);
		    transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _speed * Time.deltaTime);
        }

        private void LookAtMousePosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                transform.LookAt(hit.point);
            }
        }

        private void LookAtMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    transform.LookAt(hit.point);
                }
            }
        }
    }
}
