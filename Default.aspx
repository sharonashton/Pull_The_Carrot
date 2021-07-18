<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<%--חיבור ל CSS--%>
<link href="Style/StyleSheet.css" rel="stylesheet" />
<link href="Style/menue.css" rel="stylesheet" />

<%--חיבור לJAVASCRIPT--%>
<script src="jscripts/jquery-1.12.0.min.js"></script>
<script src="jscripts/JavaScriptDefault.js"></script>
<script src="jScripts/menu.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">

    
           

<head runat="server">
    <title>המשחקים שלי</title>

</head>
<body>
           <header>
                   <!--קישור לדף עצמו כדי להתחיל את המשחק מחדש בלחיצה על הלוגו-->
            <a href="index.html">
                <img src="images/Untitled-1.png" /> <!--הלוגו של המשחק שלכם-->
                <!--<p>משוך בגזר</p>-->
            </a>
            <!--תפריט הניווט בראש העמוד-->
            <nav>
                <ul>
                     <li><a class="helpFalse">עזרה</a></li>
                    <li><a class="about">אודות</a></li>
                    <li><a href="index.html" >למשחק</a></li>
                   
                </ul>
            </nav>
        </header>
         <div id="aboutDiv" class="popUp bounceInDown hide">
            
                <a class="closeAbout">X</a>
                <p class="headP">אודות</p>
   
            <img class="imgtop" src="images/Untitled-1.png" />
           
                <p id="madeby"><b>אפיון ופיתוח</b>:</p>        
            
            <p class="names" id="noam">
                נועם אהרון

            </p>

            <p class="names" id="sharon">
                שרון אשתון

            </p>

            <p class="names" id="limor">
                לימור גולד

            </p>
            <p>
                <img class="Pics" id="noamPic" src="images/noam.png" />
                <img class="Pics" id="sharonPic" src="images/sharon.png" />
                <img class="Pics" id="limorPic" src="images/limor.png" />

            </p>
               
                        <p >
                אופיין ופותח במסגרת פרויקט בקורסים:
             </p> <p>
                 סביבות לימוד אינטראקטיביות 2 (13122 ) +תכנות אינטראקציה ואנימציה 2 (13131 ) +תכנות 2 (13211)
 </p><p>תשפ"א</p>
            <a class="HIT" href="http://www.hit.ac.il/">  <img class="HIT" src="images/HIT.png" /> </a>
            <p class="Telem"> <a class="Telem" href="http://www.hit.ac.il/telem/overview">  הפקולטה לטכנולוגיית למידה</a></p>
        </div>

 <form id="form1" runat="server">

    <asp:Panel ID="backWindow" runat="server"></asp:Panel>

        <asp:Panel ID="popUpCheck" runat="server">
         
            <div id="textDelete">
         <asp:Label ID="checkDelite" runat="server" Text=" "></asp:Label>
         <br />
        
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="yesDelite" runat="server" OnClick="yesDelite_Click" Text="כן" />
      
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="noDelite" runat="server" OnClick="noDelite_Click" Text="לא" />
    </div>
             </asp:Panel>

     <div id="setHead" class="headers"> <asp:Label ID="Label1" runat="server" Text="המשחקים שלי"></asp:Label></div>
<div id="addNewGame">

    <asp:Button  ID="addGame" runat="server" Text="הוסף משחק" OnClick="addGame_Click" />
        </div>
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Panel ID="settingsinfoP1" runat="server"> <img id="settingsinfoIMG" src="images/!!.png" width="18px;" /></asp:Panel>
     
         <asp:Panel ID="settingsinfoP" runat="server">
            
             <asp:Label ID="infoTXT" runat="server" Text="הגדרת זמן, אופציית משחק, שם המשחק ומשוב סיכום"></asp:Label>
         </asp:Panel>
     <div id ="grid1">
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile.xml" XPath="/pullTheCarrot/game"></asp:XmlDataSource>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" OnRowCommand="GridView1_RowCommand" CellPadding="1" CellSpacing="1" HorizontalAlign="Center" TabIndex="-1" ShowHeaderWhenEmpty="True">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                 <asp:TemplateField HeaderText="שם המשחק">
                  <ItemTemplate>
                      <asp:Label ID="gameName" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "GameSubject").ToString())%>'></asp:Label>
                  </ItemTemplate>
                     <ItemStyle Width="300px" />
                </asp:TemplateField>

                              <asp:TemplateField HeaderText="קוד המשחק">
                   <ItemTemplate>
                       <asp:Label ID="gamCode" runat="server" Text='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>'></asp:Label>
                   </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

               <asp:TemplateField HeaderText="כמות שאלות">
                  <ItemTemplate>
                      <asp:Label ID="numQuestion" runat="server" itemID='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' Text= '<%#XPathBinder.Eval(Container.DataItem,"count(questions/question)").ToString()%>'></asp:Label>
                  </ItemTemplate>

                   <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>

                <asp:TemplateField HeaderText="הגדרות" ItemStyle-BorderStyle="NotSet">
                   
                  <ItemTemplate>
                      <asp:ImageButton ID="gameSettings" runat="server" ImageUrl="~/images/setting.png" Width="24" Height="24" CommandName="setRow" itemID='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>'/>
                     
                  </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="עריכה">
                 <ItemTemplate>
                     <asp:ImageButton ID="gameEdit" runat="server"  ImageUrl="~/images/edit.png" CommandName="editRow" itemID='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>'/>
                 </ItemTemplate>

                     <ItemStyle HorizontalAlign="Center" />

                </asp:TemplateField>
           
                 <asp:TemplateField HeaderText="מחיקה">
                 <ItemTemplate>
                  <asp:ImageButton ID="gameDelite" runat="server" ImageUrl="~/images/delite.png" CommandName="deleteRow" itemID='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' />
                 </ItemTemplate>
                     <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                                <asp:TemplateField HeaderText="פרסום">
                    <ItemTemplate>
                        <div id="toolcancel"class="tooltip">
                        <asp:CheckBox ID="isPublish" CssClass="notPublished" OnCheckedChanged="isPublish_CheckedChanged" runat="server" itemID='<%#XPathBinder.Eval(Container.DataItem, "@GameCode").ToString()%>' Checked='<%#Convert.ToBoolean(XPathBinder.Eval(Container.DataItem, "@isPublish"))%>' AutoPostBack="true"/>
                            <asp:Label ID="tooltiptxt" class="tooltipTxt" runat="server" Text="דרושות לפחות 10 שאלות לפרסום"></asp:Label>
                            </div>
                            </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#F19E65" />
        </asp:GridView>
         </div>



         <%--<asp:Panel ID="backWindow" runat="server"></asp:Panel>--%>
   
  
     <asp:Panel ID="unPublished" runat="server">
         <asp:Label ID="hoverPublish" runat="server" Visible="false" Text="דרושות לפחות 10 שאלות לפרסום"></asp:Label>
     </asp:Panel>

     <asp:Panel ID="HoverSettings" runat="server" > 
         <asp:Label ID="settingsHover" runat="server" Visible="false" Text="הגדרת זמן, אופציית משחק, שם המשחק ומשוב סיום"></asp:Label>
     </asp:Panel>

    </form>
</body>
</html>
