namespace SharpBoxes.WPFHelpers.Progress;

public class CustomProgessBody(double progress, string? message) : IProgessBody
{
    public double Progress { get; set; } = progress;
    public string? Message { get; set; } = message;
}
