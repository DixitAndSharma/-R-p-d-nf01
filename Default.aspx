<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .profileImg {
            height: 380px;
            padding-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="mainDiv container">

        <div class="col-lg-3" id="leftPanel" runat="server">
        </div>
        <div id="rightPanel" class="col-lg-9" runat="server"></div>

    </div>
</asp:Content>

