using PodcastManager.Api;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .Configure()
    .Build();

app.SetUp();

app.Urls.Add("http://192.168.5.164:5000/");
app.Run();