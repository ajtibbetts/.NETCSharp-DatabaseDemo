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
    public partial class Login : System.Web.UI.Page
    {

        private static int accessLevel = 0;
        private static int userID = 0;
        static string connStr = @"Server=localhost\LOCALCAPSTONESQL;Initial Catalog=testDB;Integrated Security=SSPI";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginComplete(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void ValidateUser(object sender, AuthenticateEventArgs e)
        {
            int userStatus = 0;
            string constr = @"Server=localhost\LOCALCAPSTONESQL;Initial Catalog=testDB;Integrated Security=SSPI";
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Validate_Login"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Username", LoginForm.UserName);
                    cmd.Parameters.AddWithValue("@Password", LoginForm.Password);
                    cmd.Connection = con;
                    con.Open();
                    userStatus = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }
                switch (userStatus)
                {
                    case -1:
                        LoginForm.FailureText = "Username and/or password is incorrect.";
                        break;
                    case -2:
                        LoginForm.FailureText = "Account has not been activated.";
                        break;
                    default:
                        using (SqlCommand cmd = new SqlCommand("[getAccessLevel]"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Username", LoginForm.UserName);
                            cmd.Connection = con;
                            con.Open();
                            accessLevel = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }
                        using (SqlCommand cmd = new SqlCommand("[getUserID]"))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@Username", LoginForm.UserName);
                            cmd.Connection = con;
                            con.Open();
                            userID = Convert.ToInt32(cmd.ExecuteScalar());
                            con.Close();
                        }


                        FormsAuthentication.RedirectFromLoginPage(LoginForm.UserName, LoginForm.RememberMeSet);
                        
                        break;
                }
            }
        }

        public static int getAccessLevel()
        {
            
            return accessLevel;
        }

        public static int getUserID()
        {

            return userID;
        }

        public static void setAccessLevel(int i)
        {
            accessLevel = i;
        }

        public static string getConnStr()
        {
            return connStr;
        }

    }
}