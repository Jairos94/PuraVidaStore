using bkPuraVidaStoreMinimal.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
//builder.Services.AddDbContext<PuraVidaStoreContext>();

app.MapGet("/", () => "Hello World!");
app.Map("Usuario", () => 
{
    using (var contex= new PuraVidaStoreContext ()) 
    {
        return contex.Usuarios.ToList();
    }
}
);

app.Run();
