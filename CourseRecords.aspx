<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseRecords.aspx.cs" Inherits="CapstoneWebApp.CourseRecords" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    

     <div style="text-align:left;float:left;">
            <asp:GridView ID="gvRecords" runat="server" AutoGenerateColumns="false" Width="878px">
                <AlternatingRowStyle BackColor="#CCCCCC" />
                <Columns>
                    <asp:BoundField DataField="RecordID" HeaderText="RecordID" />
                    <asp:BoundField DataField="courseID" HeaderText="Course ID" />
                    <asp:BoundField DataField="courseName" HeaderText="Course Name" />
                    <asp:BoundField DataField="studentID" HeaderText="Student ID" />
                    <asp:BoundField DataField="FirstName" HeaderText="Student Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Student Name" />
                    <asp:BoundField DataField="facultyID" HeaderText="Faculty ID" />
                    <asp:BoundField DataField="FFirstName" HeaderText="Faculty Name" />
                    <asp:BoundField DataField="FLastName" HeaderText="Faculty Name" />
                    <asp:BoundField DataField="courseDate" HeaderText="Course Date" />
                    <asp:BoundField DataField="studentGrade" HeaderText="Grade" />

                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("RecordID") %>' OnClick="lnk_OnClick" >View</asp:LinkButton>
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
                        <asp:Label id="LabelID" Text="Record ID: " runat="server" CssClass="auto-style2" />
                    </td>
                    <td>
                        <asp:Label id="txtID" Text="" runat="server" CssClass="auto-style2" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelCID" runat="server" Text="Course ID: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextCID" runat="server" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelCName" runat="server" Text="Course Name: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCName" Enabled="false" runat="server" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelCDate" runat="server" Text="Course Date: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCDate" runat="server" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="LabelCGrade" runat="server" Text="Student Grade: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextCGrade" runat="server" Height="23px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelSID" runat="server" Text="Student ID: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextSID" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelSFName" runat="server" Text="Student First: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextSFName" Enabled="false" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelSLName" runat="server" Text="Student Last: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextSLName" Enabled="false" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelFID" runat="server" Text="Instructor ID: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextFID" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelFFName" runat="server" Text="Instructor First: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextFFName" Enabled="false" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelFLName" runat="server" Text="Instructor Last: " CssClass="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextFLName" Enabled="false" runat="server" Height="26px"></asp:TextBox>
                    </td>
                </tr>
                
            </table>
            </asp:Panel>
            <asp:Panel ID="CreateListControls" runat="server" Visible="false" Enabled="false">
                <table class="auto-style1">
                    <tr>
                        <td>
                            <asp:Label ID="LabelDDC" runat="server" Text="Select Course: " CssClass="auto-style2"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownCourses" runat="server">   </asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="LabelDatepicker" runat="server" Text="Select Course Date: " CssClass="auto-style2"></asp:Label>
                        </td>
                        <td>
                            <Br />
                            <asp:Calendar ID="datepicker" runat="server"></asp:Calendar>
                            <Br />
                        </td>
                    </tr>
                    
                    <tr>
                        <td>
                            <asp:Label ID="LabelDDS" runat="server" Text="Select Student: " CssClass="auto-style2"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownStudents" runat="server">   </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelDDF" runat="server" Text="Select Faculty: " CssClass="auto-style2"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownFaculty" runat="server">  </asp:DropDownList> 
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelDDG" runat="server" Text="Select Grade: " CssClass="auto-style2"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownGrade" runat="server">
                                 <asp:listitem text="0" value="0"></asp:listitem>
                                 <asp:listitem text="1" value="1"></asp:listitem>
                                 <asp:listitem text="2" value="2"></asp:listitem>
                                 <asp:listitem text="3" value="3"></asp:listitem>
                                 <asp:listitem text="4" value="4"></asp:listitem>
                            </asp:DropDownList> 
                        </td>
                    </tr>
                
                
                </table>

            </asp:Panel>
            <br />
            
            </div>
</asp:Panel>

</asp:Content>
