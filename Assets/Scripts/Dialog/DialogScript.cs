using System.Collections;
using TMPro;
using UnityEngine;

public class DialogTutorial : MonoBehaviour
{
    
    [SerializeField]
    private GameObject _dialog;
    [SerializeField]
    private GameObject _continueButton;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private string[] _sentences;
    private int _index;
    private bool isTriggered;
    [SerializeField]
    private float _wordDelay = 0.1f;
    
    private bool _aPressed;
    private bool _dPressed;
    private bool _wPressed;
    private bool _sPressed;
    private bool _isAllMovementPressed;
    private bool _isFirePressed;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
    }
    
    IEnumerator Type()
    {
        foreach (var letter in _sentences[_index].ToCharArray())
        {
            _text.text += letter;
            yield return new WaitForSeconds(_wordDelay);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_text.text == _sentences[_index])
        {
            _continueButton.SetActive(true);
        }

        if (_text.text == _sentences[1] || _text.text == _sentences[2])
        {
            _continueButton.SetActive(false);
        }
        
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                _aPressed = true;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                _dPressed = true;
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _wPressed = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                _sPressed = true;
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse0) && _text.text == _sentences[2])
            {
                _isFirePressed = true;
                NextSentence();
            }
            
            if (_aPressed && _dPressed && _wPressed && _sPressed && !_isAllMovementPressed)
            {
                _isAllMovementPressed = true;
                NextSentence();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTriggered)
        {
            _dialog.SetActive(true);
            isTriggered = true;
            StartCoroutine(Type());
        }
    }
    
    public void NextSentence()
    {
        _continueButton.SetActive(false);
        if (_index < _sentences.Length - 1)
        {
            _index++;
            _text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            _text.text = "";
            _dialog.SetActive(false);
        }
    }
}
