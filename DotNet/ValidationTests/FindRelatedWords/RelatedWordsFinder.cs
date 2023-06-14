using Newtonsoft.Json;
using OpenAI_API;
using System.Text;

namespace ValidationTests.FindRelatedWords;

public class RelatedWordsFinder
{
    private readonly OpenAIAPI _api;

    public RelatedWordsFinder(OpenAIAPI api)
    {
        _api = api;
    }

    public async Task<List<string>> FindRelatedWords(string userInput)
    {
        var userInputDelimiter = "####";
        var wordsDelimiter = "```";

        var possibleWords = """
Coffee
Bicycle
Duck
Sunglasses
Backpack
Tennis
Umbrella
Guitar
Clock
Plant
""";

        var systemMessage = $"""
You will be given a word delimited by {userInputDelimiter} characters.
Your task is to find all related words from a list of possible words.
Output a JSON array like this:
    ["<relatedword>","<relatedword>" ..]
OR
    []

The related words must be found in the list of possible words below.
If no related words can be found, output an empty JSON array.
Do not output anything except JSON.

Possible words:
{wordsDelimiter}
{possibleWords}
{wordsDelimiter}
""";

        var one_shot_user = "hot";
        var one_shot_assistant = "[\"Coffee\"]";

        string prompt = CreatePrompt(systemMessage, one_shot_user, one_shot_assistant, userInput);

        try
        {
            var result = await _api.Completions.GetCompletion(prompt);

            var listOfWords = GetStringsFromJsonList(result);

            return listOfWords;
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception encountered when calling OpenAI services. Please check API Key validity.");
            Console.WriteLine(e);
        }

        return null;

    }

    static string CreatePrompt(string systemMessage, string one_shot_user, string one_shot_assistant, string userInput)
    {
        var sb = new StringBuilder();

        sb.AppendLine(systemMessage);

        sb.AppendLine($"Input: {one_shot_user}");
        sb.AppendLine($"Output: {one_shot_assistant}");

        sb.AppendLine();

        sb.AppendLine($"Input: {userInput}");
        sb.AppendLine($"Output: ");

        return sb.ToString();
    }

    static List<string> GetStringsFromJsonList(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            Console.WriteLine("Unable to convert empty string to List.");
            return null;
        }

        try
        {
            var listOfStrings = JsonConvert.DeserializeObject<List<string>>(json);

            return listOfStrings;
        }
        catch (Exception)
        {
            Console.WriteLine("Invalid JSON. Unable to convert to List.");
            return null;
        }
    }
}
