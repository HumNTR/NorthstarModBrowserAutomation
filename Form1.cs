using Octokit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ModBrowserAutomation
{
    public partial class Form1 : Form
    {
        string tempPath;
        public Form1()
        {
            InitializeComponent();
            tempPath = Path.GetTempPath();
        }
        string id, authname, name, mod;

        private void button1_Click(object sender, EventArgs e)
        {

            
            using (WebClient client = new WebClient()) // WebClient class inherits IDisposable
            {
                // Or you can get the file content without saving it
                string htmlCode = client.DownloadString(textBox1.Text);
                if (htmlCode.Contains("octolytics-dimension-repository_id")) id = htmlCode.Substring(htmlCode.IndexOf("octolytics-dimension-repository_id") + 45,9);

            }
            getAuth_Name_Mod();
            textBox2.Text = name + ";" + authname + ";" + textBox1.Text + ";" + id + ";" + mod;
          

        }
        private  async void getAuth_Name_Mod()
        {
            GitHubClient git; git = new GitHubClient(new ProductHeaderValue("idk"));
            var results =  git.Repository.Get(long.Parse(id)).Result;
            authname = results.Owner.Login;
            
            name = results.Name;
            if (git.Repository.Release.GetAll(long.Parse(id)).Result.Count == 0) mod = "1";
            else mod = "0";


        }
    }
}
