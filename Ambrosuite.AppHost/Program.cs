var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Ambrosuite_ApiService>("apiservice");

builder.AddProject<Projects.Ambrosuite_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
