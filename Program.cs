using ChronoDB.Cli;
using ChronoDB.Storage;

var store = new JsonMeasurementStore();
var repl = new ChronoDbRepl(store);
repl.Run();
