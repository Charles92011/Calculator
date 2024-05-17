using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Automation;

namespace RemoteCalculator {

    internal class Program {
 
        static void Main(string[] args) {

            using (Calculator calculator = new Calculator()) {

                if (calculator.Run()) {

                    calculator.Focus();
                    //calculator.Calculate("1+4+8");
                    calculator.UseAutomation();
                }
            }
        }
    }
}
