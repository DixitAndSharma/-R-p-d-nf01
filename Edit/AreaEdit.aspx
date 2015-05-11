<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AreaEdit.aspx.cs" Inherits="Edit_AreaEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../js/jquery-1.9.1.min.js"></script>
    <link href="../css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" />
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <script src="../js/jquery-ui-autocomplete.min.js"></script>

    <script>
        $(document).ready(function () {

            $("#txtParentName").autocomplete({
                maxheight: 40, maxwidth: 40,
                minLength: 1, select: function (event, ui) {
                    
                    $("#txtParentId").val(ui.item.id);

                }, source: SendRequest, autoFocus: true
            });


        });

        function SendRequest(request, response) {
            var term = request.term;

            lastXhr = $.getJSON("/Data.aspx?Action=SearchAreas", request, function (data, status, xhr) {
                if (xhr === lastXhr)
                    response(data);
            });
        }

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
                    <td>Parent
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="txtParentName"></asp:TextBox>
                        <asp:HiddenField runat="server" ID="txtParentId" />
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
                    <td>
                    </td>
                    <td>
                        <asp:Button Text="Save" runat="server" OnClick="Unnamed1_Click" />                    </td>
                </tr>

            </table>
        </div>
    </form>
</body>
</html>
