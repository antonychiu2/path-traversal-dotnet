using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", (HttpContext ctx) => {
    String path = ctx.Request.Query["path"];

    return File.ReadAllText(path);
});


app.MapGet("/infolder", (HttpContext ctx) => {
    String path = "data/" + ctx.Request.Query["filename"] + ".json";

    return File.ReadAllText(path);
});

app.MapGet("intermediateVariable/infolder", (HttpContext ctx) => {
    String filename = ctx.Request.Query["filename"];
    String path = "data/" + filename  + ".json";

    return File.ReadAllText(path);
});

app.MapGet("/infolder/delete", (HttpContext ctx) => {
    File.Delete("C:\\users\\reports\\" + ctx.Request.Query["filename"]);
    return "done!";
});

app.MapGet("intermediateVariable/infolder/delete", (HttpContext ctx) => {
    String filename = ctx.Request.Query["filename"];
    File.Delete("C:\\users\\reports\\" + filename);
    return "done!";
});

app.MapGet("intermediateVariable/infolder/delete/withEscaping", (HttpContext ctx) => {
    String filename = ctx.Request.Query["filename"];
    File.Delete("C:\\users\\reports\\" + Regex.Replace(Convert.ToString(filename), "([/\\\\:*?\"<>|])|(^\\s)|([.\\s]$)", "_"));
    return "done!";
});

app.Run();
