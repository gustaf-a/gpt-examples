namespace ValidationTests.Tests;

internal class TestResult
{
    public bool IsPass { get; set; } = true;
    public List<string> Messages { get; set; } = new();

    public override string ToString()
    {
        return $"Test {(IsPass ? "passed" : "failed")}: {String.Join((Environment.NewLine), Messages)}";
    }
}
