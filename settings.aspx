<%@ Page Language="C#" AutoEventWireup="true" CodeFile="settings.aspx.cs" Inherits="settings" %>
<!DOCTYPE html>

<link href="Style/StyleSheet.css" rel="stylesheet" />
<link href="Style/menue.css" rel="stylesheet" />

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%-- חיבור בין גאבה סקריפט לעמוד--%> 
    <script src="jscripts/jquery-1.12.0.min.js"></script>
    <script src="jscripts/myScript.js"></script>
    <script src="jScripts/menu.js"></script>
    <style type="text/css">
        .CharacterCount {}
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


        <div>
           <div id="setHead" class="headers"> <asp:Label ID="Label1" runat="server" Text="הגדרות"></asp:Label>

            <asp:Button ID="goBack" runat="server" OnClick="goBack_Click" Text="חזרה למסך הראשי" />
               </div>
            <br />
            <p> שם המשחק <a class="greenT" id ="gamenamewriting"> 2-30 תווים </a>  </p>
          
            <asp:TextBox ID="addGameTB" CharacterLimit="30" CharacterForLabel="nameCount" CssClass="CharacterCount" runat="server" Height="70px" Width="500px" Font-Names="Rubik" Font-Size="14pt"></asp:TextBox>
               
                <asp:Label ID="nameCount" runat="server" Text="" CssClass="redT"></asp:Label>  
            
            <p> זמן לשאלה:</p>
            <asp:RadioButtonList ID="gameTime" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal" Width="566px" Font-Names="Rubik">
                <asp:ListItem Value="30"> שניות 30</asp:ListItem>
                <asp:ListItem Value="60" Selected="True">60 שניות</asp:ListItem>
                <asp:ListItem Value="90">90 שניות</asp:ListItem>
                <asp:ListItem Value="0">ללא הגבלת זמן</asp:ListItem>
            </asp:RadioButtonList>
              <p> אופציות לניקוד משחק:</p>
             <asp:RadioButtonList ID="ScoreGame" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal" Width="380px" Font-Names="Rubik">
                <asp:ListItem Value="1" Selected="True"> מבחן עם ציון</asp:ListItem>
                <asp:ListItem Value="0"> תרגול בלי ציון</asp:ListItem>
            </asp:RadioButtonList>

             <p>   עריכת משוב סיום <a class="greenT" id="endtyping">2-60 תווים</a></p> 
               
            <asp:TextBox ID="endFeedback" CharacterLimit="60"  CharacterForLabel="feedbackCount" CssClass="CharacterCount" runat="server" Height="70px" Width="500px" text="כל הכבוד! סיימת את יום העבודה שלך והבאת את כל הגזרים הביתה." Font-Names="Rubik" Font-Size="14pt"></asp:TextBox>
            <asp:Label ID="feedbackCount" runat="server" Text="0/60" CssClass="redT"></asp:Label>  

            <br />
           <%-- <asp:Label ID="Unpublished" runat="server" Text=""></asp:Label>--%>
        </div>
        <div class="tooltipQ1">
         
        <asp:Button ID="creatGame" runat="server" OnClick="creatGame_Click"  Text="שמור והמשך" />
        <asp:Label ID="tooltip1" CssClass="" runat="server" Text="Label"> שם המשחק אינו עומד בתנאי היצירה</asp:Label>
       </div>
        </form>


</body>
</html>
