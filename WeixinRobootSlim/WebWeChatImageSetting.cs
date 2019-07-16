using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeixinRobotLib.Entity.Linq;
using Newtonsoft.Json;
namespace WeixinRobootSlim
{
    public partial class WebWeChatImageSetting : Form
    {
        public WebWeChatImageSetting()
        {
            InitializeComponent();
        }
        public RunnerForm RunnerF = null;
        private void WebWeChatImageSetting_Load(object sender, EventArgs e)
        {

        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            try
            {
                WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
                var data = WeixinRobootSlim.Linq.Util_Services.GetWebSendPicSetting( GlobalParam.UserKey
                    ,WX_SourceType
                    , WX_UserName
                     );
              
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

                    data.PIC_StartHour = Convert.ToInt32(tb_StartHour.Text);
                    data.PIC_StartMinute = Convert.ToInt32(tb_StartMinute.Text);
                    data.PIC_EndHour = Convert.ToInt32(tb_EndHour.Text);
                    data.Pic_EndMinute = Convert.ToInt32(tb_EndMinute.Text);
                    WeixinRobootSlim.Linq.Util_Services.SaveWebSendPicSetting(data);
               
                DataRow[] list = RunnerF.MemberSource.Select("User_ContactID='"+WX_UserName+"'");
                foreach (var rowitem in list)
                {
                    rowitem.SetField<Boolean?>("User_IsSendPic", cb_IsSendPIC.Checked);
                }

                ws.WX_WebSendPICSettingMatchClassSave(JsonConvert.SerializeObject( subsource),GlobalParam.GetUserParam(),WX_SourceType,_WX_UserName);

                MessageBox.Show("保存成功");
            }
            catch (Exception anyerror)
            {

                MessageBox.Show("保存失败" + anyerror.Message);
            }
        }


        List<WX_WebSendPICSettingMatchClass> subsource = new List<WX_WebSendPICSettingMatchClass>();

        public string WX_UserName
        {
            get
            {
                return _WX_UserName;
            }
            set
            {
                _WX_UserName = value;
                WeixinRoboot.RobootWeb.WebService ws = new WeixinRoboot.RobootWeb.WebService();
                if (WX_SourceType == "" || WX_SourceType == null || _WX_UserName == "" || _WX_UserName == null)
                {
                    return;
                }

                var data = WeixinRobootSlim.Linq.Util_Services.GetWebSendPicSetting( GlobalParam.UserKey
                     ,WX_SourceType
                     , value
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

                    tb_StartHour.Text = data.PIC_StartHour.HasValue ? data.PIC_StartHour.ToString() : "8";
                    tb_StartMinute.Text = data.PIC_StartMinute.HasValue ? data.PIC_StartMinute.ToString() : "58";
                    tb_EndHour.Text = data.PIC_EndHour.HasValue ? data.PIC_EndHour.ToString() : "2";
                    tb_EndMinute.Text = data.Pic_EndMinute.HasValue ? data.Pic_EndMinute.ToString() : "3";

                    if (data.PIC_StartHour.HasValue == false)
                    {
                        data.PIC_StartHour = 8;
                    }
                    if (data.PIC_StartMinute.HasValue == false)
                    {
                        data.PIC_StartMinute = 58;
                    }
                    if (data.PIC_EndHour.HasValue == false)
                    {
                        data.PIC_EndHour = 2;
                    }
                    if (data.Pic_EndMinute.HasValue == false)
                    {
                        data.Pic_EndMinute = 3;
                    }
                    WeixinRobootSlim.Linq.Util_Services.SaveWebSendPicSetting(data);
                }
                else
                {
                    tb_ballinterval.Text = "120";
                }


                var source = JsonConvert.DeserializeObject<Game_FootBall_VS[]>(ws.Game_FootBall_VS_Where(GlobalParam.UserKey
                    //&&   (t.LastAliveTime==null||t.LastAliveTime>=DateTime.Today.AddDays(-3))
                    , GlobalParam.JobID
                    ));
                foreach (var item in source)
                {

                    WX_WebSendPICSettingMatchClass subset = ws.WX_WebSendPICSettingMatchClass_SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                       && t.WX_SourceType == WX_SourceType
                       && t.WX_UserName == value
                       && t.MatchBallType == item.GameType
                       && t.MatchClassName == item.MatchClass

                       );
                    if (subset == null)
                    {
                        subset = new WX_WebSendPICSettingMatchClass();
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
