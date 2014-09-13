<%@ Page Title="" Language="C#" MasterPageFile="~/Edit/edit.master" AutoEventWireup="true" CodeFile="Area.aspx.cs" Inherits="Edit_Area" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>

        $(document).ready(function () {
            GetAreas();

            $(".fancybox").fancybox();
        });

        function GetAreas() {

            $.fancybox.close();
            $.ajax({
                cache: false, async: false, url: "/Data.aspx?Action=GetAreas", success: function (data) {

                    var str = "<table class='table table-bordered'><tr><td>Id<td>Name<td>Parent";
                    var lines = data.split("~");

                    for (var i = 1; i < lines.length - 1; i++) {

                        var f = lines[i].split("^");

                        console.log(f);

                        str += "<tr><td><a title='Area Edit' class='fancybox fancybox.iframe' href='/Edit/AreaEdit.aspx?id="
                    + f[0] + "&Title=Area Edit'>" + f[0] + "</a><td>" + f[1] + "<td>" + f[3];
                    }

                    str += "<tr><td colspan='3'><a title='Area Edit' class='btn btn-success fancybox fancybox.iframe' href='/Edit/AreaEdit.aspx?id=0"
                    + "&Title=Area Edit'>Add Area</a>";

                    str += "</table>";

                    $("#divAreas").html(str);
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divAreas"></div>
</asp:Content>

