using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator {
    public partial class CalculatorForm : Form {

        // member data
        private String m_result = ""; // store result for printing
        private String m_tempValue = ""; // store string value of number
        private decimal m_val; // store decimal value of number
        private char m_lastVal; // keep track of the last value used
        private Stack<decimal> m_values = new Stack<decimal>(); // stack for storing the values for calculation 
        private Stack<char> m_operators = new Stack<char>(); // stack for storing the operators for calculation
        private Font m_originalFont;

        private bool VALID = true; // keep track of if there has been an error or undefined mathematical behaviour
        private bool ENTER = false; // keep track of if enter has been pressed or not
        private string m_errorMessage = ""; // store the error message for displaying when the equals key is pressed

        
        ///////////////////////////////////////////// INIT METHODS //////////////////////////////////////////////////
        
        // Public methods
        public CalculatorForm() {
            InitializeComponent();
            answerLbl.Focus();
            m_originalFont = answerLbl.Font;
            this.KeyPress += new KeyPressEventHandler(CalculatorForm_KeyPress);
        }
 
        // Private methods
        private void CalculatorForm_Load(object sender, EventArgs e) {
            // perform additional setup if needed
        }
        private void CalculatorForm_KeyPress(object sender, KeyPressEventArgs e) {
            switch (e.KeyChar.ToString()) {
                case "\r":
                case "\n":
                    btnEqual_Click(sender, e);
                    break;
                case "\b":
                    btnClear_Click(sender, e);
                    break;
                case "0":
                    btn0_Click(sender, e);
                    break;
                case "1":
                    btn1_Click(sender, e);
                    break;
                case "2":
                    btn2_Click(sender, e);
                    break;
                case "3":
                    btn3_Click(sender, e);
                    break;
                case "4":
                    btn4_Click(sender, e);
                    break;
                case "5":
                    btn5_Click(sender, e);
                    break;
                case "6":
                    btn6_Click(sender, e);
                    break;
                case "7":
                    btn7_Click(sender, e);
                    break;
                case "8":
                    btn8_Click(sender, e);
                    break;
                case "9":
                    btn9_Click(sender, e);
                    break;
                case ".":
                    btnDot_Click(sender, e);
                    break;
                case "(":
                    btnLeftParenth_Click(sender, e);
                    break;
                case ")":
                    btnRightParenth_Click(sender, e);
                    break;
                case "*":
                case "x":
                case "X":
                    btnTimes_Click(sender, e);
                    break;
                case "/":
                    btnDiv_Click(sender, e);
                    break;
                case "-":
                    btnMinus_Click(sender, e);
                    break;
                case "+":
                    btnPlus_Click(sender, e);
                    break;
            }
            answerLbl.Focus();
        }
 
        // Labels
        private void answerLbl_Click(object sender, EventArgs e) {
            answerLbl.Text = m_result;
        }

        
        //////////////////////////////////////// BUTTON CONTROLS ///////////////////////////////////////
        
        private void btnEqual_Click(object sender, EventArgs e) {
            // parse string and print result
            if (!ENTER) {
                try {
                    if (m_tempValue.Length > 0) {
                        m_val = Convert.ToDecimal(m_tempValue);
                        m_values.Push(m_val); // get the last number
                        m_tempValue = ""; // reset temp value
                    }
                    m_result = "";

                    // keep finishing the calculations 
                    while (m_operators.Count != 0 && VALID) {
                        VALID = StackCalc();
                    }


                    if (VALID) {
                        try {
                            m_result = m_values.Pop().ToString();
                        } catch (InvalidOperationException operationE) {
                            Console.WriteLine(operationE.Message);
                            m_result = "Error";
                        }
                    } else {
                        m_result = m_errorMessage;
                    }
                    answerLbl.Text = m_result; // print the final result

                    // make sure the stacks are reset after
                    m_values.Clear();
                    m_operators.Clear();
                } catch (FormatException formatE) {
                    Console.WriteLine(formatE.Message + "HERE");
                    answerLbl.Text = "Error";
                } catch (OverflowException overflowE) {
                    Console.WriteLine(overflowE.Message);
                    answerLbl.Text = "Number was too large/small";
                } finally {
                    VALID = true; // reset for next iteration
                    ENTER = true;
                    StringLabelResize(); // resize label if need be
                    answerLbl.Focus();
                }
            } else {
                VALID = true; // reset for next iteration
                ENTER = true; // enter has been pressed twice so reset
                m_result = "";
                answerLbl.Text = m_result; // print the final result
                StringLabelResize(); // resize label if need be
                answerLbl.Focus();
            }
             
        }
        // Reset Button
        private void btnClear_Click(object sender, EventArgs e) {
            m_result = "";
            answerLbl.Text = m_result;
            m_tempValue = "";

            // make sure the stacks are reset after
            m_values.Clear();
            m_operators.Clear();
            answerLbl.Font = m_originalFont; // reset the font when clearing
            answerLbl.Focus();
        }


        // Numeric buttons
        private void btn0_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "0";
            answerLbl.Text = m_result;
            m_tempValue += "0";
            m_lastVal = '0';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn1_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "1";
            answerLbl.Text = m_result;
            m_tempValue += "1";
            m_lastVal = '1';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn2_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "2";
            answerLbl.Text = m_result;
            m_tempValue += "2";
            m_lastVal = '2';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn3_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "3";
            answerLbl.Text = m_result;
            m_tempValue += "3";
            m_lastVal = '3';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn4_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "4";
            answerLbl.Text = m_result;
            m_tempValue += "4";
            m_lastVal = '4';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn5_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "5";
            answerLbl.Text = m_result;
            m_tempValue += "5";
            m_lastVal = '5';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn6_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "6";
            answerLbl.Text = m_result;
            m_tempValue += "6";
            m_lastVal = '6';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn7_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "7";
            answerLbl.Text = m_result;
            m_tempValue += "7";
            m_lastVal = '7';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn8_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "8";
            answerLbl.Text = m_result;
            m_tempValue += "8";
            m_lastVal = '8';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btn9_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "9";
            answerLbl.Text = m_result;
            m_tempValue += "9";
            m_lastVal = '9';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        // non numeric buttons
        private void btnDot_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += ".";
            answerLbl.Text = m_result;
            m_tempValue += ".";
            m_lastVal = '.';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btnRightParenth_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += ")";
            answerLbl.Text = m_result;

            try {
                if (m_tempValue.Length != 0) {
                    m_val = Convert.ToDecimal(m_tempValue);
                    m_values.Push(m_val); // get the last number
                    m_tempValue = ""; // reset temp value
                }
               

                // go through and process the parenthesis
                while (m_operators.Peek() != '(' && VALID) {
                    VALID = StackCalc();

                    Console.WriteLine(m_operators.Peek() + ", size: " + m_operators.Count);
                    foreach (decimal d in m_values)
                        Console.Write(d + ", ");
                }
                Console.WriteLine(m_operators.Pop() + "was popped"); // remove the left parenthesis
                foreach (decimal d in m_values)
                    Console.Write(d + ", ");
            } catch (FormatException formatE) {
                Console.WriteLine(formatE.Message);
                VALID = false;
                m_errorMessage = "Error";
            } catch (OverflowException overflowE) {
                Console.WriteLine(overflowE.Message);
                VALID = false;
                m_errorMessage = "Error";
            } finally {
                m_lastVal = ')';
                StringLabelResize(); // resize label if need be
                answerLbl.Focus();
            }    
        }
        private void btnLeftParenth_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "(";
            answerLbl.Text = m_result;
            m_operators.Push('(');
            m_tempValue = "";
            m_lastVal = '(';
            StringLabelResize(); // resize label if need be
            answerLbl.Focus();
        }
        private void btnDiv_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "/";
            answerLbl.Text = m_result;

            if (VALID) { // only perform calculations if the current equation is valid
                try {
                    if (m_tempValue.Length > 0) {
                        m_val = Convert.ToDecimal(m_tempValue);
                        m_values.Push(m_val); // get the last number
                        m_tempValue = ""; // reset temp value
                    }
                    m_operators.Push('/'); // highest precedence (right now) so push
                } catch (FormatException formatE) {
                    Console.WriteLine(formatE.Message);
                    VALID = false;
                    m_errorMessage = "Error";
                } catch (OverflowException overflowE) {
                    Console.WriteLine(overflowE.Message);
                    VALID = false;
                    m_errorMessage = "Error";
                } finally {
                    m_lastVal = '/';
                    StringLabelResize(); // resize label if need be
                    answerLbl.Focus();
                }
            }
        }
        private void btnTimes_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "*";
            answerLbl.Text = m_result;

            if (VALID) { // only perform calculations if the current equation is valid
                try {
                    if (m_tempValue.Length > 0) {
                        m_val = Convert.ToDecimal(m_tempValue);
                        m_values.Push(m_val); // get the last number
                        m_tempValue = ""; // reset temp value
                    }
                    m_operators.Push('*'); // highest precedence (right now) so push
                } catch (FormatException formatE) {
                    Console.WriteLine(formatE.Message);
                    VALID = false;
                    m_errorMessage = "Error";
                } catch (OverflowException overflowE) {
                    Console.WriteLine(overflowE.Message);
                    VALID = false;
                    m_errorMessage = "Error";
                } finally {
                    m_lastVal = '*';
                    StringLabelResize(); // resize label if need be
                    answerLbl.Focus();
                }
            }
        }
        private void btnMinus_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "-";
            answerLbl.Text = m_result;

            if (VALID) { // only perform calculations if the current equation is valid
                // if the string is empty, the sign is a unary operator, not a binary op
                Console.WriteLine(m_lastVal + " was the last value");

                if (m_tempValue.Length == 0 && m_lastVal != ')') { // choose when to properly apply the unary op
                    m_tempValue += "-";
                    answerLbl.Focus();

                } else { // binary op so process
                    try {
                        if (m_tempValue.Length > 0) {
                            m_val = Convert.ToDecimal(m_tempValue);
                            m_values.Push(m_val); // get the last number
                            m_tempValue = ""; // reset temp value
                        }

                        if (m_operators.Count > 0)
                            VALID = StackCalc();

                        m_operators.Push('-');
                    } catch (FormatException formatE) {
                        Console.WriteLine(formatE.Message);
                        VALID = false;
                        m_errorMessage = "Error";
                    } catch (OverflowException overflowE) {
                        Console.WriteLine(overflowE.Message);
                        VALID = false;
                        m_errorMessage = "Error";
                    } finally {
                        m_lastVal = '-';
                        StringLabelResize(); // resize label if need be
                        answerLbl.Focus();
                    }
                }
            }
        }
        private void btnPlus_Click(object sender, EventArgs e) {
            if (ENTER) ENTER = false;
            m_result += "+";
            answerLbl.Text = m_result;

            if (VALID) { // only perform calculations if the current equation is valid
                try {
                    if (m_tempValue.Length > 0) {
                        m_val = Convert.ToDecimal(m_tempValue);
                        m_values.Push(m_val); // get the last number
                        m_tempValue = ""; // reset temp value
                    }

                    if (m_operators.Count > 0)
                        VALID = StackCalc();

                    m_operators.Push('+');
                } catch (FormatException formatE) {
                    Console.WriteLine(formatE.Message);
                    VALID = false;
                    m_errorMessage = "Error";
                } catch (OverflowException overflowE) {
                    Console.WriteLine(overflowE.Message);
                    VALID = false;
                    m_errorMessage = "Error";
                } finally {
                    m_lastVal = '+';
                    StringLabelResize(); // resize label if need be
                    answerLbl.Focus();
                }
            }
        }

        
        ////////////////////////////////////// HELPER FUNCTIONS ////////////////////////////////////////////
        
        /**
         * Purpose: To perform arithmetic operations based on operator precedence.
         * Pre: The operator stack and values stack must not be empty to perform calculations
         * Post: The stacks are modified and the values popped are properly used and the result is 
         * pushed back on the values stack.
         * Return: true if no errors occured, else false if there was a stack error or arithmetic error.
         */
        private bool StackCalc() {
            decimal v1, v2;
            char op;

            // check operator precedence
            if (m_operators.Count > 0 && m_values.Count > 0) {
                if (m_operators.Peek() == '*' || m_operators.Peek() == '/') {
                    try {
                        v2 = m_values.Pop(); // most recent item pushed
                        v1 = m_values.Pop();
                        op = m_operators.Pop();

                        switch (op) {
                            case '/': // not all cases valid
                                if (v2 == 0) { // invalid operation of division by 0
                                    m_errorMessage = "Undefined";
                                    return false;
                                } else m_values.Push(v1 / v2); // valid division
                                break;
                            case '*': // all cases valid
                                m_values.Push(v1 * v2);
                                break;
                        }

                    } catch (InvalidOperationException e) {
                        Console.WriteLine(e.Message);
                        m_errorMessage = "Error";
                        return false;
                    }
                } else if (m_operators.Peek() == '-' || m_operators.Peek() == '+') {
                    try {
                        v2 = m_values.Pop(); // most recent item pushed
                        v1 = m_values.Pop();
                        op = m_operators.Pop();

                        switch (op) {
                            case '+': // no ill side effects
                                m_values.Push(v1 + v2);
                                break;
                            case '-': // no ill side effects
                                m_values.Push(v1 - v2);
                                break;
                        }
                    } catch (InvalidOperationException e) {
                        Console.WriteLine(e.Message + "HERE");
                        m_errorMessage = "Error";
                        return false;
                    }
                } else if (m_operators.Peek() == '(') {
                    // dealt with outside of this iteration
                    Console.WriteLine(m_values.Peek());
                    return true;
                } else { 
                    // error
                    Console.WriteLine("Error in StackCalc() stacks");
                    m_errorMessage = "Error";
                    return false;
                }
            } else {
                m_errorMessage = "Error";
                return false;
            }
            return true; // no issues so continue on
        }

        /**
         * Purpose: To resize the answer label font if the string begins to exceed its max size.
         * Pre: The label has been instantiated.
         * Post: The font size is modified if the label characters exceed it.
         * Return: void
         * 
         * Borrowed from https://stackoverflow.com/questions/9527721/resize-text-size-of-a-label-when-the-text-got-longer-than-the-label-size
         */
        private void StringLabelResize() {
            while (answerLbl.Width < TextRenderer.MeasureText(answerLbl.Text, new Font(answerLbl.Font.FontFamily, 
                                                                                       answerLbl.Font.Size, 
                                                                                       answerLbl.Font.Style)).Width) {
                answerLbl.Font = new Font(answerLbl.Font.FontFamily, 
                                          answerLbl.Font.Size - 0.1f, 
                                          answerLbl.Font.Style);
            }
        }
    }
}
