using UnityEngine;
using System.Collections;

/**
 * Change the camera into an orbital camera. An orbital is a camera
 * that can be rotated and that will automatically reorient itself to
 * always point to the target.
 * 
 * The orbit camera allow zooming and dezooming with the mouse wheel.
 * 
 * By clicking the mouse and dragging on the screen, the camera is moved. 
 * The angle of rotation  correspond to the distance the cursor travelled. 
 *  
 * The camera will keep the angular position when the button is pressed. To
 * rotate more, simply repress the mouse button et move the cursor.
 *
 * This script must be added on a camera object.
 *
 * @author Mentalogicus
 * @date 11-2011
 */
public class OrbitCamera : MonoBehaviour
{
    //The target of the camera. The camera will always point to this object.
    [SerializeField]
    private Transform _target;

    //The default distance of the camera from the target.
    [SerializeField]
    private float _distance = 20.0f;

    //Control the speed of zooming and dezooming.
    [SerializeField]
    private float _zoomStep = 1.0f;

    //The speed of the camera. Control how fast the camera will rotate.
    [SerializeField]
    private float _xSpeed = 1f;
    [SerializeField]
    private float _ySpeed = 1f;

    //The position of the cursor on the screen. Used to rotate the camera.
    private float _x = 0.0f;
    private float _y = 0.0f;
    [SerializeField]
    private float _altura = 1.0f;
    [SerializeField]
    private float d;

    //Distance vector. 
    private Vector3 _distanceVector;

    [SerializeField]
    private int OtherSide;

    private Quaternion rotation;
    private Vector3 position;

    //PRUEBAS
    [SerializeField]
    private Transform t;
    [SerializeField]
    private float vel = 0.001f;
    [SerializeField]
    private GameObject Manager;

    /**
  * Move the camera to its initial position.
  */
    void Start()
    {
        _distanceVector = new Vector3(0.0f, _altura, -_distance);

        Vector2 angles = this.transform.localEulerAngles;
        _x = angles.x;
        _y = angles.y;

        this.Rotate(_x, _y);
    }

    /**
  * Rotate the camera or zoom depending on the input of the player.
  */
    void LateUpdate()
    {
        if (_target)
        {
            this.RotateControls();
            this.Zoom();

            this.Rotate(_x, _y); //PRUEBA
        }
    }

    /**
  * Rotate the camera when the first button of the mouse is pressed.
  * 
  */
    void RotateControls()
    {
        if (Input.GetButton("Fire1"))
        {
            _x += Input.GetAxis("Mouse X") * _xSpeed;
            _y += -Input.GetAxis("Mouse Y") * _ySpeed;

            //_x -= Time.deltaTime* _xSpeed;
            this.Rotate(_x, _y);
        }
        //MARKETING
        if (Manager.GetComponent<JuegoManager>().isMARKETING)
        {
            t.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
            if (Input.GetKey(KeyCode.Backspace))
            {
                t.position = new Vector3(0, 0, 0);
                vel = 0.001f;
            }
            if (Input.GetKey("[+]"))
                vel += 0.001f;
            if (Input.GetKey("[-]"))
                vel -= 0.001f;
            if (Input.GetKey("q"))
                t.position += transform.up * vel;
            if (Input.GetKey("a"))
                t.position -= transform.up * vel;
            if (Input.GetKey("right"))
                t.position += transform.right * vel;
            if (Input.GetKey("left"))
                t.position -= transform.right * vel;
            if (Input.GetKey("up"))
            {
                //t.position += transform.forward * vel;
                _distance -= vel;
                _distanceVector = new Vector3(0.0f, _altura, -_distance);
            }
            if (Input.GetKey("down"))
            {
                //t.position -= transform.forward * vel;
                _distance += vel;
                _distanceVector = new Vector3(0.0f, _altura, -_distance);
            }
            if (Input.GetMouseButton(2))
            {
                t.position += transform.right * Input.GetAxis("Mouse X");
                t.position += t.transform.forward * Input.GetAxis("Mouse Y");
            }
            if (Input.GetMouseButton(1))
            {
                t.position += transform.up * Input.GetAxis("Mouse Y");
            }
        }
    }

    /**
  * Transform the cursor mouvement in rotation and in a new position
  * for the camera.
  */
    void Rotate(float x, float y)
    {
        //Transform angle in degree in quaternion form used by Unity for rotation.
        rotation = Quaternion.Euler(y + d, x + OtherSide, 0.0f);

        //The new position is the target position + the distance vector of the camera
        //rotated at the specified angle.
        position = rotation * _distanceVector + _target.position;

        //Update the rotation and position of the camera.
        transform.rotation = rotation;
        transform.position = position;
    }

    /**
  * Zoom or dezoom depending on the input of the mouse wheel.
  */
    void Zoom()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0.0f)
        {
            this.ZoomOut();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0.0f)
        {
            this.ZoomIn();
        }

    }

    /**
  * Reduce the distance from the camera to the target and
  * update the position of the camera (with the Rotate function).
  */
    void ZoomIn()
    {
        _distance -= _zoomStep;
        _distanceVector = new Vector3(0.0f, _altura, -_distance);
        this.Rotate(_x, _y);
    }

    /**
  * Increase the distance from the camera to the target and
  * update the position of the camera (with the Rotate function).
  */
    void ZoomOut()
    {
        _distance += _zoomStep;
        _distanceVector = new Vector3(0.0f, _altura, -_distance);
        this.Rotate(_x, _y);
    }

} //End class