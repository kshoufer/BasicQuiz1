using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasicQuiz1
{
    public partial class SplashPage : Form
    {
        public SplashPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            QuestionPage qp = new QuestionPage();
            qp.Visible = true;
            this.Hide();
        }
    }
}
