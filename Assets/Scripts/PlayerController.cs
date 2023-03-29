using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int SwipeSpeed = 10;
    public int ForwardSpeed = 5;
    public static bool Isbattle = false;

    private bool _move = false;
    
    void Update()
    {
        if (_move == true && Isbattle == false)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Time.deltaTime * ForwardSpeed);
        }

        if (Input.GetMouseButton(0) && Isbattle == false)
        {
            _move = true;
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitPosition, 100))              
            {
                Vector3 sweepPosition = hitPosition.point;
                sweepPosition.y = transform.position.y;
                sweepPosition.z = transform.position.z;
                transform.position = Vector3.MoveTowards(transform.position, sweepPosition, Time.deltaTime * SwipeSpeed);
            }
        }
    }
}
