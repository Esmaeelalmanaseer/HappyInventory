using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HappyInventory.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class LogsController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public LogsController(IWebHostEnvironment env)
    {
        _env = env;
    }

    [HttpGet]
    public IActionResult GetLogs()
    {
        var logsFolder = Path.Combine(_env.ContentRootPath, "Logs");

        if (!Directory.Exists(logsFolder))
        {
            Directory.CreateDirectory(logsFolder);
            return Ok("Logs folder created. No logs available yet.");
        }

        var logFiles = Directory.GetFiles(logsFolder)
                                .OrderByDescending(f => f)
                                .ToList();

        if (!logFiles.Any())
            return Ok("Logs folder is empty. No log files found.");

        var latestLogFile = logFiles.First();
        string content = "";
        using (var stream = new FileStream(latestLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        using (var reader = new StreamReader(stream))
        {
            content = reader.ReadToEnd();
        }

        return Ok(content);
    }
    [AllowAnonymous]
    [HttpGet("download")]
    public IActionResult DownloadLatestLog()
    {
        var logsFolder = Path.Combine(_env.ContentRootPath, "Logs");

        if (!Directory.Exists(logsFolder))
            return NotFound("Logs folder not found");

        var latestLog = Directory.GetFiles(logsFolder)
            .OrderByDescending(f => f)
            .FirstOrDefault();

        if (latestLog == null)
            return NotFound("No log files found");

        byte[] content;

        using (var stream = new FileStream(latestLog, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                content = memoryStream.ToArray();
            }
        }
        var fileName = Path.GetFileName(latestLog);

        return File(content, "text/plain", fileName);
    }
}
