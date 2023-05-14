using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MqttSupport;

/// <summary>
/// Log helper class 
/// </summary>
public static class LogHelper
{
    public static void Log(string message, ConsoleColor consoleColor = ConsoleColor.Black)
    {
        if (consoleColor != ConsoleColor.Black)
        {
            var backup = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(message);
            Console.ForegroundColor = backup;
        }
        else
        {
            Console.WriteLine(message);
        }
    }
}
