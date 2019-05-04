using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WeixinRoboot
{
    public partial class WebWeChatImageSetting : Form
    {
        public WebWeChatImageSetting()
        {
            InitializeComponent();
        }

        private void WebWeChatImageSetting_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var data = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                     && t.WX_SourceType == WX_SourceType
                     && t.WX_UserName == WX_UserName
                     );
                if (data != null)
                {
                    data.ballinterval = Convert.ToInt32(tb_ballinterval.Text);
                    data.footballPIC = cb_footballPIC.Checked;
                    data.bassketballpic = cb_bassketballpic.Checked;
                    data.balluclink = cb_balluclink.Checked;

                    data.card = cb_card.Checked;
                    data.cardname = tb_cardname.Text;
                    data.shishicailink = cb_shishicailink.Checked;
                    data.NumberPIC = cb_NumberPIC.Checked;
                    data.dragonpic = cb_dragonpic.Checked;
                    data.numericlink = cb_numericlink.Checked;
                    data.dragonlink = cb_dragonlink.Checked;
                    data.NumberAndDragonPIC = cb_numericanddragon.Checked;

                    data.ballstart = cb_ballstart.Checked;
                    data.ballend = cb_ballend.Checked;

                    data.balllivepoint = cb_balllivepoint.Checked;
                    data.HKSixResult = cb_SixResult.Checked;


                    data.NumberDragonTxt = cb_NumberDragonTxt.Checked;
                    data.NiuNiuPic = cb_NiuNiuPic.Checked;
                    data.NoBigSmallSingleDoublePIC = cb_NoBigSmallSingleDoublePIC.Checked;

                    data.IsSendPIC = cb_IsSendPIC.Checked;

                    data.PIC_StartHour = Convert.ToInt32( tb_StartHour.Text);
                    data.PIC_StartMinute = Convert.ToInt32( tb_StartMinute.Text);
                    data.PIC_EndHour =  Convert.ToInt32(tb_EndHour.Text);
                    data.Pic_EndMinute =  Convert.ToInt32(tb_EndMinute.Text);
                    db.SubmitChanges();
                }
                else
                {
                    Linq.WX_WebSendPICSetting newd = new Linq.WX_WebSendPICSetting();

                    newd.aspnet_UserID = GlobalParam.UserKey;
                    newd.WX_UserName = WX_UserName;
                    newd.WX_SourceType = WX_SourceType;


                    newd.ballinterval = Convert.ToInt32(tb_ballinterval.Text);
                    newd.footballPIC = cb_footballPIC.Checked;
                    newd.bassketballpic = cb_bassketballpic.Checked;
                    newd.balluclink = cb_balluclink.Checked;

                    newd.card = cb_card.Checked;
                    newd.cardname = tb_cardname.Text;
                    newd.shishicailink = cb_shishicailink.Checked;
                    newd.NumberPIC = cb_NumberPIC.Checked;
                    newd.dragonpic = cb_dragonpic.Checked;
                    newd.numericlink = cb_numericlink.Checked;
                    newd.dragonlink = cb_dragonlink.Checked;
                    newd.balllivepoint = cb_balllivepoint.Checked;

                    newd.NumberAndDragonPIC = cb_numericanddragon.Checked;

                    newd.ballstart = cb_ballstart.Checked;
                    newd.ballend = cb_ballend.Checked;
                    newd.HKSixResult = cb_SixResult.Checked;


                    newd.NumberDragonTxt = cb_NumberDragonTxt.Checked;
                    newd.NiuNiuPic = cb_NiuNiuPic.Checked;
                    newd.NoBigSmallSingleDoublePIC = cb_NoBigSmallSingleDoublePIC.Checked;

                    newd.IsSendPIC = cb_IsSendPIC.Checked;

                    newd.PIC_StartHour = Convert.ToInt32(tb_StartHour.Text);
                    newd.PIC_StartMinute = Convert.ToInt32(tb_StartMinute.Text);
                    newd.PIC_EndHour = Convert.ToInt32(tb_EndHour.Text);
                    newd.Pic_EndMinute = Convert.ToInt32(tb_EndMinute.Text);

                    db.WX_WebSendPICSetting.InsertOnSubmit(newd);
                    db.SubmitChanges();
                }


                foreach (var item in subsource)
                {
                    var datasub = db.WX_WebSendPICSettingMatchClass.SingleOrDefault(t =>
                        t.aspnet_UserID == GlobalParam.UserKey
                      && t.WX_SourceType == WX_SourceType
                      && t.WX_UserName == WX_UserName
                      && t.MatchBallType == item.MatchBallType
                      && t.MatchClassName == item.MatchClassName
                      );
                    if (datasub != null)
                    {
                        datasub.SendAny = item.SendAny;
                        db.SubmitChanges();
                    }
                    else
                    {
                        Linq.WX_WebSendPICSettingMatchClass newsub = new Linq.WX_WebSendPICSettingMatchClass();

                        newsub.SendAny = item.SendAny;

                        newsub.aspnet_UserID = GlobalParam.UserKey;
                        newsub.WX_SourceType = WX_SourceType;
                        newsub.WX_UserName = WX_UserName;
                        newsub.MatchBallType = item.MatchBallType;
                        newsub.MatchClassName = item.MatchClassName;


                        db.WX_WebSendPICSettingMatchClass.InsertOnSubmit(newsub);
                        db.SubmitChanges();

                    }

                }

                MessageBox.Show("保存成功");
            }
            catch (Exception anyerror)
            {

                MessageBox.Show("保存失败" + anyerror.Message);
            }
        }


        List<Linq.WX_WebSendPICSettingMatchClass> subsource = new List<Linq.WX_WebSendPICSettingMatchClass>();

        public string WX_UserName
        {
            get
            {
                return _WX_UserName;
            }
            set
            {
                _WX_UserName = value;
                if (WX_SourceType == "" || WX_SourceType == null || _WX_UserName == "" || _WX_UserName == null)
                {
                    return;
                }
                Linq.dbDataContext db = new Linq.dbDataContext(System.Configuration.ConfigurationManager.ConnectionStrings[GlobalParam.DataSourceName].ConnectionString);
                db.ExecuteCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
                var data = db.WX_WebSendPICSetting.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                     && t.WX_SourceType == WX_SourceType
                     && t.WX_UserName == value
                     );
                if (data != null)
                {
                    tb_ballinterval.Text = data.ballinterval.ToString();
                    cb_footballPIC.Checked = data.footballPIC.Value;
                    cb_bassketballpic.Checked = data.bassketballpic.Value;
                    cb_balluclink.Checked = data.balluclink.Value;

                    cb_card.Checked = data.card.Value;
                    tb_cardname.Text = data.ballinterval.ToString();
                    cb_shishicailink.Checked = data.shishicailink.Value;
                    cb_NumberPIC.Checked = data.NumberPIC.Value;
                    cb_dragonpic.Checked = data.dragonpic.Value;
                    cb_numericlink.Checked = data.numericlink.Value;
                    cb_dragonlink.Checked = data.dragonlink.Value;


                    cb_ballstart.Checked = data.ballstart.HasValue ? data.ballstart.Value : false;
                    cb_ballend.Checked = data.ballend.HasValue ? data.ballend.Value : false;

                    cb_balllivepoint.Checked = data.balllivepoint.HasValue ? data.balllivepoint.Value : false; ;

                    cb_SixResult.Checked = data.HKSixResult.HasValue ? data.HKSixResult.Value : false;
                    cb_numericanddragon.Checked = data.NumberAndDragonPIC.HasValue ? data.NumberAndDragonPIC.Value : false;



                    cb_NumberDragonTxt.Checked = data.NumberDragonTxt.HasValue ? data.NumberDragonTxt.Value : false;
                    cb_NiuNiuPic.Checked = data.NiuNiuPic.HasValue ? data.NiuNiuPic.Value : false;
                    cb_NoBigSmallSingleDoublePIC.Checked = data.NoBigSmallSingleDoublePIC.HasValue ? data.NoBigSmallSingleDoublePIC.Value : false;

                    cb_IsSendPIC.Checked = data.IsSendPIC.HasValue ? data.IsSendPIC.Value : false;

                    tb_StartHour.Text =data.PIC_StartHour.HasValue?data.PIC_StartHour.ToString():"8";
                    tb_StartMinute.Text = data.PIC_StartMinute.HasValue?data.PIC_StartMinute.ToString():"8";
                    tb_EndHour.Text = data.PIC_EndHour.HasValue ? data.PIC_EndHour.ToString() : "8";
                    tb_EndMinute.Text = data.Pic_EndMinute.HasValue ? data.Pic_EndMinute.ToString() : "8";

                    if (data.PIC_StartHour.HasValue==false)
                    {
                        data.PIC_StartHour = 6;
                    }
                    if (data.PIC_StartMinute.HasValue == false)
                    {
                        data.PIC_StartMinute = 0;
                    }
                    if (data.PIC_EndHour.HasValue == false)
                    {
                        data.PIC_EndHour = 3;
                    }
                    if (data.Pic_EndMinute.HasValue == false)
                    {
                        data.Pic_EndMinute = 0;
                    }
                    db.SubmitChanges();
                }
                else
                {
                    tb_ballinterval.Text = "120";
                }


                var source = db.Game_FootBall_VS.Where(t => t.aspnet_UserID == GlobalParam.UserKey
                    //&&   (t.LastAliveTime==null||t.LastAliveTime>=DateTime.Today.AddDays(-3))
                     && t.Jobid == GlobalParam.JobID
                    );
                foreach (var item in source)
                {

                    Linq.WX_WebSendPICSettingMatchClass subset = db.WX_WebSendPICSettingMatchClass.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                       && t.WX_SourceType == WX_SourceType
                       && t.WX_UserName == value
                       && t.MatchBallType == item.GameType
                       && t.MatchClassName == item.MatchClass

                       );
                    if (subset == null)
                    {
                        subset = new Linq.WX_WebSendPICSettingMatchClass();
                        subset.aspnet_UserID = GlobalParam.UserKey;
                        subset.WX_SourceType = WX_SourceType;
                        subset.WX_UserName = value;
                        subset.MatchBallType = item.GameType;
                        subset.MatchClassName = item.MatchClass;

                        subset.SendAny = false;

                        db.WX_WebSendPICSettingMatchClass.InsertOnSubmit(subset);
                        db.SubmitChanges();

                    }
                    subsource.Add(subset);
                    bs_matchclass.DataSource = subsource;

                }




            }
        }
        private string _WX_UserName = "";

        public string WX_SourceType { get; set; }

        string Mode = "ALL";
        private void btn_reflect_Click(object sender, EventArgs e)
        {
            if (Mode == "ALL")
            {


                foreach (var item in subsource)
                {
                    item.SendAny = true;
                }
                Mode = "ALLNOT";
            }
            else
            {
                foreach (var item in subsource)
                {
                    item.SendAny = false;
                }
                Mode = "ALL";
            }
            this.Refresh();
        }

    }
}
