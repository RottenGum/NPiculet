﻿using System;
using System.IO;
using NPiculet.Base.EF;
using NPiculet.Logic.Base;
using NPiculet.Logic.Business;
using NPiculet.Logic.Data;
using NPiculet.Toolkit;

namespace modules.info
{
	public partial class AdvEdit : AdminPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!Page.IsPostBack) {
				this.Id.Value = WebParmKit.GetQuery("key", 0).ToString();
				BindType();
				BindData();
			}
		}

		private void BindType()
		{
			BasDictItemBus ibus = new BasDictItemBus();
			var list = ibus.GetActiveItemList("Publicity");
			BindKit.BindToListControl(this.Position, list, "Name", "Code");
		}

		private readonly CmsAdvInfoBus _bus = new CmsAdvInfoBus();

		private void BindData()
		{
			var advId = Convert.ToInt32(this.Id.Value);
			if (advId > 0) {
				var model = _bus.QueryModel("Id=" + advId);
				if (model != null) {
					BindKit.BindModelToContainer(this.editor, model);
					this.txtTitle.Text = model.Title;
					if (model.StartDate > DateTime.MinValue) this.StartDate.Text = model.StartDate.ToString("yyyy-MM-dd");
					if (model.EndDate > DateTime.MinValue) this.EndDate.Text = model.EndDate.ToString("yyyy-MM-dd");

					ShowThumb(model);
					SetControlStatus();
				}
			}
		}

		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (Page.IsValid) {
				var model = new CmsAdvInfo();
				BindKit.FillModelFromContainer(this.editor, model);

				model.Title = this.txtTitle.Text;

				if (!string.IsNullOrEmpty(this.BannerImage.FileName)) {
					//清理老图像
					if (!string.IsNullOrEmpty(this.PreviewImage.ImageUrl)) {
						var f = new FileInfo(Server.MapPath(this.PreviewImage.ImageUrl));
						if (f.Exists) f.Delete();
					}
					//更新新图
					model.Image = FileWebKit.SaveZoomImage(this.BannerImage.PostedFile, 1200);
					//model.Image = FileWebKit.SaveFile(this.AdvImage.PostedFile);
				}

				if (!string.IsNullOrEmpty(this.BannerCover.FileName)) {
					//清理老图像
					if (!string.IsNullOrEmpty(this.PreviewCover.ImageUrl)) {
						var f = new FileInfo(Server.MapPath(this.PreviewCover.ImageUrl));
						if (f.Exists) f.Delete();
					}
					//更新新图
					model.Cover = FileWebKit.SaveZoomImage(this.BannerCover.PostedFile, 1200);
					//model.Image = FileWebKit.SaveFile(this.AdvImage.PostedFile);
				}

				if (this.Id.Value == "0") {
					//model.Type = "";
					model.Click = 0;
					model.IsEnabled = 1;
					model.CreateDate = DateTime.Now;
					model.Creator = this.CurrentUserName;

					this.Id.Value = _bus.InsertIdentity(model).ToString();
				} else {
					_bus.Update(model, null);
				}

				ShowThumb(model);

				this.promptControl.ShowSuccess("保存成功！");
			}
		}

		private void ShowThumb(CmsAdvInfo adv)
		{
			if (!string.IsNullOrEmpty(adv.Image)) {
				this.ImageHyperLink.NavigateUrl = adv.Image;
				this.PreviewImage.ImageUrl = adv.Image;
				this.PreviewImage.Visible = true;
			} else {
				//this.PreviewImage.Visible = false;
			}

			if (!string.IsNullOrEmpty(adv.Cover)) {
				this.CoverHyperLink.NavigateUrl = adv.Cover;
				this.PreviewCover.ImageUrl = adv.Cover;
				this.PreviewCover.Visible = true;
			} else {
				//this.PreviewCover.Visible = false;
			}
		}

		protected void Position_OnSelectedIndexChanged(object sender, EventArgs e) {
			SetControlStatus();
		}

		private void SetControlStatus() {
			this.phCover.Visible = this.Position.SelectedValue == "Cover";
		}
	}
}