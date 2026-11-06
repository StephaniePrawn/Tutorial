using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTask2 : MonoBehaviour
{
    [SerializeField] int Score = 0;
    [SerializeField] float Timer = 60;
    [SerializeField] bool CompletionStatus = false;


    [SerializeField] TextMeshProUGUI ScoreTextBox;
    [SerializeField] TextMeshProUGUI TimerTextBox;
    [SerializeField] TextMeshProUGUI CompletionTextBox;

    [SerializeField] AudioSource playerSound;
    [SerializeField] AudioClip coinClip;
    [SerializeField] AudioClip gameOverClip;

    [SerializeField] Rigidbody physicsBody; 

    [SerializeField] float moveSpeed = 10;

    private void OnMouseDown()
    {
        playerSound.PlayOneShot(coinClip);
    }

    void AddPoints(int amount)
    {
        Score += amount;
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        physicsBody.linearVelocity = new Vector3(1, 0 , 0);
        physicsBody.AddForce(new Vector3(200, 0 , 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playerSound != null)
        {
            playerSound.Play();
        }
        
        Score = Score + 1;

        Destroy(other.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(0, 0, Time.deltaTime * 0.5f); //movement of ball

        Timer = Timer - Time.deltaTime;
        Timer = Mathf.Clamp(Timer, 0, 60);
        UpdateTextBoxes();

        float zInput = (Keyboard.current.wKey.value - Keyboard.current.sKey.value);
        float xInput = (Keyboard.current.sKey.value - Keyboard.current.aKey.value);
        //Velocity = new Vector3(xInput, 0, zInput) * Time.deltaTime * moveSpeed;

        Vector3 direction = new Vector3(xInput, 0, zInput);
        physicsBody.linearVelocity = moveSpeed * direction;

        float X = Mathf.Repeat(transform.position.x + 5f, 10f) - 5f;
        float Z = Mathf.Repeat(transform.position.z + 5f, 10f) - 5f;
        transform.position = new Vector3(X, transform.position.y, Z); 

        if (transform.position.z < -5)
            transform.position += new Vector3(0, 0, 10);

        if (transform.position.z > 5)
            transform.position -= new Vector3(0, 0, 10);

        if (Score>=3)
        { 
            CompletionStatus = true; 
        }

        if (Timer <=0 && !CompletionStatus)
        {
        //    Destroy(gameObject);
                    
        }

        if (Timer <= 0 && Score!=3)
        {
            playerSound.PlayOneShot(gameOverClip);
            this.enabled = false;
        }
    }

    void UpdateTextBoxes()
    {
        ScoreTextBox.text = "Your Score: " + Score.ToString();
        TimerTextBox.text = "Your Time: " + Timer.ToString("F0");
        CompletionTextBox.text = "Level Complete? " + CompletionStatus.ToString();
    }

}



