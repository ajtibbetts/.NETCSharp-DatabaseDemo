using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CapstoneWebApp
{
    public partial class StringSplit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            splitString();
        }

        protected void splitString()
        {

            string phrase = "The!quick!brown!fox!jumps!over!the!lazy!dog.";
            string[] words = phrase.Split('!');

            foreach (var word in words)
            {
                System.Diagnostics.Debug.WriteLine($"{word}");
            }
        }
    }
}