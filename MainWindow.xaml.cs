using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        double mainValue = 0;
        double heldValue = 0;
        bool hasDecimal = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        // Clears the main window
        private void btnClearMain_Click(object sender, RoutedEventArgs e)
        {
            ClearMain();
        }

        // Sets both values to a default of 0
        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            mainValue = 0;
            heldValue = 0;

            UpdateBothValues();
        }

        private void PerformOperation(object sender, RoutedEventArgs e)
        {
            string symbol = ConvertContent(sender);
            ConvertValuesToNumbers();

            if (txtSymbol.Text == "" && symbol == "=")
            {
                return;
            }

            hasDecimal = false;

            double sum = 0;
            if (txtSymbol.Text == "")
            {
                txtSymbol.Text = symbol;
                MainToHeld();
                ClearMain();
                return;
            }

            if (symbol == "=")
            {
                sum = MathOperation(txtSymbol.Text);

                mainValue = sum;
                heldValue = 0;
                txtSymbol.Text = "";


            }
            else
            {
                sum = MathOperation(txtSymbol.Text);
                AssignValueToHeld(sum);
                txtSymbol.Text = symbol;
            }

            UpdateBothValues();

        }

        private void AddValueToDisplay(object sender, RoutedEventArgs e)
        {

            string value = ConvertContent(sender);

            bool isDecimal = value == ".";

            if(isDecimal)
            {
                if (hasDecimal) return;
                else if (txtMain.Text == "0")
                {
                    txtMain.Text = "0.";
                    return;
                }

                hasDecimal = true;
            }


            if (txtMain.Text == "0")
            {
                txtMain.Text = value;
                return;
            }
            
            txtMain.Text += value;


        } // AddValueToDisplay()

        #region Math Operations
        public double Add(double number1, double number2)
        {
            return number1 + number2;
        } // Add()

        public double Subtract(double number1, double number2)
        {
            return number1 - number2;
        }

        public double Multiply(double number1, double number2)
        {
            return number1 * number2;
        }

        public double Divide(double number1, double number2)
        {
            return number2 / number1;
        }

        // I've written this method to take care our doing individual calculations. Another part of my
        // approach by breaking down every method to do a single thing. Here I just pass in the symbol
        // that was clicked. I use a switch and my math methods I created to perform the calculation.
        // Then I return the result.

        public double MathOperation(string symbol)
        {

            double sum = 0;
            switch (symbol)
            {
                case "+":
                    sum = Add(mainValue, heldValue);
                    break;
                case "-":
                    sum = Subtract(mainValue, heldValue);
                    break;
                case "*":
                    sum = Multiply(mainValue, heldValue);
                    break;
                case "/":
                    sum = Divide(mainValue, heldValue);
                    break;
            }
            return sum;
        }
        #endregion MathOperations


     


        #region GUIControl

        // Helper Method:  I use this in tandem with our events. I call this everytime i'm coverting a number from our button to a string we can work with.
        public string ConvertContent(object sender)
        {
            Button button = (Button)sender;
            string number = button.Content.ToString();
            return number;
        }

        // Assigns a value to heldValue
        // This is a helper method. Even though it's just one line, the name basically replaces needing
        // to comment "assigning value to held" because our method name is descriptive
        public void AssignValueToHeld(double value)
        {
            heldValue = value;
        }

        public void MainValue()
        {
            txtMain.Text = mainValue.ToString();
        }
        public void HeldValue()
        {
            txtHeld.Text = heldValue.ToString();
        }

        // Updates the values displayed in both the main and help text panels from their
        // variable counter parts
        public void UpdateBothValues()
        {
            HeldValue();
            MainValue();

        }

        // Sets main value to 0 and updates the main window to the mainvalue
        public void ClearMain()
        {
            mainValue = 0;
            MainValue();
        }

        // Swaps the value in main to the value in held
        public void MainToHeld()
        {
            txtHeld.Text = txtMain.Text;
        }

        // Parses all numbers
        public void ConvertValuesToNumbers()
        {
            mainValue = double.Parse(txtMain.Text);
            heldValue = double.Parse(txtHeld.Text);
        }


        #endregion GuiControl

    
    }
}
