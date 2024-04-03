using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public struct VerificationRequest 
    {
        public string VerificationToken;
        public LoginErrors? error;

        public VerificationRequest(string token, LoginErrors? _error) 
        {
            VerificationToken = token;
            error = _error;
        }
    }

    public struct LoginReponse 
    {
        public bool wasSuccess;
        public LoginErrors? error;

        public LoginReponse(bool _success, LoginErrors? _error) 
        {
            wasSuccess = _success;            
            error = _error;
        }
    }
    public enum LoginErrors{Invalid_Credentials, No_Response, Unknown_Error}   

 

    private string baseUri = "https://localhost";
    private string Port = "7093";
    public string Username  { get; set; }
    public string Password { private get; set; }
    public string UriString { get { return baseUri + ":" + Port; } }
    public bool IsLoggedIn { get; set; }

    private HttpClientHandler httpClientHandler;
    public HttpClient httpClient { get; set; }

    
    void Start()
    {
        httpClientHandler = new HttpClientHandler();
        httpClient = new HttpClient(httpClientHandler);
    }

 
    void Update()
    {

    }


    //Gets a verification token from login page and then attempts to login with username and password from networkManager
    public async Task<LoginReponse> Login()
    {
        VerificationRequest request = await GetVerificationToken();
        if (request.error != null) 
        {
            return new LoginReponse(false, request.error);
        }
        string verificationToken = request.VerificationToken;      

        FormUrlEncodedContent LoginData = GetLoginContent(verificationToken, Username, Password);

        // Send POST request with form data
        HttpResponseMessage response = await httpClient.PostAsync($"{UriString}/Identity/Account/Login", LoginData);
        if (response == null) 
        {
            return new LoginReponse(false, request.error);
        }
        string content = await response.Content.ReadAsStringAsync();
        if (content.Contains("Hello " + Username, StringComparison.OrdinalIgnoreCase))         
        {
            IsLoggedIn = true;
            return new LoginReponse(true, null); ;
        }
        else
        {
            return new LoginReponse(false, LoginErrors.Unknown_Error);
        }
    }
    //Gets a verification token from login page and then attempts to login with username and password specified
    public async Task<LoginReponse> Login(string _username, string _password)
    {
        Username = _username;
        VerificationRequest request = await GetVerificationToken();
        if (request.error != null)
        {
            return new LoginReponse(false, request.error);
        }
        string verificationToken = request.VerificationToken;

        FormUrlEncodedContent LoginData = GetLoginContent(verificationToken, Username, _password);
        //Debug.Log(await LoginData.ReadAsStringAsync());
        // Send POST request with form data
        HttpResponseMessage response = await httpClient.PostAsync($"{UriString}/Identity/Account/Login", LoginData);
        if (response == null)
        {
            return new LoginReponse(false, request.error);
        }
        string content = await response.Content.ReadAsStringAsync();
        //Debug.Log(content);
        if (content.Contains("Hello " + _username, StringComparison.OrdinalIgnoreCase))
        {
            IsLoggedIn = true;
            return new LoginReponse(true, null); ;
        }
        else
        {
            return new LoginReponse(false, LoginErrors.Unknown_Error);
        }
    }


    private FormUrlEncodedContent GetLoginContent(string verificationToken, string username, string password)
    {
        return new FormUrlEncodedContent(new[]
        {
                new KeyValuePair<string, string>("Input.Email", username),
                new KeyValuePair<string, string>("Input.Password", password),
                new KeyValuePair<string, string>("Input.RememberMe", "true"),
                new KeyValuePair<string, string>("__RequestVerificationToken", verificationToken)
            });
    }

    private async Task<VerificationRequest> GetVerificationToken()
    {
        // Send GET request to retrieve the login page

        HttpResponseMessage loginPageResponse;
        try
        {
            loginPageResponse = await httpClient.GetAsync($"{UriString}/Identity/Account/Login");
            loginPageResponse.EnsureSuccessStatusCode(); // Throw an exception if the response is not successful
        }
        catch
        {
            return new VerificationRequest(null, LoginErrors.No_Response);
        }

        string loginPageContent = await loginPageResponse.Content.ReadAsStringAsync();

        // Extract the verification token from the login page content
        string verificationToken = ExtractVerificationToken(loginPageContent);
        return new VerificationRequest(verificationToken, null);
    }
    private string ExtractVerificationToken(string htmlContent)
    {
        // Define the regular expression pattern to match the verification token
        string pattern = "<input[^>]+name=\"__RequestVerificationToken\"[^>]+value=\"([^\"]+)\"[^>]*>";
        Match match = Regex.Match(htmlContent, pattern);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }
        else
        {
            Console.WriteLine("Verification token not found.");
            return "";
        }
    }

    public async Task<bool> PostHighScore(string mapName, float fastestLap, int bestComboScore, float bestComboTime)
    {
        // Create the form data
        var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("MapName", mapName),
                new KeyValuePair<string, string>("FastestLap", fastestLap.ToString()),
                new KeyValuePair<string, string>("BestComboScore", bestComboScore.ToString()),
                new KeyValuePair<string, string>("BestComboTime", bestComboTime.ToString())
            };

        // Create FormUrlEncodedContent
        var content = new FormUrlEncodedContent(formData);

        // Send POST request to the UpdateHighScore endpoint
        HttpResponseMessage response = await httpClient.PostAsync($"{UriString}/Highscores/UpdateHighScore", content);

        Console.WriteLine("Status Code: " + response.StatusCode.ToString());

        // Check if the response is successful
        return response.IsSuccessStatusCode;
    }

    public async Task<List<Highscore>> GetHighscores(string MapName)
    {
        List<Highscore> highscores = new List<Highscore>();

        // Send POST request to the UpdateHighScore endpoint
        HttpResponseMessage response = await httpClient.GetAsync($"{UriString}/Highscores/GetHighscores?MapName=" + MapName);

        string json = await response.Content.ReadAsStringAsync();

        highscores = HighScores.ParseHighscores(json);

        return highscores;
    }


}


