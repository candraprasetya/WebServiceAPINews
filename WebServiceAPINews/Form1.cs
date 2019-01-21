using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebServiceAPINews
{
    public partial class Form1 : Form
    {
        WebClient webClient = new WebClient();
        int buttonRead = 1;
        int newsTitle = 1;

        public Form1()
        {
            InitializeComponent();
        }

        private System.Windows.Forms.Label AddTitleLabel(String title)
        {
            System.Windows.Forms.Label newsLabel = new System.Windows.Forms.Label();
            this.Controls.Add(newsLabel);
            newsLabel.Top = newsTitle * 50;
            newsLabel.Left = 15;
            newsLabel.Text = title;
            newsLabel.AutoSize = true;
            newsTitle = newsTitle + 1;

            return newsLabel;
        }

        private System.Windows.Forms.Button AddDetailButton(int postId)
        {
            System.Windows.Forms.Button buttonDetails = new System.Windows.Forms.Button();
            this.Controls.Add(buttonDetails);
            buttonDetails.Top = buttonRead * 50;
            buttonDetails.Left = 650;
            buttonDetails.Name = postId.ToString();
            buttonDetails.Text = "Detail";
            buttonDetails.Click += new EventHandler(btnDetails_Click);
            buttonRead = buttonRead + 1;

            return buttonDetails;
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            Button buttonDetails = sender as Button;
            try
            {
                var data = webClient.DownloadString("https://jsonplaceholder.typicode.com/posts/" + buttonDetails.Name);
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(data);

                String detail = obj["body"];
                MessageBox.Show(detail, "Detail from Post ID : " + buttonDetails.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var data = webClient.DownloadString("https://jsonplaceholder.typicode.com/posts");
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(data);

                for (int i = 0; i < 100; i++)
                {
                    int postId = obj[i]["id"];
                    String title = obj[i]["title"];
                    AddTitleLabel(title);
                    AddDetailButton(postId);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
