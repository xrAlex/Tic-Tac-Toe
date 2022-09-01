using System;
using System.Windows.Forms;

namespace Tic_Tac_Toe.UI
{
    public partial class GameConfigurationForm : Form
    {
        public GameConfigurationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var game = new GameBoardForm(Convert.ToInt32(numericUpDown1.Value),
                checkBox1.Checked, checkBox2.Checked);
            game.Show();
        }
    }
}

