using ValidationTests.FindRelatedWords;

namespace ValidationTests.Tests;

internal class FindRelatedWordsTests
{
    private readonly RelatedWordsFinder _relatedWordsFinder;

    public FindRelatedWordsTests(RelatedWordsFinder relatedWordsFinder)
    {
        _relatedWordsFinder = relatedWordsFinder;
    }

    public async Task<List<TestResult>> TestWordAnswerPairs(List<WordAnswerPair> wordAnswerPairs)
    {
        var testResults = new List<TestResult>();

        foreach (var wordAnswerPair in wordAnswerPairs)
        {
            testResults.Add(await TestWordAnswerPair(wordAnswerPair));
        }

        return testResults;
    }

    private async Task<TestResult> TestWordAnswerPair(WordAnswerPair wordAnswerPair)
    {
        var foundWords = await _relatedWordsFinder.FindRelatedWords(wordAnswerPair.InputWord);

        var wordsFoundCorrect = new List<string>();
        var wordsFoundIncorrect = new List<string>();

        foreach (var foundWord in foundWords)
            if (wordAnswerPair.IdealAnswer.Contains(foundWord))
                wordsFoundCorrect.Add(foundWord);
            else
                wordsFoundIncorrect.Add(foundWord);

        var testResult = new TestResult();

        testResult.IsPass = IsTestPass(wordAnswerPair, wordsFoundCorrect, wordsFoundIncorrect);

        if (wordsFoundIncorrect.Any())
            testResult.Messages.Add($"Words incorrectly found: {String.Join(",", wordsFoundIncorrect)}");

        if (wordsFoundCorrect.Any())
            testResult.Messages.Add($"Words correctly found: {String.Join(",", wordsFoundCorrect)}");

        return testResult;

    }

    private static bool IsTestPass(WordAnswerPair wordAnswerPair, List<string> wordsFoundCorrect, List<string> wordsFoundIncorrect)
    {
        return !wordsFoundIncorrect.Any()
            && wordsFoundCorrect.Count.Equals(wordAnswerPair.IdealAnswer.Count);
    }
}
