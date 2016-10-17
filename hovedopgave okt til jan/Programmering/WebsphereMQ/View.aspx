<%@ Page Language="C#" AutoEventWireup="true" CodeFile="View.aspx.cs" Inherits="View" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="height: 410px">
    <form id="form1" runat="server">
    <div id="divdiv" runat="server">
    
    </div>
    
        <p>
            &nbsp;</p>
        <p>
            <asp:TextBox ID="TextBox1" runat="server" Width="626px"></asp:TextBox>
        </p>
        <p>
            &nbsp;</p>
        <p>
    
        <asp:Button ID="Button1" runat="server" Height="50px"  OnClick="Button1_Click" Text="Button" Width="189px" />
        </p>
        <p>
            &nbsp;</p>
        <asp:TextBox ID="TextBox2" runat="server" Width="632px"></asp:TextBox>
        <p>
            <asp:TextBox ID="TextBox3" runat="server" Width="629px"></asp:TextBox>
        </p>
    </form>
</body>
</html>
