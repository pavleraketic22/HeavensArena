using UnityEngine;

public class Welcome : MonoBehaviour
{
    public GameObject tutorialCanvas;  
    public GameObject panelExplanation; 
    public GameObject panelControls;   
    private bool tutorialActive = false;

    void Start()
    {
        tutorialCanvas.SetActive(false);
        panelExplanation.SetActive(true);
        panelControls.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player") && !tutorialActive)
        {
            tutorialActive = true;
            tutorialCanvas.SetActive(true);
            Time.timeScale = 0f; 
        }
    }
    
    public void ShowControls()
    {
        panelExplanation.SetActive(false);
        panelControls.SetActive(true);
    }
    
    public void CloseTutorial()
    {
        tutorialCanvas.SetActive(false);
        Time.timeScale = 1f; 
        Destroy(gameObject); 
    }
}