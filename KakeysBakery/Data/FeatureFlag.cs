namespace KakeysBakery.Data;

public static class FeatureFlagService
{
    public static bool IsAvailable { get; set; }

    public static void SetVariable(bool value)
    {
        IsAvailable = value;
    }
}
