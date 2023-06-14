var apiKey = "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"; //insert API key here

if(string.IsNullOrWhiteSpace(apiKey))
    apiKey = GetNonEmptyInput("No API key found. Insert your API key here:");

var api = new OpenAI_API.OpenAIAPI(apiKey);

var userInput = GetNonEmptyInput("Please write your input to the AI model:");

try
{
    var result = await api.Completions.GetCompletion(userInput);

    Console.WriteLine(result);
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