﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPiculet.Logic.Base;
using NPiculet.Logic.Business;
using NPiculet.Logic.Data;
using NPiculet.Toolkit;

public partial class modules_system_PointSet : AdminPage
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!Page.IsPostBack) {
			BindData();
		}

		this.NPager1.PageClick += (o, args) => {
			BindData();
		};
	}

	private readonly SysUserInfoBus _bus = new SysUserInfoBus();

	private void BindData()
	{
		string whereString = "IsDel=0";
		string key = this.txtKeywords.Text.FormatSqlParm();
		if (!string.IsNullOrEmpty(key))
			whereString += string.Format(" and (Account LIKE '%{0}%' or Name LIKE '%{0}%')", key);

		int count = _bus.RecordCount(whereString);
		this.NPager1.PageSize = 10;
		this.NPager1.RecordCount = count;

		DataTable dt = _bus.GetUserList(this.NPager1.CurrentPage, this.NPager1.PageSize, whereString, "OrderBy, CreateDate DESC");

		this.list.DataSource = dt.DefaultView;
		this.list.DataBind();
	}

	protected string GetStatusString(string enabled)
	{
		return enabled == "1" ? "启用" : "停用";
	}

	protected void btnSearch_Click(object sender, EventArgs e)
	{
		BindData();
	}
}