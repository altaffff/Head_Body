    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public enum BodyType
    {
        Combined,
        Head
    }

    public class PlayerBodyController : MonoBehaviour
    {
        public BodyType bodyType;
        public static PlayerBodyController instance;
        
        public float moveSpeed = 5f;
        public float jumpForce = 10f;
        public LayerMask groundLayer;
        public Transform groundCheck;
        private float groundCheckRadius = 0.2f;
        private bool isGrounded;
        private Rigidbody2D rb;
        private float moveInput;
        public bool isActive;
        public GameObject DummyHead;
        public GameObject headTrigger;

        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            if (SceneLoader.instance.key != null && SceneLoader.instance.door != null)
            {
                SceneLoader.instance.key.SetActive(true);
                SceneLoader.instance.door.SetActive(false);
            }
            rb = GetComponent<Rigidbody2D>();

           
        }

        void Update()
        {
            if (bodyType == BodyType.Combined && Input.GetKeyDown(KeyCode.W) && !headTrigger.activeInHierarchy)
            {
                isActive = false;
                GamePlayManager.instance.headControler.transform.position = DummyHead.transform.position;
                DummyHead.SetActive(false);
                GamePlayManager.instance.headControler.gameObject.SetActive(true);
                GamePlayManager.instance.headControler.HeadThrust();
                Invoke(nameof(ActivateHeadTrigger), .4f);
            }
            if (!isActive) return;

            CheckGround();
            HandleMovement();

            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        public void HeadThrust()
        {
            rb.AddForce(new Vector2(1, 8), ForceMode2D.Impulse);
        }

        void ActivateHeadTrigger()
        {
            headTrigger.SetActive(true);
        }

        void HandleMovement()
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        void Jump()
        {
            if (isGrounded)
            {
               rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
        }

        void CheckGround()
        {
           
                isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

                Collider2D collider = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,
                    LayerMask.GetMask("MovingPlatform"));

                if (collider != null)
                {
                    GroundedOnMovingPlatforms(collider.transform);
                }
              
            
        }

        void GroundedOnMovingPlatforms(Transform ground)
        {
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (bodyType == BodyType.Head)
            {
                if (other.gameObject.CompareTag("Head"))
                {
                    PlayerBodyController wholeBody = GamePlayManager.instance.wholeBodyController;
                    wholeBody.headTrigger.SetActive(false);
                    wholeBody.DummyHead.SetActive(true);
                    wholeBody.isActive = true;
                    gameObject.SetActive(false);
                }
            }

            if (other.gameObject.CompareTag("Key"))
            {
                if (bodyType == BodyType.Combined || bodyType == BodyType.Head)
                {
                    Destroy(other.gameObject);
                    LevelData.instance.KeyObtained(SceneLoader.instance.key,SceneLoader.instance.door);
                }
            }
            //This is only used if special key is used to manipulate the obstracle on and off 
            if (other.gameObject.CompareTag("SpecialKey"))
            {
                LevelData.instance.Level2ObstracleManuplation(LevelData.instance.specialKeyObstracle,LevelData.instance.specialKey);
                LevelData.instance.Level2ButtonCover(LevelData.instance.buttonCover);
            }

            if (other.gameObject.CompareTag("Button"))
            {
                if (bodyType == BodyType.Combined)
                {
                    other.gameObject.transform.Translate(transform.position.x, -5.8f, transform.position.z);
                    LevelData.instance.Level2ButtonPressed(LevelData.instance.buttonObstracle1,LevelData.instance.buttonObstracle2,LevelData.instance.buttonObstracle3,LevelData.instance.buttonObstracle4);
                }
            }

            if (other.gameObject.CompareTag("Door"))
            {
                
                Debug.Log("Collided with Door");
                if (bodyType == BodyType.Combined)
                {
                    Debug.Log("Player is Combined. Loading next scene.");
                    Debug.Log($"Colliding with {other.gameObject.name}");
                    SceneLoader.instance.NextLevel();
                }
                else
                {
                    Debug.Log("Player is not combined");
                }
            }

            if (other.gameObject.CompareTag("Spikes"))
            {
                if (bodyType == BodyType.Combined || bodyType == BodyType.Head)
                {
                    Destroy(gameObject);
                    SceneLoader.instance.ShowGameOverScreen();
                }
            }

            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(other.gameObject.transform);
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }