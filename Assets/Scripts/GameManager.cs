using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public Questions[] questions;
    private static List<Questions> unanswerQuestion;
    private Questions currentQuestion;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private Text trueAnswerQuestion;
    [SerializeField]
    private Text falseAnswerQuestion;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private float timeBetweenQuestions = 3f;

    [SerializeField]
    private Slider slider;

    void Start()
    {
        if(unanswerQuestion == null || unanswerQuestion.Count == 0) 
        {
            unanswerQuestion = questions.ToList<Questions>();

        }

        slider.value = 1.0f;

        SetCurrentQuestion();

        //StartCoroutine(TransitionToNextQuestion());
    }

    void SetCurrentQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unanswerQuestion.Count);
        currentQuestion = unanswerQuestion[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            trueAnswerQuestion.text = "CORRECT";
            falseAnswerQuestion.text = "WRONG";
        } else
        {
            trueAnswerQuestion.text = "WRONG";
            falseAnswerQuestion.text = "CORRECT";
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        unanswerQuestion.Remove(currentQuestion);

        yield return new WaitForSeconds(timeBetweenQuestions);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        while (slider.value>0)
        {
            slider.value -=0.5f;
        }

        
    }

    IEnumerator UpdateSlider()
    {
        yield return new WaitForSeconds(0.1f);
    }
    public void UserSelectTrue()
    {
        animator.SetTrigger("True");

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse()
    {
        animator.SetTrigger("False");

        StartCoroutine(TransitionToNextQuestion());
    }

}
