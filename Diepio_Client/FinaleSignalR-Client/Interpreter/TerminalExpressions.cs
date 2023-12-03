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
            form.ChangeSpeed("zero");
            form.ChangeDebug("zero");
        }
    }

    public class LowExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.ChangeSpeed("low");
            form.ChangeDebug("low");
        }
    }

    public class MediumExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.ChangeSpeed("medium");
            form.ChangeDebug("medium");
        }
    }

    public class HighExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.ChangeSpeed("high");
            form.ChangeDebug("high");
        }
    }

    public class SaveExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.AddMessage("save aaaaa");
            form.SavePositions();
        }
    }

    public class RestoreExpression : IExpression
    {
        public void interpret(Form1 form)
        {
            form.AddMessage("restore aaaaa");
            form.RestorePositions(0);
        }
    }
}
