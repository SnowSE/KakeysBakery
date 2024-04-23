namespace KakeysBakery.Data;

public static class FeatureFlag
{
    public static bool IsAvailable = false;

    public static void SetVariable(bool value)
    {
        IsAvailable = value;
    }
}
