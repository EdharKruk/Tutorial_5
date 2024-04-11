using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var animals = new List<Animal>();
var visits = new List<Visit>();

app.MapGet("/animals", () => animals);

app.MapGet("/animals/{id}", (int id) => animals.FirstOrDefault(a => a.Id == id));

app.MapPost("/animals", (Animal animal) =>
{
    animals.Add(animal);
    return Results.Created($"/animals/{animal.Id}", animal);
});

app.MapPut("/animals/{id}", (int id, Animal updatedAnimal) => {
    var animal = animals.FirstOrDefault(a => a.Id == id);
    if (animal == null) return Results.NotFound();
    animal.Name = updatedAnimal.Name;
    animal.Category = updatedAnimal.Category;
    animal.Weight = updatedAnimal.Weight;
    animal.FurColor = updatedAnimal.FurColor;
    return Results.NoContent();
});

app.MapDelete("/animals/{id}", (int id) => {
    var animal = animals.FirstOrDefault(a => a.Id == id);
    if (animal == null) return Results.NotFound();
    animals.Remove(animal);
    return Results.NoContent();
});

app.MapControllers();

app.Run();

