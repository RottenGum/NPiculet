﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Breadcrumb.ascx.cs" Inherits="web_uc_Breadcrumb" %>
<ul class="sui-breadcrumb">
	<li>当前位置：</li>
	<li><asp:HyperLink ID="home" runat="server" NavigateUrl="~/default.aspx">首页</asp:HyperLink></li>
	<li class="active">
		<asp:HyperLink ID="listPageLink" runat="server">
			<asp:Literal ID="curPageName" runat="server"></asp:Literal>
		</asp:HyperLink>
	</li>
</ul>