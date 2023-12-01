using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaleSignalR_Client.Interpreter
{
    public class DebugExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.debugChangeEnable = true;
        }
    }

    public class SpeedExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.speedChangeEnable = true;
        }
    }

    public class ZeroExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.changeSpeed("zero");
            form.changeDebug("zero");
        }
    }

    public class LowExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.changeSpeed("low");
            form.changeDebug("low");
        }
    }

    public class MediumExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.changeSpeed("medium");
            form.changeDebug("medium");
        }
    }

    public class HighExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.changeSpeed("high");
            form.changeDebug("high");
        }
    }
}
