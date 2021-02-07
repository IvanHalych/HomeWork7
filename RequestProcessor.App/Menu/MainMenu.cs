using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RequestProcessor.App.Exceptions;
using RequestProcessor.App.Logging;
using RequestProcessor.App.Models;
using RequestProcessor.App.Services;

namespace RequestProcessor.App.Menu
{
    /// <summary>
    /// Main menu.
    /// </summary>
    internal class MainMenu : IMainMenu
    {
        /// <summary>
        /// Constructor with DI.
        /// </summary>
        /// <param name="options">Options source</param>
        /// <param name="performer">Request performer.</param>
        /// <param name="logger">Logger implementation.</param>
        private IRequestPerformer performer;
        private IOptionsSource options;
        private ILogger logger;
        public MainMenu(
            IRequestPerformer performer, 
            IOptionsSource options, 
            ILogger logger)
        {
            this.performer = performer;
            this.options = options;
            this.logger = logger;
        }

        public async Task<int> StartAsync() 
        {
            try
            {
                logger.Log("Download options");
                MainMenu.ConsoleWrite("Download options");
                var optionsValue = (await options.GetOptionsAsync()).Where(option =>option.Item1.IsValid==true);
                logger.Log("Run tasks");
                MainMenu.ConsoleWrite("Run tasks");
                var tasks = optionsValue
                    .Select(opt => performer.PerformRequestAsync(opt.Item1, opt.Item2)).ToArray();
                logger.Log("Wait tasks");
                MainMenu.ConsoleWrite("Wait tasks");
                Task.WaitAll(tasks);
                tasks.ToList().ForEach(t =>
                {
                    logger.Log("Id task:" + t.Id + ",Status:" + t.Status.ToString() + ",Result:" + t.Result);
                    MainMenu.ConsoleWrite("Id task:" + t.Id + ",Status:" + t.Status.ToString() + ",Result:" + t.Result);
                });
                return 0;
            }
            catch (PerformException perform)
            {
                return -1;

            }
        }
        public static void ConsoleWrite(string message)
        {
            Console.WriteLine(message);
        }
    }
}
