﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace TaoHua_Insertor
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="WeinxinRoboot")]
	public partial class dbDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region 可扩展性方法定义
    partial void OnCreated();
    partial void InsertTaoHua_GameResult(TaoHua_GameResult instance);
    partial void UpdateTaoHua_GameResult(TaoHua_GameResult instance);
    partial void DeleteTaoHua_GameResult(TaoHua_GameResult instance);
    #endregion
		
		public dbDataContext() : 
				base(global::TaoHua_Insertor.Properties.Settings.Default.WeinxinRobootConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public dbDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<TaoHua_GameResult> TaoHua_GameResult
		{
			get
			{
				return this.GetTable<TaoHua_GameResult>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.TaoHua_GameResult")]
	public partial class TaoHua_GameResult : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private string _GamePeriod;
		
		private string _GameResult;
		
    #region 可扩展性方法定义
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGamePeriodChanging(string value);
    partial void OnGamePeriodChanged();
    partial void OnGameResultChanging(string value);
    partial void OnGameResultChanged();
    #endregion
		
		public TaoHua_GameResult()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GamePeriod", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string GamePeriod
		{
			get
			{
				return this._GamePeriod;
			}
			set
			{
				if ((this._GamePeriod != value))
				{
					this.OnGamePeriodChanging(value);
					this.SendPropertyChanging();
					this._GamePeriod = value;
					this.SendPropertyChanged("GamePeriod");
					this.OnGamePeriodChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GameResult", DbType="NVarChar(50) NOT NULL", CanBeNull=false, IsPrimaryKey=true)]
		public string GameResult
		{
			get
			{
				return this._GameResult;
			}
			set
			{
				if ((this._GameResult != value))
				{
					this.OnGameResultChanging(value);
					this.SendPropertyChanging();
					this._GameResult = value;
					this.SendPropertyChanged("GameResult");
					this.OnGameResultChanged();
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
}
#pragma warning restore 1591
