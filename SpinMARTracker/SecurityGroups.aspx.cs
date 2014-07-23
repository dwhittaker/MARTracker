/*
 * Created by SharpDevelop.
 * User: DWHITTAKER
 * Date: 7/21/2014
 * Time: 3:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;

namespace SpinMARTracker
{
	/// <summary>
	/// Description of SecurityGroups
	/// </summary>
	public class SecurityGroups : Page
	{	
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		#region Data
		protected DataGrid dgSgrpGrid;
		protected CheckBox chkCanDel;
		protected CheckBox chkCanMer;
		protected CheckBox chkDiffMer;
		protected CheckBox chkEditRel;
		protected CheckBox chkExpMO;
		protected CheckBox chkManU;
		protected CheckBox chkManGrp;
		protected Button btnSaveUpdate;
		protected Button btnCancel;
		protected TextBox txtSgroup;
		protected Panel PanAddUpdate;
			
		#endregion
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		#region Page Init & Exit (Open/Close DB connections here...)

		protected void PageInit(object sender, System.EventArgs e)
		{
		}
		//----------------------------------------------------------------------
		protected void PageExit(object sender, System.EventArgs e)
		{
		}

		#endregion
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		#region Page Load
		private void Page_Load(object sender, System.EventArgs e)
		{
			//------------------------------------------------------------------
			if(!IsPostBack)
			{
			}
			dgSgrpGrid.DataSource = Utility.GetSQLDataView("Select * from SecurityGroups");
			dgSgrpGrid.DataBind();
			//------------------------------------------------------------------
		}
		#endregion
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		#region Add more events here...

		#endregion
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		#region Initialize Component

		protected override void OnInit(EventArgs e)
		{	InitializeComponent();
			base.OnInit(e);
		}
		//----------------------------------------------------------------------
		private void InitializeComponent()
		{	//------------------------------------------------------------------
			this.Load	+= new System.EventHandler(Page_Load);
			this.Init   += new System.EventHandler(PageInit);
			this.Unload += new System.EventHandler(PageExit);
			this.btnSaveUpdate.Click += new System.EventHandler(AddUpdateGroup);
			//------------------------------------------------------------------
			//------------------------------------------------------------------
		}
		#endregion
		//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
		#region Data Manipulation
		protected void AddUpdateGroup(object sender, EventArgs e){
			SqlConnection sqlconn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]);
			SqlCommand addcmd = new SqlCommand("CreateSGroup", sqlconn);
			
			if (btnSaveUpdate.Text == "Add Group"){
				addcmd.CommandType = CommandType.StoredProcedure;
				addcmd.Parameters.Add("@name",SqlDbType.VarChar);
				addcmd.Parameters.Add("@canDel",SqlDbType.Bit);
				addcmd.Parameters.Add("@canMer",SqlDbType.Bit);
				addcmd.Parameters.Add("@EditRel",SqlDbType.Bit);
				addcmd.Parameters.Add("@MerD",SqlDbType.Bit);
				addcmd.Parameters.Add("@CanEx",SqlDbType.Bit);
				addcmd.Parameters.Add("@ManU",SqlDbType.Bit);
				addcmd.Parameters.Add("@ManG",SqlDbType.Bit);
				addcmd.Parameters["@name"].Value = txtSgroup.Text;
				addcmd.Parameters["@canDel"].Value = chkCanDel.Checked;
				addcmd.Parameters["@canMer"].Value = chkCanMer.Checked;
				addcmd.Parameters["@EditRel"].Value = chkEditRel.Checked;
				addcmd.Parameters["@MerD"].Value = chkDiffMer.Checked;
				addcmd.Parameters["@CanEx"].Value = chkExpMO.Checked;
				addcmd.Parameters["@ManU"].Value = chkManU.Checked;
				addcmd.Parameters["@ManG"].Value = chkManGrp.Checked;
				
				sqlconn.Open();
				
				addcmd.ExecuteNonQuery();
				
				addcmd = null;
				
				sqlconn.Close();
				
				dgSgrpGrid.DataSource = Utility.GetSQLDataView("Select * from SecurityGroups");
				dgSgrpGrid.DataBind();
			}

		}
		protected void EditGroup(object sender, DataGridCommandEventArgs e){
			
		}
		protected void DeleteGroup(object sender, DataGridCommandEventArgs e){
			SqlConnection sqlconn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"]);
			SqlCommand delcmd = new SqlCommand("DeleteSgroup",sqlconn);
			
			delcmd.CommandType = CommandType.StoredProcedure;
			delcmd.Parameters.Add("@sgid",SqlDbType.Int);
			delcmd.Parameters["@sgid"].Value = Convert.ToInt32(e.Item.Cells[0].Text.ToString());
			
			sqlconn.Open();
			
			delcmd.ExecuteNonQuery();
			
			delcmd = null;
			
			sqlconn.Close();
			
			dgSgrpGrid.DataSource = Utility.GetSQLDataView("Select * from SecurityGroups");
			dgSgrpGrid.DataBind();
		}
		#endregion
	}

}
