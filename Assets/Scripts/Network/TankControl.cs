using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class TankControl : NetworkBehaviour
{


    public float m_RotSpeed;           // tank rotation speed modifier
    public float m_Speed;              // tank movement speed modifier
    public GameObject m_Barrel;        // location for spawning shot projectiles

    public float m_HoverSpeed;         // tank hover speed modifier
    Vector3 buoyancyCentreOffSet;      // offset the hover force to allow for random water physics

    private Rigidbody m_Vehicle;       // reference to the tank's physics component
    private GameObject m_Turret;       // reference to the child turret object

    private float m_ReloadTimer;       // counts down to 0, tank can only shoot when not counting down

    //controls
    private bool controller = false;


    // TODO: tank is not assigned a team
    private TeamNames m_Team = TeamNames.Red;          // enumerated team for this tank


    // Use this for initialization
    void Start()
    {
        m_Vehicle = GetComponent<Rigidbody>();
        m_Turret = FindClosestTarget("Turret");
        m_Barrel = GameObject.Find("BulletSpawn");
        m_ReloadTimer = 0.0f;

        Transform MainCam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

    }

    // perform hovering of the car rigid body
    private void Hover()
    {
        const float BASEHOVERLEVEL = 2.0f;  // default max hover height in Y
        float hoverLevel = 2.0f;            // adjusted max hover height
        float floatHeight = 0.1f;           // 
        float bounceDamp = 0.05f;           //
        float forceFactor;                  // 
        Vector3 actionPoint;
        Vector3 upLift;                     // force to add to tank model

        // raycast to the floor to adjust hover height
        RaycastHit floorDetect; // holds ID of object detected by RayCast
        if (Physics.Raycast(transform.position, Vector3.down, out floorDetect, 100))
        {
            hoverLevel = BASEHOVERLEVEL + floorDetect.point.y;  // add Y pos of object below to adjust hover level
        }

        actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffSet); // generate action point for water physicis
        forceFactor = 1f - ((actionPoint.y - hoverLevel) / floatHeight);    // get the force by which to move the tank up

        if (forceFactor > 0.0f)   // check if tank needs to hover up or down
        {
            upLift = -Physics.gravity * (forceFactor - m_Vehicle.velocity.y * bounceDamp);  // add amount to hover up by using gravity
            m_Vehicle.AddRelativeForce(upLift * m_HoverSpeed);
            // line below makes hover act like floating in water
            //m_Vehicle.AddForceAtPosition(upLift, actionPoint); 
        }
    }

    // shooting behaviour
    private void Shoot()
    {
        float primaryAttack = Input.GetAxis("Fire1");
        bool fired = false;

        //Reloading
        if (m_ReloadTimer > 0.0f)
        {
            m_ReloadTimer -= Time.deltaTime;
        }
        else
        {
            m_ReloadTimer = 0.0f;
        }

        //Shooting
        if ((primaryAttack < -0.6f || Input.GetMouseButtonDown(0)) && !fired)
        {
            CmdFire();
            fired = true;
        }

        if (primaryAttack > -0.5f)
        {
            fired = false;
        }


    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        float shootHorizontal = Input.GetAxis("RightStick X");
        float shootVertical = Input.GetAxis("RightStick Y"); ;

        Vector3 turretRotate = new Vector3(shootHorizontal, 0f, shootVertical);

        Vector3 move = new Vector3(moveHorizontal, 0f, moveVertical);

        //Checks for controllers
        if (Input.GetJoystickNames().Length > 0)
        {
            m_Turret.transform.rotation = Quaternion.LookRotation(new Vector3(turretRotate.x, 0f, turretRotate.z));
        }
        else //mouse/keyboard controls
        {
            float tankHeight = m_Turret.transform.position.y + transform.position.y;    //Height for aiming reticle
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;


        }

        transform.rotation = Quaternion.LookRotation(new Vector3(m_Vehicle.velocity.x, 0f, m_Vehicle.velocity.z));
        m_Vehicle.AddForce(move * m_Speed);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //first check if this is the client
        if (isLocalPlayer)
            {
                return;
            }
        Move();
        Hover();
        Shoot();
    }

    // find the closest object, child of the tank
    GameObject FindClosestTarget(string trgt)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(trgt);

        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }

    // shooting command received and handled by the server
    [Command]
    void CmdFire()
    {
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();  // get bullet object from pool

        if (bullet != null) // check if object pool returned a bullet
        {
            if (m_ReloadTimer <= 0.0f)    // only shoot if not waiting for reload
            {
                bullet.GetComponent<BulletTravel>().SetTeam(m_Team);
                bullet.transform.position = m_Barrel.transform.position;
                bullet.transform.rotation = m_Barrel.transform.rotation;
                bullet.SetActive(true);
                m_ReloadTimer = bullet.GetComponent<BulletTravel>().m_ReloadSpeed;    // get reload time from projectile
            }
        }

        // server spawns bullet on all clients (NOTE: convert this to work with the object pool, NetworkServer.Spawn instantiates)
        NetworkServer.Spawn(bullet);
    }
}

