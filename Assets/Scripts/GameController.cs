using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Transform enemy;

    [Header("User Interface")]
    public Text scoreText;
    public Text waveText;

    private int score = 0;
    private int waveNumber = 0;



    [Header("Wave Properties")]
    public float timeBeforeSpawning = 1.5f;
    public float timeBetweenEnemies = 0.25f;
    public float timeBeforeWaves = 2.0f;

    public int enemiesPerWave = 10;
    private int currentNumberOfEnemies = 0;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnEnemies());
	}

    IEnumerator SpawnEnemies()
    {
        // 게임 시작 전에 플레이어에게 시간을 준다.
        yield return new WaitForSeconds(timeBeforeSpawning);

        // timeBeforeSpawning 이 지나면 이 루프에 들어간다.
        while(true)
        {
            // 이전 웨이브의 적이 모두 죽기 전에는 새로운 적을 생성하지 않는다.
            if (currentNumberOfEnemies <= 0)
            {
                waveNumber++;
                waveText.text = "Wave: " + waveNumber;

                // 10개의 적을 무작위 위치에 생성한다.
                for (int i = 0; i < enemiesPerWave; i++)
                {
                    // 화면 밖에 생성하길 원한다.
                    // Random.range는 첫번째와 두 번째 파라미터 사이의 숫자를 내보낸다.
                    float randDistance = Random.Range(10, 25);

                    // 적은 어느 방향에서도 올 수 있다.
                    Vector2 randDirection = Random.insideUnitCircle;
                    Vector3 enemyPos = this.transform.position;

                    // 거리와 방향을 사용해서 위치를 설정한다.
                    enemyPos.x += randDirection.x * randDistance;
                    enemyPos.y += randDirection.y * randDistance;

                    // 적을 생성하고 생성된 적의 숫자를 증가시킨다
                    // Instantiate 는 첫 번째 파라미터의 복제를 만들고
                    // 두 번째 파라미터의 위치에 세 번째 파라미터의 방향으로 생성한다.
                    Instantiate(enemy, enemyPos, this.transform.rotation);
                    currentNumberOfEnemies++;
                    yield return new WaitForSeconds(timeBetweenEnemies);
                }
            }

            // 다음 웨이브의 적을 생성할 때까지 어느 정도의 시간을 기다려야 하는가
            yield return new WaitForSeconds(timeBeforeWaves);
        }
    }

    public void IncreaseScore(int increase)
    {
        score += increase;
        scoreText.text = "Score: " + score;
    }
	
    public void KilledEnemy()
    {
        currentNumberOfEnemies--;   
    }

	// Update is called once per frame
	void Update () {
		
	}
}
