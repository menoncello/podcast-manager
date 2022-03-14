using Microsoft.AspNetCore.Mvc;
using PodcastManager.Administration;
using PodcastManager.Api;
using PodcastManager.Api.Modules.Administration;
using PodcastManager.Api.Modules.ItunesCrawler;
using PodcastManager.CrossCutting.Rabbit;
using PodcastManager.ItunesCrawler.Adapters;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .Configure()
    .Build();

app.SetUp();

app.Urls.Add("http://192.168.5.164:5000/");
app.Run();