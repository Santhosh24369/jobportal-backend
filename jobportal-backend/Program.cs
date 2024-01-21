
using jobportal_backend.Services;
using jobportal_backend;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JobSeekDatabaseSettings>(
    builder.Configuration.GetSection("JobSeekDatabase"));
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<JobServices>();
builder.Services.AddSingleton<Loginser>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();
app.UseLogUrl();
app.MapGet("/", () => "Hello World!");

app.MapPost("/login", async (Loginser users, Loginmodel User) =>
{

    var resl = await users.Login(User);
    if (resl is null || resl.password != User.password)
    {
        return null;

    }
    return resl;
});

app.MapPost("/Signup", async (Loginser usercre, Loginmodel newUser) =>
{
    await usercre.CreateAsync(newUser);
    return await usercre.GetAsync(newUser.Id);
});

app.MapGet("/GetJobs", async Task<List<JobModel>> (JobServices Job) => {
    return await Job.GetAsync();
});

app.MapGet("/Jobs/{id:length(24)}", async (JobServices Job, string id) =>
{
    return await Job.GetAsync(id);
});

app.MapPost("/Jobs", async (JobServices Job, JobModel newJob) =>
{
    await Job.CreateAsync(newJob);
    return await Job.GetAsync(newJob.Id);
}
);

app.MapPut("/Jobs{id:length(24)}", async (JobServices Job, JobModel updatedJob, string id) =>
{
    var res = await Job.GetAsync(id);
    if (res is null)
    {
        return Results.NotFound();
    }
    updatedJob.Id = res.Id;

    await Job.UpdateAsync(id, updatedJob);

    return Results.NoContent();
});

app.MapDelete("/jobs/{id:length(24)}", async (JobServices Job, string id) =>
{
    var res = await Job.GetAsync(id);
    if (res is null)
    {
        return Results.NotFound();
    }
    await Job.RemoveAsync(id);

    return Results.NoContent();
});
app.Run();
