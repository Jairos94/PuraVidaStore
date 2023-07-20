using BackupPuraVidaStore;

IHost host = Host.CreateDefaultBuilder(args)
     .ConfigureAppConfiguration((hostContext, config) =>
     {
         // Obtiene la ruta actual del directorio de trabajo
         string currentDirectory = Directory.GetCurrentDirectory()+ "\\appsettings.json";

         // Agregar el archivo appsettings.json desde la ruta completa
         config.AddJsonFile(currentDirectory, optional: false, reloadOnChange: true);
     })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.Configure<BackupSettings>(hostContext.Configuration.GetSection("Configuraciones"));
    })
    .Build();

await host.RunAsync();
