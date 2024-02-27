namespace KakeysBakery.Tests;

public class BakeryTests : IClassFixture<BakeryFactory>
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(1, 1); 
    }
}