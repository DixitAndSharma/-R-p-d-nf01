<%@ Page Title="" Language="C#" MasterPageFile="~/Edit/edit.master" AutoEventWireup="true" CodeFile="Identity.aspx.cs" Inherits="Edit_Identity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/jquery-ui-autocomplete.min.js"></script>
    <link href="../css/jquery-ui-1.10.2.custom.min.css" rel="stylesheet" />

    <script>

        $(document).ready(function () {
            GetIdentity();

            $(".fancybox").fancybox();

            $(".txtArea").autocomplete({
                maxheight: 40, maxwidth: 40,
                minLength: 1, select: function (event, ui) {

                    $.ajax({

                        cache: false, async: false, url: "/Data.aspx?Action=LinkAreas&Data1=" + ui.item.id + "&Data2=" + $(this).data('id'), success: function (data) {

                            GetIdentity();

                        }
                    });

                    $("#txtParentId").val(ui.item.id);

                }, source: SendRequest, autoFocus: true
            });

        });

        function UnlinkAreas(areaId, identityId) {
            $.ajax("/Data.aspx?Action=UnlinkAreas&Data1=" + areaId + "&Data2=" + identityId, function () {

                GetIdentity();

            });
        }

        function SendRequest(request, response) {
            var term = request.term;

            lastXhr = $.getJSON("/Data.aspx?Action=SearchAreas", request, function (data, status, xhr) {
                if (xhr === lastXhr)
                    response(data);
            });
        }

        function GetIdentity() {
            

            $.fancybox.close();
            $.ajax({
                cache: false, async: false, url: "/Data.aspx?Action=GetIdentity", success: function (data) {

                    var str = "<table class='table table-bordered'><tr><td>Id<td>Name<td>Area";
                    var lines = data.split("~");

                    for (var i = 1; i < lines.length - 1; i++) {

                        var f = lines[i].split("^");

                        console.log(f);

                        str += "<tr><td><a title='Identity Edit' class='fancybox fancybox.iframe' href='/Edit/IdentityEdit.aspx?id="
                    + f[0] + "&Title=Identity Edit'>" + f[0] + "</a><td>" + f[1] + "<td>" + f[2] + " <input type='text' data-id='" + f[0] + "' class='txtArea' />";
                    }

                    str += "<tr><td colspan='3'><a title='Identity Edit' class='btn btn-success fancybox fancybox.iframe' href='/Edit/IdentityEdit.aspx?id=0"
                    + "&Title=Identity Edit'>Add Identity</a>";

                    str += "</table>";

                    $("#divIdentity").html(str);
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="divIdentity"></div>
</asp:Content>

