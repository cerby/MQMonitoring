<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">


<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
    <%--<meta http-equiv="refresh"  content="10"/>--%>
</head>
 
<body>
    <header><h3>WebSphere MQ overvågning</h3></header>
    <form id="form1" runat="server">

        

        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate >
                <asp:Timer ID="Timer1" runat="server" Interval="10000" OnTick="Timer1_Tick">
                </asp:Timer>
                <div id="div" runat="server" class ="box">

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>

</body>
</html>
