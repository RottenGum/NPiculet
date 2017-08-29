﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NPiculet.Authorization;

public partial class modules_Logout : System.Web.UI.Page
{
	protected void Page_Load(object sender, EventArgs e)
	{
		LoginKit.Logout();
		Response.Redirect("~/modules/Login.aspx");
	}
}