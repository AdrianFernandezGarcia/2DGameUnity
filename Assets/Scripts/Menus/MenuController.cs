using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Button previousButton;
    [SerializeField]
    private float scaleAmount ;
    [SerializeField]
    private GameObject defaultButton;
    private Button selectedButton;
    private Vector3 defaultButtonScale;
    void Start()
    {

        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton);
                      
        }

        defaultButtonScale = defaultButton.transform.localScale;
    }

    private void Update()
    {
        OnGUI();
       
        MenuNavigation();
    }

    void OnDisable()
    {
        if (previousButton != null) UnHighlightButton(previousButton);
    }

    private void OnEnable()
    {

        EventSystem.current.SetSelectedGameObject(defaultButton);
    }

    void HighlightButton(Button button)
    {
        button.transform.localScale = new Vector3(scaleAmount, scaleAmount, scaleAmount);
        button.image.color = button.colors.selectedColor;
        
    }

    void UnHighlightButton(Button button)
    {
       
        button.transform.localScale = defaultButtonScale;
        button.image.color = button.colors.normalColor;
    }

    
    public void MenuNavigation()
    {

        var selectedObj = EventSystem.current.currentSelectedGameObject;

        if (selectedObj == null) return;
        selectedButton = selectedObj.GetComponent<Button>();
        if (selectedButton != null && selectedButton != previousButton)
        {

            HighlightButton(selectedButton);
        }

        if (previousButton != null && previousButton != selectedButton)
        {
            UnHighlightButton(previousButton);
        }

        previousButton = selectedButton;

    }

	private void OnGUI()
	{
        
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1) || Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Mouse3) || Input.GetKeyDown(KeyCode.Mouse4) || Input.GetKeyDown(KeyCode.Mouse5) || Input.GetKeyDown(KeyCode.Mouse6)){

            return;
        }
        
    }

	//GETTERS Y SETTERS

	public  void SetDefaultButton(Button button)
    {
        this.selectedButton = button;
    }


}