using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;


namespace BasicQuiz1
{
    public partial class QuestionPage : Form
    {
        Question QuestionRow = new Question();
        List<Question> QuestionRowList = new List<Question>();
        int intUserAnswer;
        int intTestQuestionNumber = 1;
        int intPageCounter = 1;


        public QuestionPage()
        {
            InitializeComponent();
            System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection();
            conn.ConnectionString = @"integrated security=SSPI;data source=KEN-HP\SQLSERVER2008R2;" +
                "persist security info=False;initial catalog=quiz1";
            try
            {
                conn.Open();
                // Insert code to process data.

                SqlCommand command = new SqlCommand(
                    "SELECT * FROM Table_1;",
                    conn);

                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
                            reader.GetString(1));
                        Question qr = new Question();
                        qr.intQuestionID = (int)reader[0];
                        qr.strQuestionDescription = (string)reader[1];
                        qr.intAnswer1ID = (int)reader[2];
                        qr.strAnswer1Description = (string)reader[3];
                        qr.intAnswer2ID = (int)reader[4];
                        qr.strAnswer2Description = (string)reader[5];
                        qr.intAnswer3ID = (int)reader[6];
                        qr.strAnswer3Description = (string)reader[7];
                        qr.intCorrectAnswerID = (int)reader[8];
                        QuestionRowList.Add(qr);
                    }
                }// end if (reader.HasRows)
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();

            }// end try

            catch (Exception ex)
            {
                MessageBox.Show("Failed to connect to data source");
            }
            finally
            {
                conn.Close();
            }

            foreach (Question objQuestion in QuestionRowList)
            {
                if (objQuestion.intQuestionID == intTestQuestionNumber)
                {
                    lblQuestion.Text = objQuestion.strQuestionDescription;
                    radioButton1.Text = objQuestion.strAnswer1Description;
                    radioButton2.Text = objQuestion.strAnswer2Description;
                    radioButton3.Text = objQuestion.strAnswer3Description;
                }
            }// end foreach
        }// end QuestionPage()


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                Console.WriteLine("radio button1 checked");
                intUserAnswer = 1;
            }
            if (radioButton2.Checked)
            {
                Console.WriteLine("radio button2 checked");
                intUserAnswer = 2;
            }
            if (radioButton3.Checked)
            {
                Console.WriteLine("radio button3 checked");
                intUserAnswer = 3;
            }

            foreach (Question objQuestion in QuestionRowList)
            {
                if (objQuestion.intQuestionID == intTestQuestionNumber)
                {
                    if (objQuestion.intCorrectAnswerID == intUserAnswer)
                    {
                        lblResult.Text = "Your answer is correct!";
                        lblResult.Visible = true;
                    }
                    else
                    {
                        lblResult.Text = "Your answer is NOT correct.";
                        lblResult.Visible = true;
                    }
                }
            }// end foreach

        }// end btnSubmit_Click

    }// end class QuestionPage
}// end namespace BasicQuiz1
