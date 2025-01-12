using UnityEngine;
using UnityEngine.SceneManagement;

namespace Effects
{
    public class ScrollBackground : MonoBehaviour
    {
        private const float ResetPositionY = 20f;
        private const float StartPositionY = 38f;
        private const float Speed = 2f;


        private void Update()
        {
            transform.position += new Vector3(0, -Speed * Time.deltaTime, 0);
            if (transform.position.y <= ResetPositionY)
            {
                transform.position = new Vector3(transform.position.x, StartPositionY, transform.position.z);
            }
        }
    }
}