using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace Web_Browser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        WebBrowser webB;
        private void Form1_Load(object sender, EventArgs e)
        {
            webB = new WebBrowser() { ScriptErrorsSuppressed = true, Parent=tabPage1,Dock=DockStyle.Fill };//tạo web browser mới
        }

        private void toolStripButton1_Click(object sender, EventArgs e)//nút quay lại
        {
            webB.GoBack();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)//nút đi tiếp
        {
            webB.GoForward();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)//nút tải lại
        {
            webB.Refresh();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)//nút dừng tải
        {
            webB.Stop();
        }
        string searchText, searchEngine= "https://www.google.com/search?q=";//search engine mặc định là google.com
        private void toolStripButton5_Click(object sender, EventArgs e)//nút tìm kiếm
        {
            searchText = toolStripTextBox1.Text;
            if (searchText.Contains("www.")||searchText.Contains("http"))
                webB.Navigate(searchText);
            else
                webB.Navigate(searchEngine + searchText);
            webB.DocumentCompleted += WebB_DocumentCompleted1;
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)//nhấn enter tìm kiếm
        {
            if (e.KeyChar == (char)Keys.Enter) 
                toolStripButton5_Click(null,EventArgs.Empty);
       }

        private void toolStripButton7_Click(object sender, EventArgs e)//nút new tab
        {
            TabPage newTab = new TabPage();
            newTab.Text = "New Tab";
            tabControl1.Controls.Add(newTab);
            tabControl1.SelectTab(tabControl1.TabCount - 1);
            webB = new WebBrowser() { ScriptErrorsSuppressed = true, Parent = newTab, Dock = DockStyle.Fill };
            toolStripTextBox1.Text = "";
        }

        private void toolStripButton8_Click(object sender, EventArgs e)//nút close tab
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void WebB_DocumentCompleted1(object sender, WebBrowserDocumentCompletedEventArgs e)//cập nhật tên tab
        {
            tabControl1.SelectedTab.Text = webB.DocumentTitle;
            toolStripTextBox1.Text = webB.Url.ToString();
        }


        private void googleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchEngine = "https://www.google.com/search?q=";
            toolStripLabel1.Text = "Google";
        }

        private void yahooToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchEngine = "https://search.yahoo.com/search?q=";
            toolStripLabel1.Text = "Yahoo";
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.F5)
                webB.Refresh();
        }

    }
}
