﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageFooter.ascx.cs" Inherits="web_uc_PageFooter" %>
<div class="ui-footer">
	<div class="copyright">
		<div class="logo-small"></div>
		<a href="/">网站首页</a>
		&nbsp;&nbsp; | &nbsp;&nbsp; <a href="/modules/Login.aspx" target="_blank">网站管理</a>
		&nbsp;&nbsp; | &nbsp;&nbsp; <a href="javascript:void(0);" onclick="SetHome(this,'http://ccc');">设为首页</a>
		&nbsp;&nbsp; | &nbsp;&nbsp; <a href="javascript:void(0);" onclick="AddFavorite('云南省经侦总队', location.href)">加入收藏</a>
		&nbsp;&nbsp; | &nbsp;&nbsp; <a href="/aboutus" target="_blank">技术支持</a><br/>
		联系方式：0871-88888888 举报电话：0871-88888888<br/>
		联系邮箱：lisa@china.com<br/>
		Copyright (c) 2017 WebsiteName All Rights Reserved
	</div>
</div>
<script  type="text/javascript">
//设为首页
	function SetHome(obj,url){
		try{
			obj.style.behavior='url(#default#homepage)';
			obj.setHomePage(url);
		}catch(e){
			if(window.netscape){
				try{
					netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
				}catch(e){
					alert("抱歉，此操作被浏览器拒绝！\n\n请在浏览器地址栏输入“about:config”并回车然后将[signed.applets.codebase_principal_support]设置为'true'");
				}
			}else{
				alert("抱歉，您所使用的浏览器无法完成此操作。\n\n您需要手动将【"+url+"】设置为首页。");
			}
		}
	}
//收藏本站
	function AddFavorite(title, url) {
		try {
			window.external.addFavorite(url, title);
		}
		catch (e) {
			try {
				window.sidebar.addPanel(title, url, "");
			}
			catch (e) {
				alert("抱歉，您所使用的浏览器无法完成此操作。\n\n加入收藏失败，请使用Ctrl+D进行添加");
			}
		}
	}
//保存到桌面
	function toDesktop(sUrl,sName){
		try {
			var WshShell = new ActiveXObject("WScript.Shell");
			var oUrlLink = WshShell.CreateShortcut(WshShell.SpecialFolders("Desktop") + "\\" + sName + ".url");
			oUrlLink.TargetPath = sUrl;
			oUrlLink.Save();
		}  
		catch(e)  {  
			alert("当前IE安全级别不允许操作！");  
		}
	}
</script>