using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using badpaybad.Scraper.DTO;
using badpaybad.Scraper.Services;
using badpaybad.Scraper.Utils;

namespace ProjectTest
{
    /// <summary>
    /// By Nguyen Phan Du
    /// badpaybad@gmail.com
    /// http://badpaybad.info
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private readonly IScraperManager _scraperManager = new ScraperManager();
        #region scraper manager event
        private void OnRefreshInterval(IScraperManager obj)
        {
            if (chkScrapers.InvokeRequired)
            {
                chkScrapers.BeginInvoke(
                    new MethodInvoker(() =>
                    {
                        InvokeCheckListBox();
                    }));
            }
            else
            {
                InvokeCheckListBox();
            }
        }

        private void OnAdded(IScraper obj)
        {
            if (chkScrapers.InvokeRequired)
            {
                chkScrapers.BeginInvoke(
                    new MethodInvoker(() =>
                    {
                        InvokeCheckListBox();
                    }));
            }
            else
            {
                InvokeCheckListBox();
            }
        }

        private void OnStoped(IScraper obj)
        {

        }

        private void OnStarted(IScraper obj)
        {

        }

        private void OnPushReport(Reports obj)
        {
            if (txtReports.InvokeRequired)
            {
                txtReports.BeginInvoke(new MethodInvoker(() =>
                    {
                        txtReports.Text = obj.ToString();
                    }));
            }
            else
            {
                txtReports.Text = obj.ToString();
            }

            //if (chkScrapers.InvokeRequired)
            //{
            //    chkScrapers.BeginInvoke(
            //        new MethodInvoker(() =>
            //        {
            //            InvokeCheckListBox();
            //        }));
            //}
            //else
            //{
            //    InvokeCheckListBox();
            //}
        }

        private void OnCurrentParseUrl(string obj)
        {

            if (txtCurrentUrl.InvokeRequired)
            {
                txtCurrentUrl.BeginInvoke(new MethodInvoker(() =>
                {
                    txtCurrentUrl.Text = obj.ToString();
                }));
            }
            else
            {
                txtCurrentUrl.Text = obj.ToString();
            }
        }

        void InvokeCheckListBox()
        {
            var theNew = _scraperManager.GetAll();
            var theCurrent = new List<IScraper>();
            var removes = new List<int>();
            var adds = new List<IScraper>();

            for (int i = 0; i < chkScrapers.Items.Count; i++)
            {
                var sc = (IScraper)chkScrapers.Items[i];
                if (theNew.Count(k => k.Id == sc.Id) <= 0)
                {
                    removes.Add(i);
                }
                theCurrent.Add(sc);
            }

            foreach (IScraper sc in theNew)
            {
                if (theCurrent.Count(i => i.Id == sc.Id) <= 0)
                {
                    adds.Add(sc);
                }
            }

            foreach (int rind in removes)
            {
                chkScrapers.Items.RemoveAt(rind);
            }
            foreach (IScraper scraper in adds)
            {
                chkScrapers.Items.Add(scraper);
            }
        }
        #endregion
        #region form action
        private void Form1_Load(object sender, EventArgs e)
        {
            var defaultMaping = Config.DefaultMaping();
            foreach (var m in defaultMaping)
            {
                chkMaps.Items.Add(m);
            }
            _scraperManager.ParseCurrentUrl += OnCurrentParseUrl;
            _scraperManager.PushReport += OnPushReport;
            _scraperManager.Started += OnStarted;
            _scraperManager.Stoped += OnStoped;
            _scraperManager.Added += OnAdded;
            _scraperManager.RefreshInterval += OnRefreshInterval;
            _scraperManager.Init();
        }

        private void btnTestExt_Click(object sender, EventArgs e)
        {
            var x = Files.GetFileExtension(txtCurrentUrl.Text, "");
            MessageBox.Show(x);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var scraper = new ScraperCommon();
            var needDefault = chkMaps.Items.Count == 0;
            var config = new Config(needDefault);
            config.PostAndSaveDataToUrl = txtResultPostToUrl.Text;
            config.InitLink = new Link();
            config.InitLink.Uri = txtInitUrl.Text;
            config.InitLink.Deep = 0;
            config.Deep = (int)numDeep.Value;
            config.IncludeExtensions = txtInitExtension.Text.Trim().Split(new[] { ';', ',', '\n' }).Select(i => i.Trim(new[] { ';', ',', '\n', '\r', ' ' })).ToList();
            if (!needDefault)
            {
                config.Maps = config.Maps ?? new List<Maping>();
                config.Maps.Clear();
                foreach (Maping item in chkMaps.Items)
                {
                    config.Maps.Add(item);
                }
            }

            _scraperManager.Add(scraper, config);
        }

