using GameV1;
using MooseEngine.Core;

var app = new Application();
app.Create<TestGame>();
app.Run();
app.Dispose();