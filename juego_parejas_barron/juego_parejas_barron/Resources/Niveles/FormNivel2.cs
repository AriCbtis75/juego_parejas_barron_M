using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using juego_parejas_barron.Resources.Niveles;

namespace juego_parejas_barron.Resources.Niveles
{
    public partial class FormNivel2 : Form
    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "❀","❀","✿","✿","✽","✽","✺","✺","☘","☘",
            "❄","❄","⚜","⚜","☾","☾", "ꕥ","ꕥ","♕","♕",
           "☆","☆","✯","✯","☯","☯","⚘","⚘","☪","☪",

        };
        Label firstClicked = null;
        Label secondClicked = null;
        int timeLeft;

        public FormNivel2()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }
        private void AssignIconsToSquares()
        {

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;

                    icons.RemoveAt(randomNumber);
                }
            }
            timeLeft = 75;
            timeLabel.Text = "75 segundos";
            timerTemp.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {

                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }
        private void CheckForWinner()
        {

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;

                }
            }
            timerTemp.Stop();
            MessageBox.Show("¡Encontraste todos los pares!", "¡Felicitaciones!");
        }

        private void timerTemp_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                timeLabel.Text = timeLeft + " segundos";
            }
            else
            {
                timerTemp.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("Se te acabo el tiempo.", "¡Los siento!");
            }
        }

        private void buttonRendirse_Click(object sender, EventArgs e)
        {
            timerTemp.Stop();
            MessageBox.Show("¡Gracias por intentarlo!", "Gracias");
            Close();
        }

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
