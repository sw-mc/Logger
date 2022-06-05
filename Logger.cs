namespace SkyWing.Logger;

public interface Logger {
	
    /**
	 * System is unusable
	 */
    public void Emergency(string message);

    /**
	 * Action must be taken immediately
	 */
    public void Alert(string message);

    /**
	 * Critical conditions
	 */
    public void Critical(string message);

    /**
	 * Runtime errors that do not require immediate action but should typically
	 * be logged and monitored.
	 */
    public void Error(string message);

    /**
	 * Exceptional occurrences that are not errors.
	 *
	 * Example: Use of deprecated APIs, poor use of an API, undesirable things
	 * that are not necessarily wrong.
	 */
    public void Warning(string message);

    /**
	 * Normal but significant events.
	 */
    public void Notice(string message);

    /**
	 * Interesting events.
	 */
    public void Info(string message);

    /**
	 * Detailed debug information.
	 */
    public void Debug(string message);

    /**
	 * Logs with an arbitrary level.
	 */
    public void Log(LogLevel level, string message);

    /**
	 * Logs a Throwable object
	 */
    public void LogException(Exception e, string? trace = null);

    public static string LogLevelToString(LogLevel level) {
        return level switch {
            LogLevel.Emergency => "emergency",
            LogLevel.Alert => "alert",
            LogLevel.Critical => "critical",
            LogLevel.Error => "error",
            LogLevel.Warning => "warning",
            LogLevel.Notice => "notice",
            LogLevel.Info => "info",
            LogLevel.Debug => "debug",
            _ => throw new ArgumentException("Unknown LogLevel: " + level)
        };
    }
    
}

public enum LogLevel {
    
    Emergency,
    Alert,
    Critical,
    Error,
    Warning,
    Notice,
    Info,
    Debug
    
}

public class SimpleLogger : Logger {

    public void Emergency(string message) {
        Log(LogLevel.Emergency, message);
    }

    public void Alert(string message) {
        Log(LogLevel.Alert, message);
    }

    public void Critical(string message) {
        Log(LogLevel.Critical, message);
    }

    public void Error(string message) {
        Log(LogLevel.Error, message);
    }

    public void Warning(string message) {
        Log(LogLevel.Warning, message);
    }

    public void Notice(string message) {
        Log(LogLevel.Notice, message);
    }

    public void Info(string message) {
        Log(LogLevel.Info, message);
    }

    public void Debug(string message) {
        Log(LogLevel.Debug, message); 
    }

    public void Log(LogLevel level, string message) {
        Console.WriteLine("[" + Logger.LogLevelToString(level).ToUpper() + "] " + message);
    }

    public void LogException(Exception e, string? trace = null) {
        Critical(e.Message);
        if (e.StackTrace != null) Console.WriteLine(e.StackTrace);
    }
    
}

public class PrefixedLogger : SimpleLogger {

    private readonly Logger logger;
    public string Prefix { get; set; }
    
    public PrefixedLogger(Logger logger, string prefix) {
        this.logger = logger;
        Prefix = prefix;
    }
    
    public new void Log(LogLevel level, string message) {
        logger.Log(level, "[" + Prefix + "] " + message);
    }
    
    public new void LogException(Exception e, string? trace = null) {
        logger.LogException(e, trace);
    }
    
}

public sealed class GlobalLogger {

    private static Logger? logger;
    
    public static Logger Logger {
        get =>logger ??= new SimpleLogger();
        set => logger = value;
    }
    
}