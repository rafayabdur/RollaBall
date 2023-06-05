using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public FixedJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    public GameObject Player;
    public GameObject ground;
    public float Speed = 0f;
    private Rigidbody rb;
    public Text count_text;
    public Text Score_text;
    private int Score;
    /*private float MovementX;
    private float MovementY;*/
    private int limit = 6;
    public Text Score_Board;
    //public bool gameover = false;
    public bool IsAllowedToTrigger = true;
    public ParticleSystem Effect;
    public Color originalColor;
  
    public GameObject GameOverScreen;

    public MeshRenderer[] environmentMaterials;
    public Color[] environmentColors;
    void Start()
    {
        GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();


        for (int i = 0; i < environmentMaterials.Length; i++)
        {
            environmentColors[i] = environmentMaterials[i].sharedMaterial.color;
        }

        //originalColor = GetComponent<MeshRenderer>().sharedMaterial.color;
        //count = 0;
        Score = 0;
        SetCountText();
    }
    public void loadscene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MiniGame");

    }
    
    //void OnMove(InputValue movementValue)
    //{

    //    if (gameover == false)
    //    {

    //        //Vector2 movementVector = movementValue.Get<Vector2>();

    //        /*MovementX = movementVector.x;
    //        MovementY = movementVector.y;*/
    //    }
    //}

    public void SetCountText()
    {

        Score_text.text = "Score : " + Score.ToString();
        //count_text.text = "SCORE: " + count.ToString();
        
        //Debug.Log("we are at place1");
        if (Score  == limit)
        {
            Time.timeScale = 0;
            //gameover = true;
            GetComponent<Rigidbody>().isKinematic = true;
            //GameOverScreen.SetActive(true);
            //UIManager.Instance.ShowGameOverScreen();
            UIManager.Instance.GameOverScreenStatus(true);
            Debug.Log("REACHED");
            //Speed = 0.0f;
            Score_text.text = "Score : " + Score.ToString();
            Score_Board.text = "Score : " + Score.ToString();
            Debug.Log("after");
            GetComponent<MeshRenderer>().sharedMaterial.color = originalColor;
            Score = 0;
            UIManager.Instance.GamePlayScreenStatus(false);
            //IsAllowedToTrigger = false;
            //Debug.Log("we are at place 2");
        }

    }
    void FixedUpdate()
    {
        //Vector3 movement = new Vector3(MovementX, 0.0f, MovementY);

        //rb.AddForce(movement * Speed);
        Joystick_Move();
    }

  
    private void OnTriggerEnter(Collider other)
    {
            if (other.gameObject.CompareTag("Pick up"))
            {
            //Cube.SetActive(true);
            ground.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
                GetComponent<MeshRenderer>().sharedMaterial.color = Random.ColorHSV();
                other.gameObject.SetActive(false);
                //count = count + 1;
                Score += 1;
                
           
                //Debug.Log("we are at place 0");
                Instantiate(Effect, this.transform.position, this.transform.rotation);
                
                SetCountText();


                Debug.Log("hiii");
                //Effect.Play();

            }
        
       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("wall"))
        {

            collision.gameObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            GetComponent<MeshRenderer>().sharedMaterial.color = Random.ColorHSV();

        }


    }
    public void Joystick_Move()
    {
        
        Vector3 _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        _moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;
        //_moveVector.y = _joystick.Horizontal * _moveSpeed * Time.deltaTime;

        if (_joystick.Horizontal != 0 && _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0.0f);

            transform.rotation = Quaternion.LookRotation(direction);
        }
        rb.MovePosition(rb.position + _moveVector);
    }

    //public void RestartButton()
    //{
    //    UIManager.Instance.GamePlayScreenStatus(true);
    //    UIManager.Instance.GameOverScreenStatus(false);


    //}


    public void ResetEnvironmentMaterialsColor()
    {
        //revert color of envcironment to original.

        for (int i = 0; i < environmentMaterials.Length; i++)
        {
            environmentMaterials[i].sharedMaterial.color = environmentColors[i];
        }
        


    }
}
