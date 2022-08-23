using MooseEngine.Core.Inputs;
using MooseEngine.Extensions.Runtime;

namespace MooseEngine.Core;

public static class Input 
{
    private static IInputAPI? InputAPI { get; set; }

    public static void Initialize(IInputAPI inputAPI)
    {
        InputAPI = inputAPI;
        Throw.IfNull(InputAPI, nameof(Input));
    }

    public static bool IsKeyPressed(Keycode keycode)
    {
        return InputAPI!.IsKeyPressed(keycode);
    }
}
