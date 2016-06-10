using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Visa.Database;
using Visa.WebCrawler;

namespace Visa.WinForms
{
    public partial class MainForm : Form
    {
        private SeleniumCrawler _crawler;
        private BackgroundWorker _crawlerWorker;
        private BackgroundWorker _progressBarWorker;
        private int startPos = 1;


        public MainForm()
        {
            InitializeComponent();
            _crawler = new SeleniumCrawler();
            _crawlerWorker = new BackgroundWorker();
            _progressBarWorker = new BackgroundWorker();

            _progressBarWorker.DoWork += progressBarWorker_DoWork;
            _progressBarWorker.ProgressChanged += progressBarWorker_ProgressChanged;

            _crawlerWorker.DoWork += _crawlerWorker_DoWork;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lookUpEditServiceCenter.Properties.DataSource =
                InstanceProvider.DataSet.ServiceCenter.ToList();
            lookUpEditVisaCategory.Properties.DataSource =
                InstanceProvider.DataSet.VisaCategory.ToList();
        }

        private void buttonShow_Click(object sender, EventArgs e)
        {
            if (!_progressBarWorker.IsBusy)
                _progressBarWorker.RunWorkerAsync(new Tuple<int, int>(
                    ((VisaDataSet.ServiceCenterRow)lookUpEditServiceCenter.GetSelectedDataRow()).Value,
                    ((VisaDataSet.VisaCategoryRow)lookUpEditVisaCategory.GetSelectedDataRow()).Value));
        }

        private void progressBarWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var incomingData = e.Argument as Tuple<int, int>;

            if (incomingData == null) return;

            foreach (var count in _crawler.DoWork(incomingData.Item1, incomingData.Item2))
            {
                for (var i = startPos; i <= count; i++)
                {
                    // Wait 100 milliseconds.
                    Thread.Sleep(100);
                    // Report progress.
                    _progressBarWorker.ReportProgress(i);
                }
                startPos = count;
                return;
            }
        }

        private void progressBarWorker_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBarControl1.EditValue = e.ProgressPercentage;
        }

        private void _crawlerWorker_DoWork(object sender, DoWorkEventArgs e)
        {

        }

    }
}
