using System;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Configuration : Form
    {
        public Configuration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var game = new GameForm(Convert.ToInt32(numericUpDown1.Value),
                checkBox1.Checked, checkBox2.Checked);
            game.Show();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
