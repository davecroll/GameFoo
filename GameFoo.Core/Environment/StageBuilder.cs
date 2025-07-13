namespace GameFoo.Core.Environment;

public class StageBuilder
{
    private Stage _stage = new(0, 0);

    public StageBuilder AddDimensions(int width, int height)
    {
        _stage = new Stage(width, height);
        return this;
    }

    public StageBuilder AddTileSpan(string renderId, int x, int y, int length, int height)
    {
        _stage.AddTileSpan(renderId, x, y, length, height);
        return this;
    }

    public StageBuilder AddSpawnPosition(int x, int y)
    {
        _stage.SetSpawnPosition(x, y);
        return this;
    }

    public Stage Build() => _stage;
}
