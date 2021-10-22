using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSysManager : MonoBehaviour
{

    GameObject inputFieldUserName, inputFieldPassword, buttonSubmit, toggleLogin, toggleCreateAccount, NetworkedClient, FindGameSessionButton, PlaceHolderGameButton, infoStuff1, infoStuff2;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go.name == "UserName")
                inputFieldUserName = go;
            else if (go.name == "Password")
                inputFieldPassword = go;
            else if (go.name == "SubmitButton")
                buttonSubmit = go;
            else if (go.name == "LogInToggle")
                toggleLogin = go;
            else if (go.name == "CreateAccountToggle")
                toggleCreateAccount = go;
            else if (go.name == "NetworkedClient")
                NetworkedClient = go;
            else if (go.name == "FindGameSessionButton")
                NetworkedClient = go;
            else if (go.name == "PlaceHolderGameButton")
                NetworkedClient = go;
            else if (go.name == "NameLabel")
                infoStuff1 = go;
            else if (go.name == "PasswordLabel")
                infoStuff2 = go;
        }
        buttonSubmit.GetComponent<Button>().onClick.AddListener(SubmitButtonPressed);
        toggleCreateAccount.GetComponent<Toggle>().onValueChanged.AddListener(ToggleCreateValueChanged);
        toggleLogin.GetComponent<Toggle>().onValueChanged.AddListener(ToggleLogInValueChanged);
        FindGameSessionButton.GetComponent<Button>().onClick.AddListener(findGameSessionButtonPressed);
        PlaceHolderGameButton.GetComponent<Button>().onClick.AddListener(placeHolderGameButtonPressed);

        changeGameState(GameStates.Login);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            changeGameState(GameStates.Login);
        }
    }

    private void SubmitButtonPressed()
    {
        Debug.Log("pressed");
        string n = inputFieldUserName.GetComponent<InputField>().text;
        string p = inputFieldPassword.GetComponent<InputField>().text;

        if (toggleLogin.GetComponent<Toggle>().isOn)
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.Login + "," + n + "," + p);
        else
            NetworkedClient.GetComponent<NetworkedClient>().SendMessageToHost(ClientToServerSignifiers.CreateAccount + "," + n + "," + p);
    }

    private void ToggleCreateValueChanged(bool newValue)
    {
        toggleLogin.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    private void ToggleLogInValueChanged(bool newValue)
    {
        toggleCreateAccount.GetComponent<Toggle>().SetIsOnWithoutNotify(!newValue);
    }

    public void findGameSessionButtonPressed()
    {

    }

    public void placeHolderGameButtonPressed()
    {

    }

    public void changeGameState(int newState)
    {
        // inputFieldUserName, inputFieldPassword, buttonSubmit, toggleLogin, toggleCreateAccount, NetworkedClient, FindGameSessionButton, PlaceHolderGameButton;
        inputFieldUserName.SetActive(false);
        inputFieldPassword.SetActive(false);
        buttonSubmit.SetActive(false);
        toggleLogin.SetActive(false);
        toggleCreateAccount.SetActive(false);
        FindGameSessionButton.SetActive(false);
        PlaceHolderGameButton.SetActive(false);
        infoStuff1.SetActive(false);
        infoStuff2.SetActive(false);
        if(newState == GameStates.Login)
        {
            inputFieldUserName.SetActive(true);
            inputFieldPassword.SetActive(true);
            buttonSubmit.SetActive(true);
            toggleLogin.SetActive(true);
            toggleCreateAccount.SetActive(true);
            infoStuff1.SetActive(true);
            infoStuff2.SetActive(true);
        } else if(newState == GameStates.MainMenu)
        {
            FindGameSessionButton.SetActive(true);

        }
        else if (newState == GameStates.WaitingForMatch)
        {

        }
        else if (newState == GameStates.PlayingTicTacToe)
        {
            PlaceHolderGameButton.SetActive(true);
        }
    }
}

public static class ClientToServerSignifiers
{
    public const int Login = 1;
    public const int CreateAccount = 2;
}

public static class ServerToClientSignifiers
{
    public const int LoginResponse = 1;
  //  public const int LoginFail = 2;
    //public const int CreateAccountSuccess = 3;
    //public const int CreateAccountFailure = 4;
}

public static class GameStates
{
    public const int Login = 1;
    public const int MainMenu = 2;
    public const int WaitingForMatch = 3;
    public const int PlayingTicTacToe = 4;
}