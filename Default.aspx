<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" ClientIDMode="Static" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        #rightPanel ul {
            color: #1071a2 !important;
            text-decoration: underline;
        }

        .mainDiv {
            padding-top: 20px;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {

            var recentArray = LoadFromLocalStorage("recent") != "" ? LoadFromLocalStorage("recent") : null;
            recentArray = JSON.parse(recentArray);
            var str = "";
            str += "<h1>Recent Search</h1>";
            str += "<div class='row'>";
            var ctr = 0;

            if (recentArray != null) {
                $(recentArray).each(function () {
                    
                    if (ctr % 4 == 0)
                        str += "</div><br><br><div class='row'>";

                    str += "<div class='col-md-2 ' ><div style='border:1px solid rgb(134, 134, 134);padding:2px'><a class='newProductMenuBlock' href='/" + this.name.replace(" ", "").toLowerCase() +
                "'><img class='img-responsive' style='width:100%;height:170px' src='/images/identity/" + this.id + ".jpg'><span class='ProductMenuCaption'><i class='fa fa-caret-right'></i>" + this.name + "</span></a></div></div>";
                    ctr++;

                });

                str += "</div>";

                $("#divRecent").html(str);

            }

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainDiv">

        <%--<div id="leftPanel" class="col-md-4" runat="server">
        </div>--%>
        <div class="row">
            <div id="rightPanel" class="col-md-12" runat="server">
            </div>
        </div>
        <br />
        <br />
        <br />
        <br />

        <div class="row">
            <div id="divRecent" class="col-md-12" runat="server">
            </div>
        </div>


    </div>

</asp:Content>

