using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class vaCham : MonoBehaviour
{
    [SerializeField] private Text locationText;
    [SerializeField] private Slider healthSlider; // Thêm Slider vào script
    private float health = 100f; // Giá trị máu ban đầu
    public AudioSource collectSound; // xu ly am thanh khi va cham
    public AudioSource gameOverAudio;
    public AudioSource runAudio;
    private bool isHitStone = true; // trang thai va cham
    public GameObject gameOverPanel;

    // Start is called before the first frame update

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Coin")
        {
            // play sound khi va cham coin
            collectSound.Play();

            hit.gameObject.GetComponent<Coin>().Dead(); // goi ham dead tu Coin

            // tang diem
            GetComponent<ScoreManager>().TangDiem(1);

        }
        else if (hit.gameObject.tag == "Stone" && isHitStone)
        {
            //tru diem
            StartCoroutine(EnableCollider(hit, 1));
            health -= 50f;
            healthSlider.value = health; // Cập nhật giá trị máu lên Slider
            if (health <= 0)
            {
                // Xử lý khi máu hết
                Die(); // Viết hàm Die để xử lý khi máu hết
            }
        }

        //update len Text canvas
        if (hit.gameObject.tag == "MushroomLocation")
        {
            locationText.text = "Mushroom: Location";
        }
        else if (hit.gameObject.tag == "StoneLocation")
        {
            locationText.text = "Stone: Location";
            //GetComponent<ScoreManager>().TangDiem(-1);
        }
        else if (hit.gameObject.tag == "HouseLocation")
        {
            locationText.text = "House: Location";
            //GetComponent<ScoreManager>().TangDiem(-1);
        }
    }
    private IEnumerator EnableCollider(ControllerColliderHit hit, float second)
    {
        isHitStone = false;
        yield return new WaitForSeconds(second); // sleep 1s
        isHitStone = true;
    }
    void Start()
    {
        healthSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Die()
    {
        // Xử lý khi người chơi chết (máu hết)
        // Ví dụ: Load lại level, hiển thị thông báo, v.v.
        gameOverAudio.Play();
        runAudio.Pause();
        gameOverPanel.SetActive(true);
    }
}
