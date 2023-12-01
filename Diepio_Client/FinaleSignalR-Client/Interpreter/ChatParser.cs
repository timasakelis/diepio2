using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Interpreter
{
    public class ChatParser
    {
        private readonly Dictionary<string, IExpression> expressions;

        public ChatParser()
        {
            expressions = new Dictionary<string, IExpression>(StringComparer.OrdinalIgnoreCase)
            {
                { "debug", new DebugExpression() },
                { "speed", new SpeedExpression() },
                { "zero", new ZeroExpression() },
                { "low", new LowExpression() },
                { "medium", new MediumExpression() },
                { "high", new HighExpression() }
            };
        }

        public bool ParseCommand(string userInput, Form1 form)
        {
            bool succeeded = false;
            string[] commandTokens = userInput.Split(' ');

            for (int i = 0; i < commandTokens.Length; i++)
            {
                if (expressions.TryGetValue(commandTokens[i], out var command))
                {
                    succeeded = true;
                    command.interpret(form);
                }
            }

            return succeeded;
        }
    }
}
