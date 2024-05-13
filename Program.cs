using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Xml.Linq;


// Print 'Hello World!'
Console.WriteLine("Hello, World!");


static void Button_Click(AutomationElement window) //object sender, RoutedEventArgs e)
{
    //Condition condition = new PropertyCondition(AutomationElement.NameProperty, "Calculator");
    //AutomationElement window = root.FindFirst(TreeScope.Children, condition);

    if (window != null)
    {
        // get and click the buttons for the calculation
        FindAndClickButton("Five", window);
        Console.WriteLine("Clicked 5");
        FindAndClickButton("Plus", window);
        Console.WriteLine("Clicked +");
        FindAndClickButton("Nine", window);
        Console.WriteLine("Clicked 9");
        FindAndClickButton("Multiply by", window);
        Console.WriteLine("Clicked *");
        FindAndClickButton("Three", window);
        Console.WriteLine("Clicked 3");
        FindAndClickButton("Divide by", window);
        Console.WriteLine("Clicked /");
        FindAndClickButton("Four", window);
        Console.WriteLine("Clicked 4");
        FindAndClickButton("Equals", window);
        Console.WriteLine("Clicked =");


        //get the result
        //System.Windows.Automation.Condition conditionResult = new PropertyCondition(AutomationElement.AutomationIdProperty, "CalculatorResults");
        //var result = calcUI.FindFirst(TreeScope.Descendants, conditionResult);

    }
}

static AutomationElement SearchForWindow(Regex windowRx)
{

    Console.WriteLine("Seaching for Window");

    TreeWalker walker = TreeWalker.ControlViewWalker;
    AutomationElement? matchingWindow = null;

    AutomationElement rootElement = AutomationElement.RootElement;
    Console.WriteLine(rootElement.Current.Name);

    AutomationElement window0 = walker.GetFirstChild(rootElement);
    Console.WriteLine("window0");
    Console.WriteLine(window0.Current.Name);
    AutomationElement window1 = walker.GetNextSibling(window0);
    Console.WriteLine("window1");
    Console.WriteLine(window1.Current.Name);
    AutomationElement window2 = walker.GetNextSibling(window1);
    Console.WriteLine("window2");
    Console.WriteLine(window2.Current.Name);

    AutomationElementCollection allWindows = rootElement.FindAll(TreeScope.Children, Condition.TrueCondition);
    foreach (AutomationElement window in allWindows) { Console.WriteLine(window.Current.Name); }

    foreach (AutomationElement window in allWindows)
    {

        Console.WriteLine("windowRx");
        Console.WriteLine(windowRx);
        Console.WriteLine("window.Current.Name");
        Console.WriteLine(window.Current.Name);
        string pattern = @"^Calculator$";
        if (Regex.IsMatch(window.Current.Name, pattern))
        {
            matchingWindow = window;
            //matchingWindow.SetFocus();

            Console.WriteLine($"Window '{window.Current.Name}' found");

            break;
        }
    }

    //if (matchingWindow == null) throw new Exception($"Window ${windowRx.ToString} not found");

    return matchingWindow;
}

static AutomationElement FindWindow(Condition condition)
{
    // Search for the window matching the condition
    AutomationElement rootElement = AutomationElement.RootElement;
    AutomationElement calculatorWindow = rootElement.FindFirst(TreeScope.Children, condition);
    return calculatorWindow;
}

static void FindAndClickButton(string name, AutomationElement calcUI)
{
    // Define conditions to find the button by name
    Condition condition1 = new PropertyCondition(AutomationElement.ClassNameProperty, "Button");
    Condition condition2 = new PropertyCondition(AutomationElement.NameProperty, name);
    Condition condition = new AndCondition(condition1, condition2);

    // Find the button
    AutomationElement button = calcUI.FindFirst(TreeScope.Descendants, condition);

    // If button is found, perform click operation
    if (button != null)
    {
        InvokePattern btnPattern = button.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
        btnPattern.Invoke();
    }
    else
    {
        Console.WriteLine($"Button '{name}' not found.");
    }
}


Regex windowRx = new Regex("Calculator", RegexOptions.IgnoreCase);
AutomationElement calculatorWindow = SearchForWindow(windowRx);

if (calculatorWindow != null) {
    Button_Click(calculatorWindow);

} else {
    Console.WriteLine("Calculator window not found.");
}
Console.WriteLine("Press Enter");
Console.ReadLine();
