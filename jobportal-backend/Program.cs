
using jobportal_backend.Services;
using jobportal_backend;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<JobSeekDatabaseSettings>(
    builder.Configuration.GetSection("JobSeekDatabase"));
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<JobServices>();
builder.Services.AddSingleton<Loginser>();
builder.Services.AddSingleton<GetJobsSer>();
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
app.MapGet("/", () => "Hello World!");

app.MapPost("/login", async (Loginser users, Loginmodel User) =>
{

    var resl = await users.Login(User);
    if (resl is null)
    {
        return null;
    }
    bool isValid = BCrypt.Net.BCrypt.Verify(User.password, resl.password);
    if (isValid)
    {
        return resl;
    }
      return null;
});

app.MapPost("/Signup", async (Loginser usercre, Loginmodel newUser) =>
{
    newUser.password = BCrypt.Net.BCrypt.HashPassword(newUser.password);
    await usercre.CreateAsync(newUser);
    return await usercre.GetAsync(newUser.Id);
});
app.MapGet("/JoinJobs",async Task<List<GetJobs>> (GetJobsSer test) => {
    return await test.GetJobs();    
});

app.MapGet("/JoinJob/{id:length(24)}", async  (GetJobsSer testid,ObjectId id) => {
    return await testid.GetJob(id);
});

app.MapGet("/GetJobs", async Task<List<JobModel>> (JobServices Job) => {
    return await Job.GetAsync();
});

app.MapGet("/Organization/{id:length(24)}", async (JobServices Job, string id) =>
{
    return await Job.GetorgAsync(id);
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
