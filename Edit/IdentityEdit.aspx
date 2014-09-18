<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IdentityEdit.aspx.cs" Inherits="Edit_AreaEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.9.1.min.js"></script>
    <link href="../css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/docs.min.css" rel="stylesheet" />
    <link href="../css/site.css" rel="stylesheet" />
    <script src="../js/jquery.min.js"></script>
    <link href="/css/jquery.cleditor.css" rel="stylesheet" />
    <script src="/js/jquery.cleditor.min.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            try {
                var HTMLEDITCONT = "bold italic underline strikethrough subscript superscript | font size style | color highlight removeformat | undo redo | link unlink | cut copy paste pastetext | print source";
                $("#txtDetails").cleditor({ width: '100%', height: 200, useCSS: true });

            } catch (e) {
                console.log(e);
            }

        });

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="table">
                <tr>
                    <td>Id</td>
                    <td>
                        <asp:Label runat="server" ID="lblId"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td>Name
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtName"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Details
                    </td>
                    <td>
                        <textarea id="txtDetails" rows="4" runat="server" width="95%"></textarea>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblImage"></asp:Label></td>
                    <td>
                        <asp:FileUpload ID="FileUpload" runat="server" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only image files are allowed!" ForeColor="Red" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.jpeg|.gif|.bmp)$" ControlToValidate="FileUpload"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </td>
                </tr>

                <tr>
                    <td></td>
                    <td>
                        <asp:Button Text="Save" CssClass="btn btn-success" runat="server" OnClick="Unnamed1_Click1" />
                    </td>
                </tr>

                

            </table>
        </div>
    </form>
</body>
</html>
