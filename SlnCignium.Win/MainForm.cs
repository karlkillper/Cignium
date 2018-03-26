using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlnCignium.Business;
using SlnCignium.Entities;
using SlnCignium.Utilities;

namespace SlnCignium.Win
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchEngine();

        }

        private void SearchEngine() {
            List<string> lst = new List<string>();
            List<BeanEngine> lt = new List<BeanEngine>();
            EngineBusiness obj = new EngineBusiness();

            lst = obj.getWords(txtWords.Text);
            BeanEngine bean;
            int k = 1;
            foreach (var item in lst)
            {
                bean = new BeanEngine();
                bean.ID = k;
                bean.Word = item;
                bean.Google = SearchEngineGoogle(item);
                bean.Bing = SearchEngineMsn(item);
                lt.Add(bean);
                k++;
            }

            Int64 TotalGoogle = 0;
            Int64 TotalMsn = 0;

            foreach (var item in lt)
            {
                if (item.Google > item.Bing) {
                    item.Winner = "Google";
                }
                else if (item.Google < item.Bing)
                {
                    item.Winner = "Msn";
                }
                else
                {
                    item.Winner = "Empate";
                }

                TotalGoogle = TotalGoogle + item.Google;
                TotalMsn = TotalMsn + item.Bing;
            }


            if (TotalGoogle > TotalMsn) {
                lblTotalWinner.Text = "Google";
            }
            else if (TotalGoogle < TotalMsn)
            {
                lblTotalWinner.Text = "Msn";
            }
            else
            {
                lblTotalWinner.Text = "Empate";
            }

            dgvResults.DataSource = lt;
        }


        private Int64 SearchEngineGoogle(string words) {
            //String words = "c%23+query+google";
            //String words = txtWords.Text;
            String mpath = "https://www.google.com.pe/search?source=hp&ei=Abq4WpunEIuG5wL_3YvgCA&q=";
            BeanAnswer ent = new BeanAnswer();
            BeanSent snt = new BeanSent();
            EngineBusiness obj = new EngineBusiness();

            snt.Words = words;
            snt.MainPath = mpath;
            snt.Operation = ConfigurationEN.GoogleSearch; 
            ent = obj.QuerySearchEngine(snt);
            //lblGoogleResults.Text = ent.Total.ToString();
            return ent.Total;
        }

        private Int64 SearchEngineMsn(string words)
        {
            //String words = "c%23+query+google";
            //String words = txtWords.Text;
            String mpath = "https://www.bing.com/search?q=";
            BeanAnswer ent = new BeanAnswer();
            BeanSent snt = new BeanSent();
            EngineBusiness obj = new EngineBusiness();

            snt.Words = words;
            snt.MainPath = mpath;
            snt.Operation = ConfigurationEN.BingSearch;
            ent = obj.QuerySearchEngine(snt);
            //lblMsnResults.Text = ent.Total.ToString();
            return ent.Total;
        }

    }
}
