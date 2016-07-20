using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using Visa.Resources;
using Visa.WebCrawler.SeleniumCrawler;

// ReSharper disable once CheckNamespace
namespace Visa.WinForms
{
    public partial class MainForm
    {
        #region Members

        private GetFirtAvailableData _crawler;

        /// <summary>
        /// Thread for crawler logic
        /// </summary>
        private BackgroundWorker _crawlerWorker;

        #endregion Members

        #region Events

        private void buttonShow_Click(object sender, EventArgs e)
        {
            _logger.Trace("Start buttonShow_Click");
            if (ValidateControlsFirst())
            {
                _logger.Info("Validation Pass.");
                StartNewWorkRoundFirst();
            }
            else
            {
                _logger.Warn("Validation Failed. Error Message is shown.");
                XtraMessageBox.Show(ResManager.GetString(ResKeys.ValidationError_Message_FirstPart),
                    ResManager.GetString(ResKeys.ValidationError_Title),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
            }
            _logger.Trace($"End buttonShow_Click. buttonShow.Enabled = {buttonShow.Enabled}.  buttonRegistry.Enabled = { buttonRegistry.Enabled }");
        }

        #endregion

        #region Functions

        private void CrawlerWorkFirstPart(object data)
        {
            _logger.Trace("Start CrawlerWorkFirstPart");
            var incomingData = data as Tuple<int, int>;

            if (incomingData == null)
            {
                _logger.Error("return _crawlerWorker_DoWork incomingData == null");
                return;
            }

            switch (_state)
            {
                case 1:
                    Invoke(new Action(() =>
                    {
                        _alertControl.AlertFormList.ForEach(alert => alert.Close());
                    }));
                    _crawler = new GetFirtAvailableData();
                    _crawler.PartOne();
                    _state = 2;
                    break;

                case 2:
                    _crawler.PartTwo(incomingData.Item1, incomingData.Item2);
                    _state = 3;
                    break;

                case 3:
                    _crawler.PartThree();
                    if (!_crawler.Error)
                    {
                        _logger.Info($"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawler.OutData}. _crawler.Error = false ");
                        XtraMessageBox.Show(_crawler.OutData,
                            ResManager.GetString(ResKeys.SearchResult),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Exclamation);
                        CloseBrowsers(false);
                        return;
                    }
                    break;
                default:
                    _logger.Error($"incorrect State = {_state} for CrawlerWorkFirstPart");
                    break;
            }
            if (_crawler?.Canceled ?? false)
            {
                SetDefaultState();
                CloseBrowsers(true);
                // ReSharper disable once PossibleNullReferenceException
                _crawler.Canceled = false;
            }
            else if (_crawler?.Error ?? false)
            {
                _logger.Warn($"return _crawlerWorker_DoWork. State = {_state}. OutData = {_crawler?.OutData}. _crawler.Error = true ");
                ShowAlert(ResManager.GetString(ResKeys.ServerError));
                Thread.Sleep(1000);
                SetDefaultState();
                CloseBrowsers(false);
            }
            else
                ShowAlert(ResManager.GetString(ResKeys.FillCaptchaAndPress));

            _logger.Trace("End CrawlerWorkFirstPart");
        }

        private bool ValidateControlsFirst()
        {
            var bRes = true;

            bRes &= lookUpEditServiceCenter.GetSelectedDataRow() != null;
            bRes &= lookUpEditVisaCategory.GetSelectedDataRow() != null;

            _logger.Info($"ValidateControlsFirst = {bRes}");
            return bRes;
        }

        private void StartNewWorkRoundFirst()
        {
            _logger.Trace("Start StartNewWorkRoundFirst");

            isFirstPart = true;
            StartNewWorkRoundBase();

            _logger.Trace("End StartNewWorkRoundFirst");
        }




        #endregion
    }
}
