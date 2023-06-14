using ValidationTests.FindRelatedWords;
using ValidationTests.Tests;

var apiKey = ""; //insert API key here

if (string.IsNullOrWhiteSpace(apiKey))
    apiKey = GetNonEmptySingleWordInput("No API key found. Insert your API key here:");

var api = new OpenAI_API.OpenAIAPI(apiKey);

var relatedWordsFinder = new RelatedWordsFinder(api);

// -------- User input ----------
await FindBasedOnUserInput(relatedWordsFinder);

// ------- Tests ---------
//await RunTests(relatedWordsFinder);

static async Task FindBasedOnUserInput(RelatedWordsFinder relatedWordsFinder)
{
    var userInput = GetNonEmptySingleWordInput("Please write a single word as input:");

    var listOfWords = await relatedWordsFinder.FindRelatedWords(userInput);

    Console.WriteLine("Related words found:");

    foreach (var word in listOfWords)
        Console.WriteLine(word);
}

static string GetNonEmptySingleWordInput(string input)
{
    string? userInput;

    do
    {
        Console.WriteLine(input);

        userInput = Console.ReadLine();

    } while (string.IsNullOrWhiteSpace(userInput));

    return userInput.Trim().Split(" ").First();
}

static async Task RunTests(RelatedWordsFinder relatedWordsFinder)
{
    var tests = new List<WordAnswerPair> {
         new WordAnswerPair
         {
             InputWord = "hot",
             IdealAnswer = {"Coffee"}
         },
        new WordAnswerPair
         {
             InputWord = "animal",
             IdealAnswer = {"duck"}
         }
    };

    var findRelatedWordsTester = new FindRelatedWordsTests(relatedWordsFinder);

    var testResults = await findRelatedWordsTester.TestWordAnswerPairs(tests);

    foreach (var testResult in testResults)
        Console.WriteLine(testResult.ToString());
}
