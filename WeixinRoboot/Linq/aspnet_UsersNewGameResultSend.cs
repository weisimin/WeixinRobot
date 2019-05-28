using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace WeixinRoboot.Linq
{
    public partial class aspnet_UsersNewGameResultSend : INotifyPropertyChanging, INotifyPropertyChanged
    {

        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);

        private System.Guid _aspnet_UserID;

        private System.Nullable<bool> _IsNewSend;

        private string _ActiveCode;

        private System.Nullable<bool> _IsBlock;

        private System.Nullable<bool> _IsSendPIC;

        private System.Nullable<bool> _IsReceiveOrder;

        private System.Nullable<int> _MaxPlayerCount;

        private System.Nullable<System.Guid> _bossaspnet_UserID;

        private System.Nullable<int> _SendImageEnd;

        private System.Nullable<int> _SendImageStart;

        private System.Nullable<int> _SendImageEnd2;

        private System.Nullable<int> _SendImageEnd3;

        private System.Nullable<int> _SendImageEnd4;

        private System.Nullable<int> _SendImageStart2;

        private System.Nullable<int> _SendImageStart3;

        private System.Nullable<int> _SendImageStart4;

        private string _ImageEndText;

        private string _ImageTopText;

        private System.Nullable<int> _BlockEndHour;

        private System.Nullable<int> _BlockEndMinute;

        private System.Nullable<int> _BlockStartHour;

        private System.Nullable<int> _BlockStartMinute;

        private string _LeiDianPath;

        private string _NoxPath;

        private string _LeiDianSharePath;

        private string _NoxSharePath;

        private System.Nullable<bool> _AdbLeidianMode;

        private System.Nullable<bool> _AdbNoxMode;

        private System.Nullable<bool> _TwoTreeNotSingle;

        private System.Nullable<bool> _XinJiangMode;

        private System.Nullable<bool> _TengXunShiFenMode;

        private System.Nullable<decimal> _FuliRatio;

        private System.Nullable<decimal> _LiuShuiRatio;

        private System.Nullable<bool> _Thread_AoZhouCai;

        private System.Nullable<bool> _Thread_ChongQingShiShiCai;

        private System.Nullable<bool> _Thread_TengXunShiFen;

        private System.Nullable<bool> _Thread_TengXunWuFen;

        private System.Nullable<bool> _Thread_VRChongqing;

        private System.Nullable<bool> _Thread_WuFen;

        private System.Nullable<bool> _Thread_XinJiangShiShiCai;

        private System.Nullable<bool> _Thread_TengXunShiFenXin;

        private System.Nullable<bool> _Thread_TengXunWuFenXin;

        private System.Nullable<bool> _Thread_HeNeiWuFen;

        #region 可扩展性方法定义
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
        partial void Onaspnet_UserIDChanging(System.Guid value);
        partial void Onaspnet_UserIDChanged();
        partial void OnIsNewSendChanging(System.Nullable<bool> value);
        partial void OnIsNewSendChanged();
        partial void OnActiveCodeChanging(string value);
        partial void OnActiveCodeChanged();
        partial void OnIsBlockChanging(System.Nullable<bool> value);
        partial void OnIsBlockChanged();
        partial void OnIsSendPICChanging(System.Nullable<bool> value);
        partial void OnIsSendPICChanged();
        partial void OnIsReceiveOrderChanging(System.Nullable<bool> value);
        partial void OnIsReceiveOrderChanged();
        partial void OnMaxPlayerCountChanging(System.Nullable<int> value);
        partial void OnMaxPlayerCountChanged();
        partial void Onbossaspnet_UserIDChanging(System.Nullable<System.Guid> value);
        partial void Onbossaspnet_UserIDChanged();
        partial void OnSendImageEndChanging(System.Nullable<int> value);
        partial void OnSendImageEndChanged();
        partial void OnSendImageStartChanging(System.Nullable<int> value);
        partial void OnSendImageStartChanged();
        partial void OnSendImageEnd2Changing(System.Nullable<int> value);
        partial void OnSendImageEnd2Changed();
        partial void OnSendImageEnd3Changing(System.Nullable<int> value);
        partial void OnSendImageEnd3Changed();
        partial void OnSendImageEnd4Changing(System.Nullable<int> value);
        partial void OnSendImageEnd4Changed();
        partial void OnSendImageStart2Changing(System.Nullable<int> value);
        partial void OnSendImageStart2Changed();
        partial void OnSendImageStart3Changing(System.Nullable<int> value);
        partial void OnSendImageStart3Changed();
        partial void OnSendImageStart4Changing(System.Nullable<int> value);
        partial void OnSendImageStart4Changed();
        partial void OnImageEndTextChanging(string value);
        partial void OnImageEndTextChanged();
        partial void OnImageTopTextChanging(string value);
        partial void OnImageTopTextChanged();
        partial void OnBlockEndHourChanging(System.Nullable<int> value);
        partial void OnBlockEndHourChanged();
        partial void OnBlockEndMinuteChanging(System.Nullable<int> value);
        partial void OnBlockEndMinuteChanged();
        partial void OnBlockStartHourChanging(System.Nullable<int> value);
        partial void OnBlockStartHourChanged();
        partial void OnBlockStartMinuteChanging(System.Nullable<int> value);
        partial void OnBlockStartMinuteChanged();
        partial void OnLeiDianPathChanging(string value);
        partial void OnLeiDianPathChanged();
        partial void OnNoxPathChanging(string value);
        partial void OnNoxPathChanged();
        partial void OnLeiDianSharePathChanging(string value);
        partial void OnLeiDianSharePathChanged();
        partial void OnNoxSharePathChanging(string value);
        partial void OnNoxSharePathChanged();
        partial void OnAdbLeidianModeChanging(System.Nullable<bool> value);
        partial void OnAdbLeidianModeChanged();
        partial void OnAdbNoxModeChanging(System.Nullable<bool> value);
        partial void OnAdbNoxModeChanged();
        partial void OnTwoTreeNotSingleChanging(System.Nullable<bool> value);
        partial void OnTwoTreeNotSingleChanged();
        partial void OnXinJiangModeChanging(System.Nullable<bool> value);
        partial void OnXinJiangModeChanged();
        partial void OnTengXunShiFenModeChanging(System.Nullable<bool> value);
        partial void OnTengXunShiFenModeChanged();
        partial void OnFuliRatioChanging(System.Nullable<decimal> value);
        partial void OnFuliRatioChanged();
        partial void OnLiuShuiRatioChanging(System.Nullable<decimal> value);
        partial void OnLiuShuiRatioChanged();
        partial void OnThread_AoZhouCaiChanging(System.Nullable<bool> value);
        partial void OnThread_AoZhouCaiChanged();
        partial void OnThread_ChongQingShiShiCaiChanging(System.Nullable<bool> value);
        partial void OnThread_ChongQingShiShiCaiChanged();
        partial void OnThread_TengXunShiFenChanging(System.Nullable<bool> value);
        partial void OnThread_TengXunShiFenChanged();
        partial void OnThread_TengXunWuFenChanging(System.Nullable<bool> value);
        partial void OnThread_TengXunWuFenChanged();
        partial void OnThread_VRChongqingChanging(System.Nullable<bool> value);
        partial void OnThread_VRChongqingChanged();
        partial void OnThread_WuFenChanging(System.Nullable<bool> value);
        partial void OnThread_WuFenChanged();
        partial void OnThread_XinJiangShiShiCaiChanging(System.Nullable<bool> value);
        partial void OnThread_XinJiangShiShiCaiChanged();
        partial void OnThread_TengXunShiFenXinChanging(System.Nullable<bool> value);
        partial void OnThread_TengXunShiFenXinChanged();
        partial void OnThread_TengXunWuFenXinChanging(System.Nullable<bool> value);
        partial void OnThread_TengXunWuFenXinChanged();
        partial void OnThread_HeNeiWuFenChanging(System.Nullable<bool> value);
        partial void OnThread_HeNeiWuFenChanged();
        #endregion

        public aspnet_UsersNewGameResultSend()
        {
            OnCreated();
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_aspnet_UserID", DbType = "UniqueIdentifier NOT NULL", IsPrimaryKey = true)]
        public System.Guid aspnet_UserID
        {
            get
            {
                return this._aspnet_UserID;
            }
            set
            {
                if ((this._aspnet_UserID != value))
                {
                    this.Onaspnet_UserIDChanging(value);
                    this.SendPropertyChanging();
                    this._aspnet_UserID = value;
                    this.SendPropertyChanged("aspnet_UserID");
                    this.Onaspnet_UserIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsNewSend", DbType = "Bit")]
        public System.Nullable<bool> IsNewSend
        {
            get
            {
                return this._IsNewSend;
            }
            set
            {
                if ((this._IsNewSend != value))
                {
                    this.OnIsNewSendChanging(value);
                    this.SendPropertyChanging();
                    this._IsNewSend = value;
                    this.SendPropertyChanged("IsNewSend");
                    this.OnIsNewSendChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ActiveCode", DbType = "NVarChar(4000)")]
        public string ActiveCode
        {
            get
            {
                return this._ActiveCode;
            }
            set
            {
                if ((this._ActiveCode != value))
                {
                    this.OnActiveCodeChanging(value);
                    this.SendPropertyChanging();
                    this._ActiveCode = value;
                    this.SendPropertyChanged("ActiveCode");
                    this.OnActiveCodeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsBlock", DbType = "Bit")]
        public System.Nullable<bool> IsBlock
        {
            get
            {
                return this._IsBlock;
            }
            set
            {
                if ((this._IsBlock != value))
                {
                    this.OnIsBlockChanging(value);
                    this.SendPropertyChanging();
                    this._IsBlock = value;
                    this.SendPropertyChanged("IsBlock");
                    this.OnIsBlockChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsSendPIC", DbType = "Bit")]
        public System.Nullable<bool> IsSendPIC
        {
            get
            {
                return this._IsSendPIC;
            }
            set
            {
                if ((this._IsSendPIC != value))
                {
                    this.OnIsSendPICChanging(value);
                    this.SendPropertyChanging();
                    this._IsSendPIC = value;
                    this.SendPropertyChanged("IsSendPIC");
                    this.OnIsSendPICChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_IsReceiveOrder", DbType = "Bit")]
        public System.Nullable<bool> IsReceiveOrder
        {
            get
            {
                return this._IsReceiveOrder;
            }
            set
            {
                if ((this._IsReceiveOrder != value))
                {
                    this.OnIsReceiveOrderChanging(value);
                    this.SendPropertyChanging();
                    this._IsReceiveOrder = value;
                    this.SendPropertyChanged("IsReceiveOrder");
                    this.OnIsReceiveOrderChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MaxPlayerCount", DbType = "Int")]
        public System.Nullable<int> MaxPlayerCount
        {
            get
            {
                return this._MaxPlayerCount;
            }
            set
            {
                if ((this._MaxPlayerCount != value))
                {
                    this.OnMaxPlayerCountChanging(value);
                    this.SendPropertyChanging();
                    this._MaxPlayerCount = value;
                    this.SendPropertyChanged("MaxPlayerCount");
                    this.OnMaxPlayerCountChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_bossaspnet_UserID", DbType = "UniqueIdentifier")]
        public System.Nullable<System.Guid> bossaspnet_UserID
        {
            get
            {
                return this._bossaspnet_UserID;
            }
            set
            {
                if ((this._bossaspnet_UserID != value))
                {
                    this.Onbossaspnet_UserIDChanging(value);
                    this.SendPropertyChanging();
                    this._bossaspnet_UserID = value;
                    this.SendPropertyChanged("bossaspnet_UserID");
                    this.Onbossaspnet_UserIDChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageEnd", DbType = "Int")]
        public System.Nullable<int> SendImageEnd
        {
            get
            {
                return this._SendImageEnd;
            }
            set
            {
                if ((this._SendImageEnd != value))
                {
                    this.OnSendImageEndChanging(value);
                    this.SendPropertyChanging();
                    this._SendImageEnd = value;
                    this.SendPropertyChanged("SendImageEnd");
                    this.OnSendImageEndChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageStart", DbType = "Int")]
        public System.Nullable<int> SendImageStart
        {
            get
            {
                return this._SendImageStart;
            }
            set
            {
                if ((this._SendImageStart != value))
                {
                    this.OnSendImageStartChanging(value);
                    this.SendPropertyChanging();
                    this._SendImageStart = value;
                    this.SendPropertyChanged("SendImageStart");
                    this.OnSendImageStartChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageEnd2", DbType = "Int")]
        public System.Nullable<int> SendImageEnd2
        {
            get
            {
                return this._SendImageEnd2;
            }
            set
            {
                if ((this._SendImageEnd2 != value))
                {
                    this.OnSendImageEnd2Changing(value);
                    this.SendPropertyChanging();
                    this._SendImageEnd2 = value;
                    this.SendPropertyChanged("SendImageEnd2");
                    this.OnSendImageEnd2Changed();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageEnd3", DbType = "Int")]
        public System.Nullable<int> SendImageEnd3
        {
            get
            {
                return this._SendImageEnd3;
            }
            set
            {
                if ((this._SendImageEnd3 != value))
                {
                    this.OnSendImageEnd3Changing(value);
                    this.SendPropertyChanging();
                    this._SendImageEnd3 = value;
                    this.SendPropertyChanged("SendImageEnd3");
                    this.OnSendImageEnd3Changed();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageEnd4", DbType = "Int")]
        public System.Nullable<int> SendImageEnd4
        {
            get
            {
                return this._SendImageEnd4;
            }
            set
            {
                if ((this._SendImageEnd4 != value))
                {
                    this.OnSendImageEnd4Changing(value);
                    this.SendPropertyChanging();
                    this._SendImageEnd4 = value;
                    this.SendPropertyChanged("SendImageEnd4");
                    this.OnSendImageEnd4Changed();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageStart2", DbType = "Int")]
        public System.Nullable<int> SendImageStart2
        {
            get
            {
                return this._SendImageStart2;
            }
            set
            {
                if ((this._SendImageStart2 != value))
                {
                    this.OnSendImageStart2Changing(value);
                    this.SendPropertyChanging();
                    this._SendImageStart2 = value;
                    this.SendPropertyChanged("SendImageStart2");
                    this.OnSendImageStart2Changed();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageStart3", DbType = "Int")]
        public System.Nullable<int> SendImageStart3
        {
            get
            {
                return this._SendImageStart3;
            }
            set
            {
                if ((this._SendImageStart3 != value))
                {
                    this.OnSendImageStart3Changing(value);
                    this.SendPropertyChanging();
                    this._SendImageStart3 = value;
                    this.SendPropertyChanged("SendImageStart3");
                    this.OnSendImageStart3Changed();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_SendImageStart4", DbType = "Int")]
        public System.Nullable<int> SendImageStart4
        {
            get
            {
                return this._SendImageStart4;
            }
            set
            {
                if ((this._SendImageStart4 != value))
                {
                    this.OnSendImageStart4Changing(value);
                    this.SendPropertyChanging();
                    this._SendImageStart4 = value;
                    this.SendPropertyChanged("SendImageStart4");
                    this.OnSendImageStart4Changed();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ImageEndText", DbType = "NVarChar(200)")]
        public string ImageEndText
        {
            get
            {
                return this._ImageEndText;
            }
            set
            {
                if ((this._ImageEndText != value))
                {
                    this.OnImageEndTextChanging(value);
                    this.SendPropertyChanging();
                    this._ImageEndText = value;
                    this.SendPropertyChanged("ImageEndText");
                    this.OnImageEndTextChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ImageTopText", DbType = "NVarChar(200)")]
        public string ImageTopText
        {
            get
            {
                return this._ImageTopText;
            }
            set
            {
                if ((this._ImageTopText != value))
                {
                    this.OnImageTopTextChanging(value);
                    this.SendPropertyChanging();
                    this._ImageTopText = value;
                    this.SendPropertyChanged("ImageTopText");
                    this.OnImageTopTextChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BlockEndHour", DbType = "Int")]
        public System.Nullable<int> BlockEndHour
        {
            get
            {
                return this._BlockEndHour;
            }
            set
            {
                if ((this._BlockEndHour != value))
                {
                    this.OnBlockEndHourChanging(value);
                    this.SendPropertyChanging();
                    this._BlockEndHour = value;
                    this.SendPropertyChanged("BlockEndHour");
                    this.OnBlockEndHourChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BlockEndMinute", DbType = "Int")]
        public System.Nullable<int> BlockEndMinute
        {
            get
            {
                return this._BlockEndMinute;
            }
            set
            {
                if ((this._BlockEndMinute != value))
                {
                    this.OnBlockEndMinuteChanging(value);
                    this.SendPropertyChanging();
                    this._BlockEndMinute = value;
                    this.SendPropertyChanged("BlockEndMinute");
                    this.OnBlockEndMinuteChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BlockStartHour", DbType = "Int")]
        public System.Nullable<int> BlockStartHour
        {
            get
            {
                return this._BlockStartHour;
            }
            set
            {
                if ((this._BlockStartHour != value))
                {
                    this.OnBlockStartHourChanging(value);
                    this.SendPropertyChanging();
                    this._BlockStartHour = value;
                    this.SendPropertyChanged("BlockStartHour");
                    this.OnBlockStartHourChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_BlockStartMinute", DbType = "Int")]
        public System.Nullable<int> BlockStartMinute
        {
            get
            {
                return this._BlockStartMinute;
            }
            set
            {
                if ((this._BlockStartMinute != value))
                {
                    this.OnBlockStartMinuteChanging(value);
                    this.SendPropertyChanging();
                    this._BlockStartMinute = value;
                    this.SendPropertyChanged("BlockStartMinute");
                    this.OnBlockStartMinuteChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LeiDianPath", DbType = "NVarChar(500)")]
        public string LeiDianPath
        {
            get
            {
                return this._LeiDianPath;
            }
            set
            {
                if ((this._LeiDianPath != value))
                {
                    this.OnLeiDianPathChanging(value);
                    this.SendPropertyChanging();
                    this._LeiDianPath = value;
                    this.SendPropertyChanged("LeiDianPath");
                    this.OnLeiDianPathChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoxPath", DbType = "NVarChar(500)")]
        public string NoxPath
        {
            get
            {
                return this._NoxPath;
            }
            set
            {
                if ((this._NoxPath != value))
                {
                    this.OnNoxPathChanging(value);
                    this.SendPropertyChanging();
                    this._NoxPath = value;
                    this.SendPropertyChanged("NoxPath");
                    this.OnNoxPathChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LeiDianSharePath", DbType = "NVarChar(500)")]
        public string LeiDianSharePath
        {
            get
            {
                return this._LeiDianSharePath;
            }
            set
            {
                if ((this._LeiDianSharePath != value))
                {
                    this.OnLeiDianSharePathChanging(value);
                    this.SendPropertyChanging();
                    this._LeiDianSharePath = value;
                    this.SendPropertyChanged("LeiDianSharePath");
                    this.OnLeiDianSharePathChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NoxSharePath", DbType = "NVarChar(500)")]
        public string NoxSharePath
        {
            get
            {
                return this._NoxSharePath;
            }
            set
            {
                if ((this._NoxSharePath != value))
                {
                    this.OnNoxSharePathChanging(value);
                    this.SendPropertyChanging();
                    this._NoxSharePath = value;
                    this.SendPropertyChanged("NoxSharePath");
                    this.OnNoxSharePathChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AdbLeidianMode", DbType = "Bit")]
        public System.Nullable<bool> AdbLeidianMode
        {
            get
            {
                return this._AdbLeidianMode;
            }
            set
            {
                if ((this._AdbLeidianMode != value))
                {
                    this.OnAdbLeidianModeChanging(value);
                    this.SendPropertyChanging();
                    this._AdbLeidianMode = value;
                    this.SendPropertyChanged("AdbLeidianMode");
                    this.OnAdbLeidianModeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_AdbNoxMode", DbType = "Bit")]
        public System.Nullable<bool> AdbNoxMode
        {
            get
            {
                return this._AdbNoxMode;
            }
            set
            {
                if ((this._AdbNoxMode != value))
                {
                    this.OnAdbNoxModeChanging(value);
                    this.SendPropertyChanging();
                    this._AdbNoxMode = value;
                    this.SendPropertyChanged("AdbNoxMode");
                    this.OnAdbNoxModeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TwoTreeNotSingle", DbType = "Bit")]
        public System.Nullable<bool> TwoTreeNotSingle
        {
            get
            {
                return this._TwoTreeNotSingle;
            }
            set
            {
                if ((this._TwoTreeNotSingle != value))
                {
                    this.OnTwoTreeNotSingleChanging(value);
                    this.SendPropertyChanging();
                    this._TwoTreeNotSingle = value;
                    this.SendPropertyChanged("TwoTreeNotSingle");
                    this.OnTwoTreeNotSingleChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_XinJiangMode", DbType = "Bit")]
        public System.Nullable<bool> XinJiangMode
        {
            get
            {
                return this._XinJiangMode;
            }
            set
            {
                if ((this._XinJiangMode != value))
                {
                    this.OnXinJiangModeChanging(value);
                    this.SendPropertyChanging();
                    this._XinJiangMode = value;
                    this.SendPropertyChanged("XinJiangMode");
                    this.OnXinJiangModeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_TengXunShiFenMode", DbType = "Bit")]
        public System.Nullable<bool> TengXunShiFenMode
        {
            get
            {
                return this._TengXunShiFenMode;
            }
            set
            {
                if ((this._TengXunShiFenMode != value))
                {
                    this.OnTengXunShiFenModeChanging(value);
                    this.SendPropertyChanging();
                    this._TengXunShiFenMode = value;
                    this.SendPropertyChanged("TengXunShiFenMode");
                    this.OnTengXunShiFenModeChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FuliRatio", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> FuliRatio
        {
            get
            {
                return this._FuliRatio;
            }
            set
            {
                if ((this._FuliRatio != value))
                {
                    this.OnFuliRatioChanging(value);
                    this.SendPropertyChanging();
                    this._FuliRatio = value;
                    this.SendPropertyChanged("FuliRatio");
                    this.OnFuliRatioChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_LiuShuiRatio", DbType = "Decimal(18,5)")]
        public System.Nullable<decimal> LiuShuiRatio
        {
            get
            {
                return this._LiuShuiRatio;
            }
            set
            {
                if ((this._LiuShuiRatio != value))
                {
                    this.OnLiuShuiRatioChanging(value);
                    this.SendPropertyChanging();
                    this._LiuShuiRatio = value;
                    this.SendPropertyChanged("LiuShuiRatio");
                    this.OnLiuShuiRatioChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_AoZhouCai", DbType = "Bit")]
        public System.Nullable<bool> Thread_AoZhouCai
        {
            get
            {
                return this._Thread_AoZhouCai;
            }
            set
            {
                if ((this._Thread_AoZhouCai != value))
                {
                    this.OnThread_AoZhouCaiChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_AoZhouCai = value;
                    this.SendPropertyChanged("Thread_AoZhouCai");
                    this.OnThread_AoZhouCaiChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_ChongQingShiShiCai", DbType = "Bit")]
        public System.Nullable<bool> Thread_ChongQingShiShiCai
        {
            get
            {
                return this._Thread_ChongQingShiShiCai;
            }
            set
            {
                if ((this._Thread_ChongQingShiShiCai != value))
                {
                    this.OnThread_ChongQingShiShiCaiChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_ChongQingShiShiCai = value;
                    this.SendPropertyChanged("Thread_ChongQingShiShiCai");
                    this.OnThread_ChongQingShiShiCaiChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_TengXunShiFen", DbType = "Bit")]
        public System.Nullable<bool> Thread_TengXunShiFen
        {
            get
            {
                return this._Thread_TengXunShiFen;
            }
            set
            {
                if ((this._Thread_TengXunShiFen != value))
                {
                    this.OnThread_TengXunShiFenChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_TengXunShiFen = value;
                    this.SendPropertyChanged("Thread_TengXunShiFen");
                    this.OnThread_TengXunShiFenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_TengXunWuFen", DbType = "Bit")]
        public System.Nullable<bool> Thread_TengXunWuFen
        {
            get
            {
                return this._Thread_TengXunWuFen;
            }
            set
            {
                if ((this._Thread_TengXunWuFen != value))
                {
                    this.OnThread_TengXunWuFenChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_TengXunWuFen = value;
                    this.SendPropertyChanged("Thread_TengXunWuFen");
                    this.OnThread_TengXunWuFenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_VRChongqing", DbType = "Bit")]
        public System.Nullable<bool> Thread_VRChongqing
        {
            get
            {
                return this._Thread_VRChongqing;
            }
            set
            {
                if ((this._Thread_VRChongqing != value))
                {
                    this.OnThread_VRChongqingChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_VRChongqing = value;
                    this.SendPropertyChanged("Thread_VRChongqing");
                    this.OnThread_VRChongqingChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_WuFen", DbType = "Bit")]
        public System.Nullable<bool> Thread_WuFen
        {
            get
            {
                return this._Thread_WuFen;
            }
            set
            {
                if ((this._Thread_WuFen != value))
                {
                    this.OnThread_WuFenChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_WuFen = value;
                    this.SendPropertyChanged("Thread_WuFen");
                    this.OnThread_WuFenChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_XinJiangShiShiCai", DbType = "Bit")]
        public System.Nullable<bool> Thread_XinJiangShiShiCai
        {
            get
            {
                return this._Thread_XinJiangShiShiCai;
            }
            set
            {
                if ((this._Thread_XinJiangShiShiCai != value))
                {
                    this.OnThread_XinJiangShiShiCaiChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_XinJiangShiShiCai = value;
                    this.SendPropertyChanged("Thread_XinJiangShiShiCai");
                    this.OnThread_XinJiangShiShiCaiChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_TengXunShiFenXin", DbType = "Bit")]
        public System.Nullable<bool> Thread_TengXunShiFenXin
        {
            get
            {
                return this._Thread_TengXunShiFenXin;
            }
            set
            {
                if ((this._Thread_TengXunShiFenXin != value))
                {
                    this.OnThread_TengXunShiFenXinChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_TengXunShiFenXin = value;
                    this.SendPropertyChanged("Thread_TengXunShiFenXin");
                    this.OnThread_TengXunShiFenXinChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_TengXunWuFenXin", DbType = "Bit")]
        public System.Nullable<bool> Thread_TengXunWuFenXin
        {
            get
            {
                return this._Thread_TengXunWuFenXin;
            }
            set
            {
                if ((this._Thread_TengXunWuFenXin != value))
                {
                    this.OnThread_TengXunWuFenXinChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_TengXunWuFenXin = value;
                    this.SendPropertyChanged("Thread_TengXunWuFenXin");
                    this.OnThread_TengXunWuFenXinChanged();
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Thread_HeNeiWuFen", DbType = "Bit")]
        public System.Nullable<bool> Thread_HeNeiWuFen
        {
            get
            {
                return this._Thread_HeNeiWuFen;
            }
            set
            {
                if ((this._Thread_HeNeiWuFen != value))
                {
                    this.OnThread_HeNeiWuFenChanging(value);
                    this.SendPropertyChanging();
                    this._Thread_HeNeiWuFen = value;
                    this.SendPropertyChanged("Thread_HeNeiWuFen");
                    this.OnThread_HeNeiWuFenChanged();
                }
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void SendPropertyChanging()
        {
            if ((this.PropertyChanging != null))
            {
                this.PropertyChanging(this, emptyChangingEventArgs);
            }
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class Util_Services
    {
        public static aspnet_UsersNewGameResultSend GetServicesSetting()
        {
            return GetServicesSetting(GlobalParam.UserKey);

        }
        public static aspnet_UsersNewGameResultSend GetServicesSetting(Guid key)
        {
            RobootWeb.WebService ws = new RobootWeb.WebService();
            string jaspnet_UsersNewGameResultSend = ws.GetSetting(key.ToString());
            aspnet_UsersNewGameResultSend tins_sets = (aspnet_UsersNewGameResultSend)JsonConvert.DeserializeObject(jaspnet_UsersNewGameResultSend);
            return tins_sets;

        }

        public static string SaveServicesSetting(aspnet_UsersNewGameResultSend tosaves)
        {

            RobootWeb.WebService ws = new RobootWeb.WebService();
            string Jresult = ws.SaveSetting(GlobalParam.UserName, GlobalParam.Password, JsonConvert.SerializeObject(tosaves));
            return Jresult;
        }
    }

}