public class HighscoreUpdateModel
{
    public string MapName { get; set; }
    public float FastestLap { get; set; }
    public int BestComboScore { get; set; }
    public float BestComboTime { get; set; }
}

public class Highscore
{
    public string Name { get; set; }
    public string Map { get; set; }
    public string Fastest_Lap { get; set; }
    public int Best_Combo_Score { get; set; }
    public string Best_Combo_Time { get; set; }

    public override string ToString()
    {
        return "Name:" + Name + ", Map:" + Map + ", Fastest Lap:" + Fastest_Lap + ", Best Combo Score:" + Best_Combo_Score + ", Best Combo Time:" + Best_Combo_Time;
    }

    public TimeSpan BestComboTimeSpan()
    {
        return TimeSpan.ParseExact(Best_Combo_Time, @"hh\:mm\:ss\:fff", CultureInfo.InvariantCulture);
    }
    public TimeSpan FastestLapTimeSpan()
    {
        return TimeSpan.ParseExact(Fastest_Lap, @"hh\:mm\:ss\:fff", CultureInfo.InvariantCulture);
    }

}

public class HighScores
{
    enum Sort_Order { DESC, ASC }
    enum Sort_By { NAME, FASTEST_LAP }

    List<Highscore> highscores;



    public static List<Highscore> ParseHighscores(string text)
    {
        List<Highscore> highscores;

        try
        {
            // Deserialize the JSON string into a list of Highscore objects
            highscores = JsonUtility.FromJson<List<Highscore>>(text);
        }
        catch (Exception ex)
        {
            // Handle any errors that may occur during deserialization
            Console.WriteLine("Error parsing highscores: " + ex.Message);
            highscores = new List<Highscore>(); // Return an empty list if parsing fails
        }

        return highscores;
    }

}