        private void btnAddAndStart_Click(object sender, EventArgs e)
        {
            var scraper = new ScraperCommon();
            var needDefault = chkMaps.Items.Count == 0;
            var config = new Config(needDefault);
            config.PostAndSaveDataToUrl = txtResultPostToUrl.Text;
            config.InitLink = new Link();
            config.InitLink.Uri = txtInitUrl.Text;
            config.InitLink.Deep = 0;
            config.Deep = (int)numDeep.Value;
            config.IncludeExtensions = txtInitExtension.Text.Trim().Split(new[] { ';', ',', '\n' }).Select(i => i.Trim(new[] { ';', ',', '\n', '\r', ' ' })).ToList();
            if (!needDefault)
            {
                config.Maps = config.Maps ?? new List<Maping>();
              
                config.Maps.Clear();
                foreach (Maping item in chkMaps.Items)
                {
                    config.Maps.Add(item);
                }
            }

            _scraperManager.Add(scraper, config);
            _scraperManager.Start(scraper.Id);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            var itms = new List<IScraper>();
            foreach (IScraper selectedItem in chkScrapers.CheckedItems)
            {
                itms.Add(selectedItem);
            }
            foreach (IScraper selectedItem in chkScrapers.SelectedItems)
            {
                itms.Add(selectedItem);
            }
            var temp = itms.Select(i => (IScraper)i).ToList();
            foreach (var scraper in temp)
            {
                _scraperManager.Start(scraper.Id);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {

            var itms = new List<IScraper>();
            foreach (IScraper selectedItem in chkScrapers.CheckedItems)
            {
                itms.Add(selectedItem);
            }
            foreach (var scraper in itms)
            {
                _scraperManager.Stop(scraper.Id);
            }
        }

        private void btnStartAll_Click(object sender, EventArgs e)
        {
            _scraperManager.StartAll();
        }

        private void btnStopAll_Click(object sender, EventArgs e)
        {
            _scraperManager.StopAll();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all checked?", "Remove all checked?", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                return;
            }
            var itms = new List<IScraper>();
            foreach (IScraper selectedItem in chkScrapers.CheckedItems)
            {
                itms.Add(selectedItem);
            }

            foreach (var scraper in itms)
            {
                _scraperManager.Remove(scraper.Id);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _scraperManager.CleanBeforeDispose();
        }
        #endregion
        #region using for maping
        private void btnMapAdd_Click(object sender, EventArgs e)
        {
            frmMapInfo frm = new frmMapInfo();
            frm.MapInfo += OnGetMapinfo;
            frm.ShowDialog();

        }

        private void OnGetMapinfo(Maping obj)
        {
            if (chkMaps.InvokeRequired)
            {
                chkMaps.BeginInvoke(new MethodInvoker(() =>
                    {
                        chkMaps.Items.Add(obj);
                    }));
            }
            else
            {
                chkMaps.Items.Add(obj);
            }
        }

        private void btnAddRemove_Click(object sender, EventArgs e)
        {
            if (chkMaps.InvokeRequired)
            {
                chkMaps.BeginInvoke(new MethodInvoker(() =>
                    {
                        RemoveCheckedMaping();
                    }));
            }
            else
            {
                RemoveCheckedMaping();
            }
        }
        void RemoveCheckedMaping()
        {
            foreach (var itm in chkMaps.CheckedItems)
            {
                chkMaps.Items.Remove(itm);
            }
        }

        #endregion

        private void btnMapView_Click(object sender, EventArgs e)
        {
            Maping info=null;
            if (chkMaps.SelectedItem != null)
            {
                info = chkMaps.SelectedItem as Maping;
            }
            else
            {
                if (chkMaps.CheckedItems.Count > 0)
                    info = chkMaps.CheckedItems[0] as Maping;
            }
            if(info!=null)
            {
                string msg =
                    string.Format(
                        "FieldInDb: {0}\r\nContentByIdOrName: {1}\r\nConentByTagName: {2}\r\nContentByClassName: {3}\r\nContentByIndex: {4}"
                        , info.FieldInDb,info.ElementById,info.ElementByTagName,info.ElementByClassName, info.ElementByIndex);
                MessageBox.Show(msg);
            }
        }
    }
}
