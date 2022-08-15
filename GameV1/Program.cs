using GameV1;
using MooseEngine.Core;

var app = new Application();
app.Create<TestGameMSN>();
app.Run();
app.Dispose();