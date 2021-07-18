<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Edit" %>
<!DOCTYPE html>

<link href="Style/StyleSheet.css" rel="stylesheet" />
<link href="Style/menue.css" rel="stylesheet" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  <%-- חיבור בין גאבה סקריפט לעמוד--%> 
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/JavaScriptEdit.js"></script>
    <script src="jScripts/menu.js"></script>

    
    
    <style type="text/css">
        .CharacterCount {}
        .auto-style1 {}
    </style>
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
        <%-- חלונית אזהרת מחיקה --%>
     <asp:Panel ID="backWindow" runat="server"></asp:Panel>
     <asp:Panel ID="popUpCheck" runat="server" Width="581px">
         <div id="textDelete">
         <asp:Label ID="checkDelite" runat="server" Text=" "></asp:Label>
         <br />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="yesDelite" runat="server" OnClick="yesDelite_Click" Text="כן" />
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             <asp:Button ID="noDelite" runat="server" OnClick="noDelite_Click" Text="לא" />
    </div>
             </asp:Panel> 

               <div id="container">                 
            <div id="headerGN">
             <asp:Label ID="gameNameTXT" runat="server" Text="Label"> </asp:Label><asp:Button ID="goBack" runat="server" OnClick="goBack_Click" Text="חזרה למסך הראשי" />                              
            </div>
                
  <%-- פקד להעלאה ראשון --%>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <%-- פקד להעלאה שני --%>
        <asp:FileUpload ID="FileUpload2" runat="server"/>
          <%-- פקד להעלאה שלישי --%>
        <asp:FileUpload ID="FileUpload3" runat="server"/>
          <%-- פקד להעלאה רביעי --%>
        <asp:FileUpload ID="FileUpload4" runat="server"/>
          <%-- פקד להעלאה חמישי --%>
        <asp:FileUpload ID="FileUpload5" runat="server"/>
                    <%-- פקד להעלאה שישי לשאלה --%>
        <asp:FileUpload ID="FileUpload0" runat="server"/>

          <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile.xml" XPath="/pullTheCarrot/game/questions/question"></asp:XmlDataSource>
             <asp:Panel ID="questionsTable1" runat="server">
                       <h1>השאלות</h1>
                       <p><a id="theQtable"></a> מינימום של<a class="greenT"> 10</a> שאלות </p>
                       <asp:Panel ID="theQNum" runat="server"> מספר השאלות:
                       <asp:Label ID="questionCount" runat="server" Text=""></asp:Label></asp:Panel>    
                 <%-- גרידוי פנימי --%>
             </asp:Panel>
                   <div id="grid2">
                   <asp:GridView ID="GridView2"  runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" OnRowCommand="GridView2_RowCommand" CellPadding="1" CellSpacing="1" HorizontalAlign="Left" style="margin-right: 55px" Width="700px" ShowHeaderWhenEmpty="True">
                       <AlternatingRowStyle BackColor="#FBE5D6" />
                       <Columns>
                           <asp:TemplateField HeaderText="השאלה">
                               <ItemTemplate>
                                 <asp:Label id="Ques" runat="server" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "questionText").ToString())%>' > </asp:Label>
                               </ItemTemplate>
                               <ItemStyle HorizontalAlign="Right" Width="400px" />
                           </asp:TemplateField>


                           <asp:TemplateField HeaderText="עריכה">
                                 <ItemTemplate>
                                     <asp:ImageButton ID="questionEdit" runat="server"  ImageUrl="~/images/edit.png" CommandName="editRowQ" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@id").ToString()%> ' />
                               </ItemTemplate>
                                 <ItemStyle HorizontalAlign="Center" Width="80px" />
                           </asp:TemplateField>

                           <asp:TemplateField HeaderText="מחיקה">
                                <ItemTemplate>
                                    <asp:ImageButton ID="questionDelet" runat="server" ImageUrl="~/images/delite.png"  CommandName="deletRowQ" theItemId='<%#XPathBinder.Eval(Container.DataItem, "@id").ToString()%>' />
                               </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="80px" />
                           </asp:TemplateField>

                       </Columns>
                       <HeaderStyle BackColor="#F19E65" />
                       <PagerStyle CssClass="grid2" />
                       <RowStyle HorizontalAlign="Center" />
                   </asp:GridView>
                   </div>
                   <%-- עריכת השאלות --%>
           <asp:Panel ID="QuestionAddEdit" runat="server">
             <h1> עריכת שאלות</h1>
               <p>השאלה - <a class="greenT">2-45</a>  <span class="auto-style1">תווים</span></p>
                <div class="firstQinline">
            <%-- עריכת השאלה --%>
           <asp:TextBox ID="theQuestion" CharacterLimit="45" CharacterForLabel="QuestCount" CssClass="CharacterCount" runat="server" Height="50px" Width="365
               px" Font-Names="Rubik" Font-Size="14pt" TextMode="MultiLine" ></asp:TextBox>
           

                + <asp:ImageButton ID="imagA0" runat="server" Height="45px" ImageUrl="~/images/imeg.png" OnClientClick="openFileUploader0();return false;"  Width="45px" />
                <asp:HiddenField ID="Hiddenvar0" runat="server" />
                &nbsp  <asp:Panel ID="PanelQuestionIMGEdit" CssClass="" runat="server">
                       <asp:ImageButton ID="EditImgQ" runat="server" ImageUrl="~/images/EditIMG.png" OnClientClick="openFileUploader0(); return false;" />
                       <br />
                       <asp:ImageButton ID="DeleteImgQ" runat="server" ImageUrl="~/images/DeleteIMG.png" OnClick="DeleteImgQ_Click" /><br />
                       <asp:ImageButton ID="ZoomImgQ" Cssclass="" runat="server" ImageUrl="~/images/ZoomIMG.png" Width="20px" OnClick="ZoomImgQ_Click"/>
                       </asp:Panel>
                </div>
                       <asp:Label ID="QuestCount" runat="server" CssClass="" Text="0/45"></asp:Label>
               <%--    עריכת המסיחים   --%>
            <div class="allEdit">
            <p>אפשרויות מענה (2 לפחות) –  <a class="greenT">2-20</a> <span class="auto-style1">תווים או תמונה </span> </p>
                <%-- מסיח ראשון --%>
             <div id="firstQA">      
            <div id="rightA">תשובה נכונה</div>   &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                 <asp:TextBox ID="rightAnswer"  CharacterLimit="20" CharacterForLabel="AnsCount1"  CssClass="CharacterCount"  runat="server" Font-Names="Rubik" Font-Size="14pt" Height="25px" Width="300px" style="border-radius: 15px">
                 </asp:TextBox>
                /
                <asp:ImageButton ID="imagA1" runat="server" Height="45px" ImageUrl="~/images/imeg.png" OnClientClick="openFileUploader1(); return false;" Width="45px" />
                 <%--<asp:Button ID="tempSave" runat="server" Text="save" OnClick="tempSave_Click" />--%>
                 <asp:HiddenField ID="Hiddenvar1" runat="server" />
                <asp:Label ID="AnsCount1" runat="server" CssClass="redT" Text="0/20"></asp:Label>
             </div>
                   <asp:Panel ID="change1" runat="server">
                   <asp:ImageButton ID="EditImg1" runat="server" ImageUrl="~/images/EditIMG.png" OnClientClick="openFileUploader1(); return false;"/>
                       <br />
                       <asp:ImageButton ID="DeleteImg1" runat="server" ImageUrl="~/images/DeleteIMG.png" OnClick="DeleteImg1_Click" /><br />
                       <asp:ImageButton ID="ZoomImg1" CssClass="" runat="server" ImageUrl="~/images/ZoomIMG.png" Width="20px" OnClick="ZoomImg1_Click" />
                       </asp:Panel>
                <%--מסיח שני--%>
                       <div id="secondQA">
          &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  <asp:TextBox ID="wrongAnswer"  class="textboxdesign" CharacterLimit="20" CharacterForLabel="AnsCount2" CssClass="CharacterCount" runat="server" Font-Names="Rubik" Font-Size="14pt" Width="300px" Height="25px"></asp:TextBox>
                          /
               <asp:ImageButton ID="imagA2" runat="server" Height="45px" ImageUrl="~/images/imeg.png" Width="45px" OnClientClick="openFileUploader2(); return false;"  />
                <asp:HiddenField ID="Hiddenvar2" runat="server" />
                 <br />
                   <asp:Label ID="AnsCount2" runat="server" Text="0/20" CssClass="redT"></asp:Label>
                     <asp:Panel ID="PanelA2IMGEdit" CssClass="" runat="server">
                       <asp:ImageButton ID="EditImgA2" runat="server" ImageUrl="~/images/EditIMG.png" OnClientClick="openFileUploader2(); return false;" />
                       <br />
                       <asp:ImageButton ID="DeleteImgA2" runat="server" ImageUrl="~/images/DeleteIMG.png" OnClick="DeleteImgA2_Click" /><br />
                       <asp:ImageButton ID="ZoomImgA2" Cssclass="" runat="server" ImageUrl="~/images/ZoomIMG.png" Width="20px" OnClick="ZoomImgA2_Click"/>
                       </asp:Panel>
                           </div>
                       <div id="extraA">
             <%--      מסיח מספר 3--%>
                <div id="thirdA">
                        <asp:Panel ID="answer3" runat="server">
                            <asp:ImageButton ID="deletAns1" runat="server" Height="29px" ImageUrl="~/images/היי.png" Width="28px" OnClick="deletAns1_Click"  />
                            &nbsp;&nbsp;&nbsp;&nbsp;   <asp:TextBox ID="wrongAnswer2" CharacterLimit="20" CharacterForLabel="AnsCount3" CssClass="CharacterCount" runat="server" Font-Names="Rubik" Font-Size="14pt" Height="25px" Width="300px"></asp:TextBox>
                            /  
                            <asp:ImageButton ID="imagA3" runat="server" Height="45px" ImageUrl="~/images/imeg.png" Width="45px"  OnClientClick="openFileUploader3(); return false;"  />
                           <asp:HiddenField ID="Hiddenvar3" runat="server" />
                            <br />
                            <asp:Label ID="AnsCount3" runat="server" Text="0/20" CssClass="redT"></asp:Label>
                        
                         <asp:Panel ID="PanelA3IMGEdit" CssClass="" runat="server">
                       <asp:ImageButton ID="EditImgA3" runat="server" ImageUrl="~/images/EditIMG.png" OnClientClick="openFileUploader3(); return false;" />
                       <br />
                       <asp:ImageButton ID="DeleteImgA3" runat="server" ImageUrl="~/images/DeleteIMG.png" OnClick="DeleteImgA3_Click" style="height: 22px" /><br />
                       <asp:ImageButton ID="ZoomImgA3" Cssclass="" runat="server" ImageUrl="~/images/ZoomIMG.png" Width="20px" OnClick="ZoomImgA3_Click"/>
                       </asp:Panel>
                            </asp:Panel>
                    </div>
           <%--        מסיח מספר 4--%>
                <div id="fourthA">
                          <asp:Panel ID="answer4" runat="server">
                              <br />
                              <asp:ImageButton ID="deletAns2" runat="server" Height="29px" ImageUrl="~/images/היי.png" OnClick="deletAns2_Click" Width="28px" />
                              &nbsp;&nbsp; &nbsp;  <asp:TextBox ID="wrongAnswer3" CharacterLimit="20" CharacterForLabel="AnsCount4" CssClass="CharacterCount" runat="server" Font-Names="Rubik" Font-Size="14pt" Height="25px" Width="300px"></asp:TextBox>
                              / 
                                <asp:ImageButton ID="imagA4" runat="server" Height="45px" ImageUrl="~/images/imeg.png" Width="45px"  OnClientClick="openFileUploader4(); return false;"  />
                          <asp:HiddenField ID="Hiddenvar4" runat="server" />
                              <br />
                            <asp:Label ID="AnsCount4" runat="server" Text="0/20" CssClass="redT"></asp:Label>
                               
                        <asp:Panel ID="PanelA4IMGEdit" CssClass="" runat="server">
                       <asp:ImageButton ID="EditImgA4" runat="server" ImageUrl="~/images/EditIMG.png" OnClientClick="openFileUploader4(); return false;" />
                       <br />
                       <asp:ImageButton ID="DeleteImgA4" runat="server" ImageUrl="~/images/DeleteIMG.png" OnClick="DeleteImgA4_Click" style="height: 22px" /><br />
                       <asp:ImageButton ID="ZoomImgA4" Cssclass="" runat="server" ImageUrl="~/images/ZoomIMG.png" Width="20px" OnClick="ZoomImgA4_Click"/>
                       </asp:Panel>
                              </asp:Panel>
                    </div>
                            <%--  מסיח מספר 5--%>
                <div id="fifthA">
                              <asp:Panel ID="answer5" runat="server">
                                   <asp:ImageButton ID="deletAns3" runat="server" Height="29px" ImageUrl="~/images/היי.png" Width="28px" OnClick="deletAns3_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;        <asp:TextBox ID="wrongAnswer4" CharacterLimit="20" CharacterForLabel="AnsCount5"  CssClass="CharacterCount" runat="server" Font-Names="Rubik" Font-Size="14pt" Height="25px" Width="300px"></asp:TextBox>
                                  /
                                  <asp:ImageButton ID="imagA5" runat="server" Height="45px" ImageUrl="~/images/imeg.png" Width="45px"  OnClientClick="openFileUploader5(); return false;"  />
                                 <asp:HiddenField ID="Hiddenvar5" runat="server" />
                                  <br />
                            <asp:Label ID="AnsCount5" runat="server" Text="0/20" CssClass="redT"></asp:Label>
                              

                             <asp:Panel ID="PanelA5IMGEdit" CssClass="" runat="server">
                       <asp:ImageButton ID="EditImgA5" runat="server" ImageUrl="~/images/EditIMG.png" OnClientClick="openFileUploader5(); return false;" />
                       <br />
                       <asp:ImageButton ID="DeleteImgA5" runat="server" ImageUrl="~/images/DeleteIMG.png" OnClick="DeleteImgA5_Click" style="height: 22px" /><br />
                       <asp:ImageButton ID="ZoomImgA5" Cssclass="" runat="server" ImageUrl="~/images/ZoomIMG.png" Width="20px" OnClick="ZoomImgA5_Click"/>
                       </asp:Panel>
                                  </asp:Panel>
                    </div>
                    </div>
                    </div>
      <br />
      <br />
               <%-- כפתורים --%>
                       <div class="buttoms">
                       <asp:Button ID="addAnswer" CssClass="addAns" runat="server" OnClick="addAnswer_Click" Text="הוספת מסיח" />
                       <asp:Button ID="saveQ" runat="server" CssClass="EditNOActive" OnClick="saveQ_Click" Text="שמור שאלה"  />
                       &nbsp;<asp:Button ID="cancelEdit" runat="server" CssClass="" OnClick="cancelEdit_Click" Text="ביטול עריכה" />
                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                   <asp:Button ID="goTosetting" runat="server" CssClass="EditActive" Text="הגדרות" OnClick="goTosetting_Click1" />
                  </div>
                   </asp:Panel>
                     <%-- חלונית רקע בהגדלת תמונה --%>

        <asp:Panel ID="BigPic" CssClass="zoomPic" runat="server">
          <asp:ImageButton ID="Imageclose" CssClass="closeZoom" runat="server" ImageUrl="~/images/X.png" OnClick="Imageclose_Click" />
            </asp:Panel>
        </div>
       <%-- <asp:Button ID="savenewpic" runat="server" Text="Button" OnClick="savenewpic_Click" />--%>
           </form>
</body>
</html>
