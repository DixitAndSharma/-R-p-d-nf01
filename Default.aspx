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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainDiv">

        <%--<div id="leftPanel" class="col-md-4" runat="server">
        </div>--%>
        <div id="rightPanel" class="col-md-12" runat="server">
        </div>

    </div>
</asp:Content>

