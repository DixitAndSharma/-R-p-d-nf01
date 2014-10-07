<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Routing" %>


<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup
        InitializeRoutes(RouteTable.Routes);

        ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition
        {
            Path = "~/xrm-adx/js/jquery-1.8.3.min.js",
            DebugPath = "~/xrm-adx/js/jquery-1.8.3.min.js"
        });
    }

    private void InitializeRoutes(RouteCollection routes)
    {
        routes.Ignore("favicon.ico");
        routes.Ignore("WebResource.axd");

        routes.MapPageRoute(routeName: "default", routeUrl: "", physicalFile: "~/Default.aspx", checkPhysicalUrlAccess: true, defaults: new RouteValueDictionary() { });
        routes.MapPageRoute(routeName: "default1", routeUrl: "{Action}", physicalFile: "~/Default.aspx", checkPhysicalUrlAccess: true, defaults: new RouteValueDictionary() { { "Action", "" } });
    }


    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
