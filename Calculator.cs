using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Windows.Input;

namespace RemoteCalculator {
    internal class Calculator : IDisposable {

        private readonly string processName = "CalculatorApp";
        private readonly string processExe = "calc.exe";
        private Process? process = null;

        public bool IsRunning() {

            process = null;

            Process[] processes = Process.GetProcessesByName(processName);

            if (processes.Any()) {
                process = processes[0];
            }

            Console.WriteLine("Calculator {0} running", processes.Any() ? "is" : "is not");

            return (process != null);
        }

        public bool Run() {

            if (IsRunning()) return true;

            Console.WriteLine("Starting Calculator");
            Process.Start("calc.exe");
            
            Thread.Sleep(1000);

            return IsRunning();
        }

        public void Focus() {

            if (process != null) {

                Console.WriteLine("Setting Focus on the Calculator");

                AutomationElement element = FindCalculatorWindow();
                element?.SetFocus();
            }
        }

        private AutomationElement FindCalculatorWindow() {

            Console.Write("Searching for Calculator");

            PropertyCondition typeCondition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
            PropertyCondition nameCondition = new PropertyCondition(AutomationElement.NameProperty, "Calculator");
            AndCondition andCondition = new AndCondition(typeCondition, nameCondition);
            
            AutomationElement? element = null;

            while (element == null) {
                Console.Write(".");
                element = AutomationElement.RootElement.FindFirst(TreeScope.Children, andCondition);
            }

            Console.WriteLine("\nFound Calculator Window");

            return element;
        }

        public void Dispose() {
            //throw new NotImplementedException();
        }

        public double Calculate(string equation) {
            double calculate = 0;

            Array keys = equation.ToCharArray();

            foreach (char key in keys) {

                string k = key.ToString();

                SendKeys.SendWait(k);
            }

            SendKeys.SendWait("=");

            return calculate;

        }

        public void UseAutomation() {

            Regex windowRx = new Regex("Calculator", RegexOptions.IgnoreCase);
            AutomationElement calculatorWindow = Rpa.SearchForWindow(windowRx);

            if (calculatorWindow != null) {
                Rpa.FindAndClickButton("Five", calculatorWindow);
                Console.WriteLine("Clicked 5");
                Rpa.FindAndClickButton("Plus", calculatorWindow);
                Console.WriteLine("Clicked +");
                Rpa.FindAndClickButton("Nine", calculatorWindow);
                Console.WriteLine("Clicked 9");
                Rpa.FindAndClickButton("Multiply by", calculatorWindow);
                Console.WriteLine("Clicked *");
                Rpa.FindAndClickButton("Three", calculatorWindow);
                Console.WriteLine("Clicked 3");
                Rpa.FindAndClickButton("Divide by", calculatorWindow);
                Console.WriteLine("Clicked /");
                Rpa.FindAndClickButton("Four", calculatorWindow);
                Console.WriteLine("Clicked 4");
                Rpa.FindAndClickButton("Equals", calculatorWindow);
                Console.WriteLine("Clicked =");

            } else {
                Console.WriteLine("Calculator window not found.");
            }
        }




    }
}
