using System;
using System.Web.UI;
using ClassLibrary;

namespace AspNetWebApp
{
    public partial class About : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var firstName = "Jeff";

            // Save to session state in a Web Forms page class.
            Session["FirstName"] = firstName;
            //Session["LastName"] = lastName;
            //Session["City"] = city;
            Session["Foo"] = new Foo { Name = "John", Bar = "Doe" };
        }
    }
}