using Microsoft.Extensions.Options;
using Serilog;
using System.Data.SqlClient;

namespace BackupPuraVidaStore
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<BackupSettings> _backupSettings;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IOptions<BackupSettings> backupSettings,IConfiguration configuration =null)
        {
            _logger = logger;
            _backupSettings = backupSettings;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Logger = new LoggerConfiguration()
              .WriteTo.Console()
              .WriteTo.File("C:\\_LogsPuraVidaStore\\ServerWorkerLog-.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();
            Log.Information("Backup worker started.");
            Log.Information(_configuration.GetConnectionString("BaseDatos"));
            Log.Information($"Connection string: {_configuration.GetConnectionString("BaseDatos")}");
            Log.Information(Directory.GetCurrentDirectory() + "\\appsettings.json");

            try
            {
                await ScheduleBackup(stoppingToken);
            }
            catch (Exception ex)
            {

                Log.Error(ex, "An error occurred during the backup process.");

            }

        }
        private async Task ScheduleBackup(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextBackupTime = new DateTime(now.Year, now.Month, now.Day, _backupSettings.Value.HoraBackup.Hours, _backupSettings.Value.HoraBackup.Minutes, _backupSettings.Value.HoraBackup.Seconds);

                if (now > nextBackupTime)
                {
                    nextBackupTime = nextBackupTime.AddDays(1);
                }

                var timeUntilBackup = nextBackupTime - now;

                await Task.Delay(timeUntilBackup, stoppingToken);

                if (!stoppingToken.IsCancellationRequested)
                {
                    await BackupDatabase();
                }
            }
        }
        private async Task BackupDatabase()
        {
            try
            {
                var backupFolderPath = _configuration["Configuraciones:RutaBackup"];
                var backupFileName = _configuration["Configuraciones:NombreDelArchivo"];
                var connectionString = _configuration.GetConnectionString("BaseDatos"); // Obtén la cadena de conexión desde el appsettings.json o de una fuente segura.
                var nombreBaseDatos = _configuration["NombreBaseDatos"];

                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    var command = connection.CreateCommand();
                    command.CommandText = $"BACKUP DATABASE PuraVidaStore TO DISK = '{Path.Combine(backupFolderPath, backupFileName)}'";
                    await command.ExecuteNonQueryAsync();
                }

                Log.Information($"Backup successful. Saved to: {Path.Combine(backupFolderPath, backupFileName)}");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred during the backup process.");
            }
        }
    }

}