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

app.Run();