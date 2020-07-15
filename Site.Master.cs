using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneWebApp
{
   
    
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            roleID.Text = Login.getAccessLevel().ToString();
            personID.Text = Login.getUserID().ToString();
            if(Login.getAccessLevel() < 6)
            {
                navUsers.Visible = false;
                navUsers.Enabled = false;
            }
            if(Login.getAccessLevel() < 3)
            {
                navStudent.Visible = false;
                navStudent.Enabled = false;
            }
            if (Login.getAccessLevel() < 2)
            {
                navRecords.Visible = false;
                navRecords.Enabled = false;
            }
        }
    }
}