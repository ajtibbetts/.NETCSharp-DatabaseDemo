using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Security;

namespace CapstoneWebApp
{
    public partial class CourseRecords : System.Web.UI.Page
    {

        readonly SqlConnection sqlCon = new SqlConnection(Login.getConnStr());
        int recordID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.User.Identity.IsAuthenticated)
            {
                Login.setAccessLevel(0);
                FormsAuthentication.RedirectToLoginPage();
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
            // if admin or mod, display all records
            if (Login.getAccessLevel() >= 5)
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("ViewAllRecords", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                sqlCon.Close();
                gvRecords.DataSource = dtbl;
                gvRecords.DataBind();
            }
            // if student, display that students records
            if (Login.getAccessLevel() == 2)
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("ViewRecordsByStudentID", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@sid", Login.getUserID());
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                sqlCon.Close();
                gvRecords.DataSource = dtbl;
                gvRecords.DataBind();
            }
            if (Login.getAccessLevel() == 3)
            {

                SqlDataAdapter sqlDa = new SqlDataAdapter("ViewRecordsByFacultyID", sqlCon);
                sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
                sqlDa.SelectCommand.Parameters.AddWithValue("@id", Login.getUserID());
                DataTable dtbl = new DataTable();
                sqlDa.Fill(dtbl);
                sqlCon.Close();
                gvRecords.DataSource = dtbl;
                gvRecords.DataBind();
            }
        }

        void fillCreateLists()
        {

            //check to see if SQL connection is opn
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();

            // populate course dropdown
            string com = "Select CourseName, CourseID  FROM Courses";
            SqlDataAdapter cadpt = new SqlDataAdapter(com, sqlCon);
            DataTable cdt = new DataTable();
            cadpt.Fill(cdt);
            DropDownCourses.DataSource = cdt;
            // DropDownCourses.DataBind();
            DropDownCourses.DataTextField = "CourseName";
            DropDownCourses.DataValueField = "CourseID";
            DropDownCourses.DataBind();
           
            
            // populate student dropdown
            com = "Select StudentID, (lastName + ', ' + firstName) AS StudentName from Students";
            SqlDataAdapter sadpt = new SqlDataAdapter(com, sqlCon);
            DataTable sdt = new DataTable();
            sadpt.Fill(sdt);
            DropDownStudents.DataSource = sdt;
            DropDownStudents.DataBind();
            DropDownStudents.DataTextField = "StudentName";
            DropDownStudents.DataValueField = "StudentID";
            DropDownStudents.DataBind();

            // populate faculty dropdown
            com = "Select FacultyID, (lastName + ', ' + firstName) AS FacultyName from Faculty";
            SqlDataAdapter fadpt = new SqlDataAdapter(com, sqlCon);
            DataTable fdt = new DataTable();
            fadpt.Fill(fdt);
            DropDownFaculty.DataSource = fdt;
            DropDownFaculty.DataBind();
            DropDownFaculty.DataTextField = "FacultyName";
            DropDownFaculty.DataValueField = "FacultyID";
            DropDownFaculty.DataBind();

            sqlCon.Close();

        }



        protected void ButtonCreateNew_Click(object sender, EventArgs e)
        {
            ButtonCreateNew.Enabled = false;
            ButtonCreateNew.Visible = false;
            ButtonConfirmAdd.Enabled = true;
            ButtonConfirmAdd.Visible = true;
            ButtonCancel.Enabled = true;
            ButtonCancel.Visible = true;
            
            // personForm.Visible = true;
            // personForm.Enabled = true;

            CreateListControls.Visible = true;
            CreateListControls.Enabled = true;

            fillCreateLists();

        }
        protected void ButtonConfirmAdd_Click(object sender, EventArgs e)
        {
            if (Login.getAccessLevel() >= 5)
            {
                //check to see if SQL connection is opn
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                SqlCommand sqlCmd = new SqlCommand("AddNewRecord", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@cid", Convert.ToInt32(DropDownCourses.SelectedValue));
                sqlCmd.Parameters.AddWithValue("@sid", Convert.ToInt32(DropDownStudents.SelectedValue));
                sqlCmd.Parameters.AddWithValue("@fid", Convert.ToInt32(DropDownFaculty.SelectedValue));
                sqlCmd.Parameters.AddWithValue("@cdate", datepicker.SelectedDate.ToShortDateString());
                sqlCmd.Parameters.AddWithValue("@cgrade", Convert.ToInt32(DropDownGrade.SelectedValue));
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                string PersonID = hfID.Value;
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
                SqlCommand sqlCmd = new SqlCommand("UpdateRecord", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", hfID.Value == "" ? 0 : Convert.ToInt32(hfID.Value));
                sqlCmd.Parameters.AddWithValue("@cid", Convert.ToInt32(TextCID.Text.Trim()));
                sqlCmd.Parameters.AddWithValue("@sid", Convert.ToInt32(TextSID.Text.Trim()));
                sqlCmd.Parameters.AddWithValue("@fid", Convert.ToInt32(TextFID.Text.Trim()));
                sqlCmd.Parameters.AddWithValue("@cdate", txtCDate.Text.Trim());
                sqlCmd.Parameters.AddWithValue("@cgrade", Convert.ToInt32(TextCGrade.Text.Trim()));
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                string PersonID = hfID.Value;
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

            CreateListControls.Visible = false;
            CreateListControls.Enabled = false;

            recordID = 0;
            hfID.Value = "";
            txtID.Text = "";
            txtID.Text = TextCID.Text = txtCName.Text = txtCDate.Text = TextCGrade.Text = TextSID.Text = TextSFName.Text = TextSLName.Text = TextFID.Text = TextFFName.Text = TextFLName.Text = "";
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
                SqlCommand sqlCmd = new SqlCommand("DeleteRecordByID", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(hfID.Value));
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

            recordID = Convert.ToInt32((sender as LinkButton).CommandArgument);
            if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("[SelectRecordByID]", sqlCon);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@id", Convert.ToInt32(recordID));
            DataTable dtbl = new DataTable();
            sqlDa.Fill(dtbl);
            sqlCon.Close();
            hfID.Value = recordID.ToString();
            txtID.Text = recordID.ToString();
            TextCID.Text = dtbl.Rows[0]["CourseID"].ToString();
            txtCName.Text = dtbl.Rows[0]["CourseName"].ToString();
            txtCDate.Text = dtbl.Rows[0]["CourseDate"].ToString();
            TextCGrade.Text = dtbl.Rows[0]["studentGrade"].ToString();
            TextSID.Text = dtbl.Rows[0]["StudentID"].ToString();
            TextSFName.Text = dtbl.Rows[0]["firstName"].ToString();
            TextSLName.Text = dtbl.Rows[0]["lastName"].ToString();
            TextFID.Text = dtbl.Rows[0]["FacultyID"].ToString();
            TextFFName.Text = dtbl.Rows[0]["FFirstName"].ToString();
            TextFLName.Text = dtbl.Rows[0]["FLastName"].ToString();
        }
    }
}