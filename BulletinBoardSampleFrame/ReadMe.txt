Constants Folder::
In Code::
    using BulletinBoardSampleFrame.CommonConstants;

	int status =1;
    if(status== PostConst.stauts_active)
    {
        Debug.WriteLine("True");
    }

Utility Folder:::
In Code::
    using OJTBulletinBoard.ValidationUtility;

    PostValidtion postValidation = new PostValidtion();
    bool passwordValidation = postValidation.CheckPasswordValidation(password, newPassword, confirmPassword);
    if (passwordValidation == true)
    {
        something
    }
    else
    {
         something
    }

DAO Folder:::
    Mainly used for retrieving and inserting data from database

Services Folder:::
    Mainly used between controller calss and DAO class.
    All condition are placed in this folder.

Resource Folder:::
    Mainly use for image,external css and external ja file

    CSS:::
    In View:::
         @section usage{
            <link href="~/Resource/css/table.css" rel="stylesheet" />
          }

    In Layout Page:::
        @RenderSection("usage", required: false)

    JS:::
    In View:::
        @section script
        {
             <script src="~/Resource/js/JavaScript.js"></script>
        }

    In Layout Page:::
        @RenderSection("script", required: false)

ViewModel:::
    It mainly reponsible for getter and setter method that are not related to database.
    Sometimes it's used as a join table 


Route Config:::
    We don't use defaults when creating Route Constraint to a Set of Specific Values
        eg.: In RegisterRoute
              routes.MapRoute(
              "Default", // Route name
              "{controller}/{action}/{id}", // Route Pattern
              new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Default values for parameters
              new { controller = "^H.*", action = "^Details$|^About$" } //Restriction for controller and action
           );

    We can also use the custom route
        eg.: In In RegisterRoute
                routes.MapRoute(
                name: "Post",
                url: "Post/{id}",
                defaults: new { controller = "Post", action = "PostView"}
            );

Layout Page::
    In Layout page name, "_" means it is not intented to be served directly in web page. And web 
    Pages framework has been configured not to allow files with leading underscores in their names 
    from being requested directly.

    How to use::
        In Layout Page::
            @Html.Partial("_TopLayout")

Changing the folder name::
    We can change the folder name by using "Directory.Move()" but the folder name will become hidden folder name
        eg.::   using System.IO namespace;

                    string path = Server.MapPath("~");
                    string Fromfol = "\\[Foldername]\\";
                    string Tofol = "\\[Foldername]\\";
                    Directory.Move(path + Fromfol, path + Tofol);