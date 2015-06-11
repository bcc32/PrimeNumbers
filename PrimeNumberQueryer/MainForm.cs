using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Number = System.Numerics.BigInteger;
using PrimeNumbers;

namespace PrimeNumberQueryer
{
    public partial class MainForm : Form
    {
        delegate void UpdateLabelDelegate(String text);
        private Thread workerThread = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // use 184091991840919913 to test

                // kill previous worker
                if (workerThread != null && workerThread.IsAlive)
                {
                    workerThread.Abort();
                }

                Number number = Number.Parse(textBox1.Text);

                workerThread = new Thread(new ThreadStart(() =>
                {
                    String message = number < 2 ? "Not defined" :
                        Primes.IsPrime(number) ? "Prime" : "Composite";
                    // safely update the label
                    this.Invoke(new UpdateLabelDelegate((text) => label1.Text = text), new object[] { message });
                }));

                workerThread.IsBackground = true;
                label1.Text = "thinking...";
                workerThread.Start();
            }
            catch (System.FormatException)
            {
                label1.Text = "Not a number.";
            }
            catch (System.OverflowException oe)
            {
                if (oe.Message.StartsWith("unreasonable"))
                {
                    label1.Text = "not feelin' it.";
                }
                else
                {
                    label1.Text = "Too big.";
                }
            }
        }
    }
}
