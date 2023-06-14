// https://github.com/PawanOsman/ChatGPT.Net/

using ChatGPT.Net;

var apiKey = ""; //insert API key here

if (string.IsNullOrWhiteSpace(apiKey))
    apiKey = GetNonEmptyInput("No API key found. Insert your API key here:");

var chatGpt = new ChatGpt(apiKey);

try
{
    var response = await chatGpt.Ask("What's the best way to enjoy a warm summers day?");

    Console.WriteLine(response);
}
catch (Exception e)
{
    Console.WriteLine("Exception encountered when calling OpenAI services. Please check API Key validity.");
    Console.WriteLine(e);
}

static string GetNonEmptyInput(string input)
{
    string? userInput;

    do
    {
        Console.WriteLine(input);
        userInput = Console.ReadLine();

    } while (string.IsNullOrWhiteSpace(userInput));

    return userInput;
}