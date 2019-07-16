using System;
using Accessibility;
using System.Xml;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
namespace WeixinRobootSlim
{
    /// <summary>
    /// 对Messenger窗口进行操作
    /// </summary>
    public class QqWindowHelper
    {
        IntPtr _QqWindowHandle;

        object _messageBox;
        IAccessible _messageBoxParent;


        IAccessible _sendBoxParent;

        object _sendBox;

        public string WindowName { get; set; }


        public System.Windows.Forms.Form MainUI { get; set; }

        public QqWindowHelper(IntPtr windowHandle, String winTitle, Boolean FindTtile = true)
        {
            _QqWindowHandle = windowHandle;

            StringBuilder RAW = new StringBuilder(512);
            NetFramework.WindowsApi.GetWindowText(windowHandle, RAW, 512);

            WindowName = RAW.ToString();
            WindowName = WindowName.Replace("智能发图", "");


            XmlDocument doc = null;
            if (FindTtile)
            {

                StartGetAccessibleObjects(_QqWindowHandle, out _messageBox, out _messageBoxParent, winTitle, out  doc);

                StartGetAccessibleObjects(_QqWindowHandle, out _sendBox, out _sendBoxParent, "输入", out  doc);

            }
        }

        /// <summary>
        /// 返回消息框内容
        /// </summary>
        /// <returns></returns>
        public string GetContent()
        {



            string value = "";
            if (_messageBox != null)
            {


                lock (GlobalParam.KeyBoardLocking)
                {




                    NetFramework.WindowsApi.ShowWindow(_QqWindowHandle, 1);

                    NetFramework.WindowsApi.SwitchToThisWindow(_QqWindowHandle, true);
                    NetFramework.WindowsApi.SetForegroundWindow(_QqWindowHandle);

                    System.Threading.Thread.Sleep(10);
                    ((IAccessible)_messageBox).accSelect(0x1, Win32.CHILDID_SELF);



                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_A, 0, 0, 0);
                    System.Threading.Thread.Sleep(10);

                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_A, 0, 2, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);
                    System.Threading.Thread.Sleep(10);

                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 0, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_C, 0, 0, 0);
                    System.Threading.Thread.Sleep(10);

                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_C, 0, 2, 0);
                    NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_CONTROL, 0, 2, 0);
                    System.Threading.Thread.Sleep(10);

                    if (MainUI != null)
                    {
                        MainUI.Invoke(new Action(() =>
                        {
                            value = System.Windows.Forms.Clipboard.GetText();

                            string Minutes = DateTime.Now.Minute.ToString();
                            Minutes = Minutes.Substring(Minutes.Length - 1);
                            if (Minutes == "3" || Minutes=="8")
                            {
                                //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_ALT, 0, 0, 0);
                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_F, 0, 0, 0);
                                System.Threading.Thread.Sleep(10);

                                NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_F, 0, 2, 0);
                               //NetFramework.WindowsApi.keybd_event(NetFramework.WindowsApi.VK_ALT, 0, 2, 0);
                                System.Threading.Thread.Sleep(10);
                            }

                        }));
                    }


                    if (_sendBox.GetType().ToString().ToUpper().Contains("INT") == false)
                    {
                        ((IAccessible)_sendBox).accSelect(0x1, Win32.CHILDID_SELF);
                    }






                }



                return value;

            }

            return value;
        }

        private object[] GetAccessibleChildren(IAccessible paccContainer)
        {
            if (paccContainer == null)
            {
                return new object[] { };
            }
            object[] rgvarChildren = new object[paccContainer.accChildCount];
            int pcObtained;
            Win32.AccessibleChildren(paccContainer, 0, paccContainer.accChildCount, rgvarChildren, out pcObtained);
            return rgvarChildren;
        }


        private void StartGetAccessibleObjects(System.IntPtr imWindowHwnd, out object FindBox, out IAccessible FindBoxParent, string SearchTile, out XmlDocument doc)
        {
            Guid guidCOM = new Guid(0x618736E0, 0x3C3D, 0x11CF, 0x81, 0xC, 0x0, 0xAA, 0x0, 0x38, 0x9B, 0x71);
            Accessibility.IAccessible IACurrent = null;
            doc = new XmlDocument();
            FindBox = null;
            FindBoxParent = IACurrent;
            Win32.AccessibleObjectFromWindow(imWindowHwnd, (int)Win32.OBJID_CLIENT, ref guidCOM, ref IACurrent);
            if (IACurrent == null)
            {
                return;
            }
            IACurrent = (IAccessible)IACurrent.accParent;
            FindBox = null;




            object[] CHILDS = GetAccessibleChildren(IACurrent);

            doc.LoadXml("<top></top>");
            Int32 index = 1;
            foreach (var CHILDITEM in CHILDS)
            {
                XmlElement subwin = doc.CreateElement("win");
                doc.DocumentElement.AppendChild(subwin);

                subwin.SetAttribute("index", index.ToString());


                if (CHILDITEM.GetType().ToString().ToUpper().Contains("INT"))
                {
                    subwin.SetAttribute("name", ((IAccessible)IACurrent).get_accName(CHILDITEM));
                    subwin.SetAttribute("value", ((IAccessible)IACurrent).get_accValue(CHILDITEM), SearchTile);
                    if (((IAccessible)IACurrent).get_accName(CHILDITEM) == SearchTile)
                    {
                        FindBox = (CHILDITEM);
                        FindBoxParent = IACurrent;
                        return;

                    }
                }
                else
                {

                    subwin.SetAttribute("name", ((IAccessible)CHILDITEM).get_accName(Win32.CHILDID_SELF));
                    subwin.SetAttribute("value", ((IAccessible)CHILDITEM).get_accValue(Win32.CHILDID_SELF), SearchTile);
                    if (((IAccessible)CHILDITEM).get_accName(Win32.CHILDID_SELF) == SearchTile)
                    {
                        FindBox = ((IAccessible)CHILDITEM);
                        FindBoxParent = IACurrent;
                        return;

                    }
                }





                index += 1;
                if (FindBox == null)
                {
                    RepeatGetAccessibleObjects(((IAccessible)CHILDITEM), 1, subwin, SearchTile, doc, ref FindBox, ref FindBoxParent);
                }
            }

            //int childCount = IACurrent.accChildCount;
            //object[] windowChildren = new object[childCount];
            //int pcObtained;
            //Win32.AccessibleChildren(IACurrent, 0, childCount, windowChildren, out pcObtained);

            //foreach (IAccessible child in windowChildren)
            //{
            //    if (child.get_accName(Win32.CHILDID_SELF) == _winTitle)
            //    {
            //        inputBox = GetAccessibleChild(child, new int[] { 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 3, 0 });

            //        break;
            //    }
            //}
        }

        private void RepeatGetAccessibleObjects(IAccessible suparent, Int32 LEVEL, XmlElement subparentnode, string SearchTile, XmlDocument doc, ref object FindBox, ref IAccessible FindBoxParent)
        {
            object[] CHILDS = GetAccessibleChildren(suparent);
            subparentnode.SetAttribute("childs", CHILDS.Length.ToString());
            Int32 SubLevel = LEVEL + 1;

            if (LEVEL > 30)
            {

                return;
            }
            Int32 index = 1;
            foreach (var CHILDITEM in CHILDS)
            {

                XmlElement subwin = doc.CreateElement("win");
                subparentnode.AppendChild(subwin);



                subwin.SetAttribute("index", index.ToString());
                try
                {




                    if (CHILDITEM.GetType().ToString().ToUpper().Contains("INT"))
                    {
                        subwin.SetAttribute("name", ((IAccessible)suparent).get_accName(CHILDITEM));
                        subwin.SetAttribute("value", ((IAccessible)suparent).get_accValue(CHILDITEM));
                        if (((IAccessible)suparent).get_accName(CHILDITEM) == SearchTile)
                        {
                            FindBox = (CHILDITEM);
                            FindBoxParent = suparent;
                            return;

                        }

                    }
                    else
                    {
                        subwin.SetAttribute("name", ((IAccessible)CHILDITEM).get_accName(Win32.CHILDID_SELF));
                        if (((IAccessible)CHILDITEM).get_accName(Win32.CHILDID_SELF) == SearchTile)
                        {
                            FindBox = ((IAccessible)CHILDITEM);
                            FindBoxParent = suparent;
                            return;

                        }
                        subwin.SetAttribute("value", ((IAccessible)CHILDITEM).get_accValue(Win32.CHILDID_SELF));

                    }
                }
                catch (Exception)
                {


                }

                index += 1;
                if (FindBox == null && CHILDITEM.GetType().ToString().ToUpper().Contains("INT") == false)
                {
                    RepeatGetAccessibleObjects(((IAccessible)CHILDITEM), SubLevel, subwin, SearchTile, doc, ref FindBox, ref FindBoxParent);
                }


            }
        }


        object FindOwner;
        IAccessible FindOwnerParent;
        public void ReloadMembers(string GroupOwnerName, DataTable ToJoinIn, string WX_SourceType,  IntPtr hwnd)
        {
            XmlDocument doc = new XmlDocument();
            StartGetAccessibleObjects(_QqWindowHandle, out FindOwner, out FindOwnerParent, GroupOwnerName, out doc);

            object[] CHILDS = GetAccessibleChildren(FindOwnerParent);

            WeixinRobotLib.Entity.Linq.WX_PCSendPicSetting pcset = db.WX_PCSendPicSetting.SingleOrDefault(t => t.WX_UserTMPID == hwnd.ToString());

            foreach (var CHILDITEM in CHILDS)
            {
                if (CHILDITEM.GetType().ToString().ToUpper().Contains("INT") == false)
                {
                    continue;
                }
                string NewName = ((IAccessible)FindOwnerParent).get_accName(CHILDITEM);

                DataRow[] testexit = ToJoinIn.Select(
                   "User_ContactID= '" + NewName.Replace("'", "''") + "' and User_SourceType = '" + WX_SourceType + "'"
                    );
                if (testexit.Length == 0)
                {

                    WeixinRobotLib.Entity.Linq.WX_UserReply userreply = db.WX_UserReply.SingleOrDefault(t => t.aspnet_UserID == GlobalParam.UserKey
                                                                                               && t.WX_UserName == NewName
                                                                                               && t.WX_SourceType == WX_SourceType
                                                                                               );
                    DataRow usr = ToJoinIn.AsEnumerable().SingleOrDefault
                             (t => t.Field<object>("User_ContactID").ToString() == NewName
                             && t.Field<object>("User_SourceType").ToString() == WX_SourceType
                             );


                    if (userreply == null)
                    {
                        WeixinRobotLib.Entity.Linq.WX_UserReply newr = new WeixinRobotLib.Entity.Linq.WX_UserReply();
                        newr.aspnet_UserID = GlobalParam.UserKey;
                        newr.WX_SourceType = WX_SourceType;
                        newr.WX_UserName = NewName;
                        newr.IsCaculateFuli = true;
                        db.WX_UserReply.InsertOnSubmit(newr);
                        db.SubmitChanges();


                    }
                    if (usr == null && userreply == null)
                    {
                        DataRow newset = ToJoinIn.NewRow();
                        newset.SetField("User_ContactID", NewName);
                        newset.SetField("User_ContactTEMPID", hwnd.ToString());
                        newset.SetField("User_SourceType", WX_SourceType);
                        newset.SetField("User_Contact", NewName);

                        newset.SetField("User_ChongqingMode", pcset.ChongqingMode);
                        newset.SetField("User_FiveMinuteMode", pcset.FiveMinuteMode);
                        newset.SetField("User_HkMode", pcset.HkMode);
                        newset.SetField("User_AozcMode", pcset.AozcMode);



                        ToJoinIn.Rows.Add(newset);
                        usr = newset;
                    }
                    else if (usr == null && userreply != null)
                    {
                        DataRow newset = ToJoinIn.NewRow();
                        newset.SetField("User_ContactID", userreply.WX_UserName);
                        newset.SetField("User_ContactTEMPID", hwnd.ToString());
                        newset.SetField("User_SourceType", userreply.WX_SourceType);
                        newset.SetField("User_Contact", userreply.WX_UserName);

                        newset.SetField("User_IsAdmin", userreply.IsAdmin);
                        newset.SetField("User_IsCaculateFuli", userreply.IsCaculateFuli);

                        newset.SetField("User_ChongqingMode", pcset.ChongqingMode);
                        newset.SetField("User_FiveMinuteMode", pcset.FiveMinuteMode);
                        newset.SetField("User_HkMode", pcset.HkMode);
                        newset.SetField("User_AozcMode", pcset.AozcMode);


                        ToJoinIn.Rows.Add(newset);
                        usr = newset;
                    }




                }


            }


        }

    }
}
