using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace CapstoneWebApp
{
    public partial class Faculty : System.Web.UI.Page
    {
        readonly SqlConnection sqlCon = new SqlConnection(Login.getConnStr());
        int PersonID = 0;
        

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.Page.User.Identity.IsAuthenticated)
            {
                Login.setAccessLevel(0);
            }

            if (Login.getAccessLevel() >= 5 && !IsPostBack)
            {
                ButtonCreateNew.Enabled = true;
                ButtonCreateNew.Visible = true;
                
            }

            FillGridView();
        }


        void FillGridView()
        {
            //check to see if SQL connection is opn
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ViewAllFaculty", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            gvFaculty.DataSource = dtbl;
            gvFaculty.DataBind();
        }

        protected void ButtonCreateNew_Click(object sender, EventArgs e)
        {
            ButtonCreateNew.Enabled = false;
            ButtonCreateNew.Visible = false;
            ButtonConfirmAdd.Enabled = true;
            ButtonConfirmAdd.Visible = true;
            ButtonCancel.Enabled = true;
            ButtonCancel.Visible = true;
            personForm.Visible = true;
            personForm.Enabled = true;
        }
        protected void ButtonConfirmAdd_Click(object sender, EventArgs e)
        {
            if (Login.getAccessLevel() >= 5)
            {
                //check to see if SQL connection is opn
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("AddNewFaculty", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@fname", txtFirstName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@lname", txtLastName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@dob", txtDOB.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                string PersonID = hfPersonID.Value;
                Clear();
                if (PersonID == "")
                    SuccessMessage.Text = "Record Saved Added!";
                else
                    SuccessMessage.Text = "Record Updated Successfully!";
                FillGridView();
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (Login.getAccessLevel() >= 5)
            {
                //check to see if SQL connection is opn
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("UpdateFaculty", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", hfPersonID.Value == "" ? 0 : Convert.ToInt32(hfPersonID.Value));
                sqlCmd.Parameters.AddWithValue("@fname", txtFirstName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@lname", txtLastName.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@dob", txtDOB.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                string PersonID = hfPersonID.Value;
                Clear();
                if (PersonID == "")
                    SuccessMessage.Text = "Record Saved Successfully!";
                else
                    SuccessMessage.Text = "Record Updated Successfully!";
                FillGridView();
            }

        }
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {

            ButtonSave.Visible = false;
            ButtonSave.Enabled = false;
            ButtonClear.Visible = false;
            ButtonClear.Enabled = false;
            ButtonDelete.Visible = false;
            ButtonDelete.Enabled = false;
            ButtonConfirmDelete.Visible = true;
            ButtonConfirmDelete.Enabled = true;
            ButtonCancel.Visible = true;
            ButtonCancel.Enabled = true;
             
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        { 
            Clear();
        }

        public void Clear()
        {
            personForm.Visible = false;
            personForm.Enabled = false;

            PersonID = 0;
            hfPersonID.Value = "";
            facultyID.Text = txtFirstName.Text = txtLastName.Text = txtEmail.Text = txtPhone.Text = "";
            SuccessMessage.Text = ErrorMessage.Text = "";

            if (Login.getAccessLevel() >= 5)
            {
                

                ButtonCreateNew.Visible = true;
                ButtonCreateNew.Enabled = true;
                ButtonConfirmAdd.Visible = false;
                ButtonConfirmAdd.Enabled = false;
                ButtonDelete.Enabled = false;
                ButtonDelete.Visible = false;
                ButtonConfirmDelete.Visible = false;
                ButtonConfirmDelete.Enabled = false;
                ButtonCancel.Visible = false;
                ButtonCancel.Enabled = false;
                ButtonSave.Visible = false;
                ButtonSave.Enabled = false;
                ButtonSave.Text = "Save";
                ButtonClear.Visible = false;
                ButtonClear.Enabled = false;
            }  
        }


        protected void ButtonCancel_Click(object sender, EventArgs e)
        {         
            Clear();
        }

        protected void ButtonConfirmDelete_Click(object sender, EventArgs e)
        {
            if (Login.getAccessLevel() >= 5)
            {
                if (!IsPostBack)
                {
                    ButtonDelete.Enabled = false;
                    FillGridView();
                }
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("DeleteFacultyByID", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(hfPersonID.Value));
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                Clear();
                FillGridView();
                SuccessMessage.Text = "Record Deleted Successfully.";
            }
        }

        protected void lnk_OnClick(object sender, EventArgs e)
        {
            personForm.Visible = true;

            if (Login.getAccessLevel() >= 5)
            {
                personForm.Enabled = true;

                ButtonSave.Text = "Update";
                ButtonSave.Visible = true;
                ButtonSave.Enabled = true;
                ButtonDelete.Enabled = true;
                ButtonDelete.Visible = true;
                ButtonClear.Visible = true;
                ButtonClear.Enabled = true;
                ButtonCreateNew.Visible = false;
                ButtonCreateNew.Enabled = false;
                ButtonConfirmAdd.Visible = false;
                ButtonConfirmAdd.Enabled = false;
                ButtonConfirmDelete.Visible = false;
                ButtonConfirmDelete.Enabled = false;
                ButtonCancel.Visible = false;
                ButtonCancel.Enabled = false;
            }

            PersonID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("SelectFacultyByID", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@facID", PersonID);
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            hfPersonID.Value = PersonID.ToString();
            facultyID.Text = PersonID.ToString();
            txtLastName.Text = dtbl.Rows[0]["lastName"].ToString();
            txtFirstName.Text = dtbl.Rows[0]["firstName"].ToString();
            txtDOB.Text = dtbl.Rows[0]["birthDate"].ToString();
            txtEmail.Text = dtbl.Rows[0]["emailAddress"].ToString();
            txtPhone.Text = dtbl.Rows[0]["phoneNumber"].ToString();

            

        }
    }
}