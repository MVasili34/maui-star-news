namespace NewsMobileApp.TempServices;

public class AnimationCreator
{
    /// <summary>
    /// Method for adding skeleton loading effect to Frames
    /// </summary>
    /// <param name="frames">Collection of Frames</param>
    /// <returns><see cref="Animation"/> instance</returns>
    public static Animation SetAnimations(params Frame[] frames)
    {
        var parentAnimation = new Animation();
        foreach (var frame in frames)
        {
            parentAnimation.Add(0, 0.5, new Animation(v => frame.Opacity = v, 1, 0.3, Easing.CubicIn));
            parentAnimation.Add(0.5, 1, new Animation(v => frame.Opacity = v, 0.3, 1, Easing.CubicIn));
        }
        return parentAnimation;
    }
}
