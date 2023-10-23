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

namespace juego_parejas_barron
{
    public partial class Form1 : Form

    {
        Random random = new Random();

        List<string> icons = new List<string>()
        {
            "♔","♔","♕","♕","♖","♖","♗","♗",
            "♘","♘","♙","♙","♧","♧","♡","♡",
        };
        Label firstClicked = null;
        Label secondClicked = null;
        int timeLeft;

        public Form1()
        {
            InitializeComponent();

            AssignIconsToSquares();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
            timeLeft = 45;
            timeLabel.Text = "45 segundos";
            timerTemp.Start();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {

                if (clickedLabel.ForeColor == Color.White)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.White;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.White;

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
            Hide();

            FormNivel2 formNivel2 = new FormNivel2();
            formNivel2.Show();

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
                Application.Exit();
            }
            
        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void buttonCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void buttonRendirse_Click(object sender, EventArgs e)
        {
            timerTemp.Stop(); 
            MessageBox.Show("¡Gracias por intentarlo!", "Gracias");
            Close();
        }

        private void buttonIntentar_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonAyuda_Click(object sender, EventArgs e)
        {
            FormAyuda ayuda = new FormAyuda();
            ayuda.Show();

        }
    }
}
