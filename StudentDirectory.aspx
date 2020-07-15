<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentDirectory.aspx.cs" Inherits="CapstoneWebApp.StudentDirectory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    
<%--            Create a grid to display all rows--%>
    <div style="text-align:left;float:left;">
            <asp:GridView ID="gvCourses" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField DataField="StudentID" HeaderText="StudentID" />
                    <asp:BoundField DataField="firstName" HeaderText="First Name" />
                    <asp:BoundField DataField="lastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="birthDate" HeaderText="DOB" />
                    <asp:BoundField DataField="emailAddress" HeaderText="Email Address" />
                    <asp:BoundField DataField="phoneNumber" HeaderText="phoneNumber" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("StudentID") %>' OnClick="lnk_OnClick" >View</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>


<asp:Panel ID="userControls" runat="server">
        <div style="text-align:left;float:left;margin-left:32px;">
        <asp:Panel ID="buttonsDash" runat="server">
            <asp:HiddenField ID="hfID" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Label ID="controlsLabel" runat="server" Text="User Controls" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button Enabled="false" Visible="false" ID="ButtonCreateNew" runat="server" OnClick="ButtonCreateNew_Click" Text="Create New" />
                    </td>
                    <td>
                        <asp:Button Enabled="false" Visible="false" ID="ButtonConfirmAdd" runat="server" OnClick="ButtonConfirmAdd_Click" Text="Confirm Add" />
                    </td>
                    <td>
                        <asp:Button Enabled="false" Visible="false" ID="ButtonSave" runat="server" OnClick="ButtonSave_Click" Text="Save" />
                    </td>
                    <td>
                            <asp:Button Enabled="false" Visible="false" ID="ButtonDelete" runat="server" Text="Delete" OnClick="ButtonDelete_Click" />
                    </td>
                    <td>
                            <asp:Button Enabled="false" Visible="false" ID="ButtonClear" runat="server" Text="Clear" OnClick="ButtonClear_Click" />
                    </td>
                    <td>
                            <asp:Button Enabled="false" Visible="false" ID="ButtonConfirmDelete" runat="server" Text="Confirm Delete" OnClick="ButtonConfirmDelete_Click" />
                    </td>
                    <td>
                            <asp:Button Enabled="false" Visible="false" ID="ButtonCancel" runat="server" Text="Cancel" OnClick="ButtonCancel_Click" />
                    </td>
                    
                    </tr>
                <tr>
                    <td colspan="2">
<%--            Create success and error messages--%>

                    <asp:Label ID="SuccessMessage" runat="server" Text="" ForeColor="Green" style="font-style: normal; font-weight: 700; font-family: Arial, Helvetica, sans-serif;"></asp:Label>
                    <asp:Label ID="ErrorMessage" runat="server" Text="" ForeColor="Red" style="font-style: normal; font-weight: 700; font-family: Arial, Helvetica, sans-serif;"></asp:Label>
                </td>
                    </tr>
            </table>
            </asp:Panel>
            <br />
        <asp:Panel ID="personForm" runat="server" Visible="false" Enabled="false">

            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:Label id="LabelID" Text="Course ID: " runat="server" CssClass="auto-style2" />
                    </td>
                    <td>
                        <asp:Label id="txtID" Text="" runat="server" CssClass="auto-style2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelName" runat="server" Text="First Name: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelLastName" runat="server" Text="Last Name: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLastName" runat="server" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelDOB" runat="server" Text="DOB: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDOB" runat="server" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelEmail" runat="server" Text="Email: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEmail" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelPhone" runat="server" Text="Phone: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPhone" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                
                
            </table>
            </asp:Panel>
            <br />
            
            </div>
</asp:Panel>
     


</asp:Content>
