var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddHttpClient("ChatGptClientCompletions", opt =>
{
    opt.BaseAddress = new Uri("https://api.openai.com/v1/completions");
});

builder.Services.AddHttpClient("ChatGptClientImage", opt =>
{
    opt.BaseAddress = new Uri("https://api.openai.com/v1/images/generations");
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
