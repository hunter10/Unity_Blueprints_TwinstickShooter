using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

    public float playerSpeed = 4.0f;
    private float currentSpeed = 0.0f;
    private Vector3 lastMovement = new Vector3();

    // 발사될 레이저
    public Transform laser;

    // 우주선의 중심과 레이저 간의 거리
    public float laserDistance = .2f;

    // 다시 발사할 때까지 기다려야 하는 시간(초)
    public float timeBetweenFires = .3f;

    // 값이 0보다 작거나 같으면 다시 발사할 수 있다.
    private float timeTilNextFire = 0.0f;

    // 레이저를 발사하기 위해 사용하는 버튼들
    public List<KeyCode> shootButton;

    // 발사할 때 재생되는 사운드  
    public AudioClip shootSound;
    // AudioSource 컴포넌트의 레퍼런스 
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
	
	void Update () 
    {
        if (!PauseMenuBehaviour.isPaused)
        {
            Rotation();
            Movement();

            foreach (KeyCode element in shootButton)
            {
                if (Input.GetKey(element) && timeTilNextFire < 0)
                {
                    timeTilNextFire = timeBetweenFires;
                    ShootLaser();
                    break;
                }
            }

            timeTilNextFire -= Time.deltaTime;
        }
	}

    // 레이저를 생성하고 초기 위치를 우주선 앞으로 지정한다.
    void ShootLaser()
    {
        audioSource.PlayOneShot(shootSound);

        // 레이저의 위치를 플레이어의 위치에 따라 지정한다.
        Vector3 laserPos = this.transform.position;

        // 레이저의 각도를 가운데에서 밖으로 향하도록 한다
        float rotationAngle = transform.localEulerAngles.z - 90;

        // 우주선에서 laserDistance만큼 떨어진 우주선 바로 앞의 위치를 계산한다.
        laserPos.x += (Mathf.Cos((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);
        laserPos.y += (Mathf.Sin((rotationAngle) * Mathf.Deg2Rad) * -laserDistance);

        Instantiate(laser, laserPos, this.transform.rotation);


    }

    void Rotation()
    {
        Vector3 worldPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(worldPos);

        float dx = this.transform.position.x - worldPos.x;
        float dy = this.transform.position.y - worldPos.y;

        float angle = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, angle + 90));

        this.transform.rotation = rot;
    }

    void Movement()
    {
        Vector3 movement = new Vector3();

        movement.x += Input.GetAxis("Horizontal");
        movement.y += Input.GetAxis("Vertical");

        movement.Normalize();

        if(movement.magnitude > 0)
        {
            currentSpeed = playerSpeed;
            this.transform.Translate(movement * Time.deltaTime * playerSpeed, Space.World);
            lastMovement = movement;
        }else{
            this.transform.Translate(lastMovement * Time.deltaTime * currentSpeed, Space.World);
            currentSpeed *= .9f;
        }
    }
}
