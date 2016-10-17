<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="StyleSheet.css" rel="stylesheet" />
</head>
 
<body>
    <header><h3>WebSphere MQ overvågning</h3></header>
    <form id="form1" runat="server">
    <div class="box">
     <table >
         <tr>
             <td>
                   q1
             </td>
         </tr>
         <tr>
             <td>
                   kø dybde
             </td>
         </tr>
         <tr>
             <td>
                   kø alder
             </td>
         </tr>
    </table>
    </div>
        <div class="box" >
            <table>
         <tr>
             <td id="tag" runat="server">
                   q2
             </td>
         </tr>
         <tr>
             <td>
                   kø dybde
             </td>
         </tr>
         <tr>
             <td>
                   kø alder
             </td>
         </tr>
    </table>
        </div>
    </form>
</body>
</html>
